set -e

clean=false
output_dir=$(dirname "$0")
version="14.2.0"
artifacts=(
    "libdatadog-aarch64-alpine-linux-musl.tar.gz"
    "libdatadog-aarch64-apple-darwin.tar.gz"
    "libdatadog-aarch64-unknown-linux-gnu.tar.gz"
    "libdatadog-x86_64-alpine-linux-musl.tar.gz"
    "libdatadog-x86_64-unknown-linux-gnu.tar.gz"
)

parse_args() {
    while [[ "$#" -gt 0 ]]; do
        case $1 in
            --clean) clean=true ;;
            --output) output_dir="$2"; shift ;;
            --version) version="$2"; shift ;;
            *) echo "Unknown parameter passed: $1"; exit 1 ;;
        esac
        shift
    done
}

prepare_output_dir() {
    if [ ! -d "$output_dir" ]; then
        echo "Output directory $output_dir does not exist"
        mkdir -p "$output_dir"
    fi
}

download_artifacts() {
    mkdir -p releases
    for artifact in "${artifacts[@]}"; do
        url="https://github.com/DataDog/libdatadog/releases/download/v$version/$artifact"
        echo "Downloading $url to releases/$artifact"
        curl -L -o releases/$artifact $url
    done
}

extract_artifacts() {
    for artifact in "${artifacts[@]}"; do
        echo "Extracting releases/$artifact"
        tar -xzf releases/$artifact -C releases
    done
}

copy_to_bin() {
    echo "Copying to runtimes"
    mkdir -p runtimes/linux-musl-arm64/native
    cp releases/libdatadog-aarch64-alpine-linux-musl/lib/libdatadog_profiling.so runtimes/linux-musl-arm64/native/libdatadog_profiling.so
    cp releases/libdatadog-aarch64-alpine-linux-musl/lib/libdatadog_profiling.so runtimes/linux-musl-arm64/native/libdatadog_profiling.debug.so

    mkdir -p runtimes/osx-arm64/native
    cp releases/libdatadog-aarch64-apple-darwin/lib/libdatadog_profiling.dylib runtimes/osx-arm64/native/libdatadog_profiling.dylib

    mkdir -p runtimes/linux-arm64/native
    cp releases/libdatadog-aarch64-unknown-linux-gnu/lib/libdatadog_profiling.so runtimes/linux-arm64/native/libdatadog_profiling.so
    cp releases/libdatadog-aarch64-unknown-linux-gnu/lib/libdatadog_profiling.so runtimes/linux-arm64/native/libdatadog_profiling.debug.so

    mkdir -p runtimes/linux-musl-x64/native
    cp releases/libdatadog-x86_64-alpine-linux-musl/lib/libdatadog_profiling.so runtimes/linux-musl-x64/native/libdatadog_profiling.so
    cp releases/libdatadog-x86_64-alpine-linux-musl/lib/libdatadog_profiling.so runtimes/linux-musl-x64/native/libdatadog_profiling.debug.so

    mkdir -p runtimes/linux-x64/native
    cp releases/libdatadog-x86_64-unknown-linux-gnu/lib/libdatadog_profiling.so runtimes/linux-x64/native/libdatadog_profiling.so
    cp releases/libdatadog-x86_64-unknown-linux-gnu/lib/libdatadog_profiling.so runtimes/linux-x64/native/libdatadog_profiling.debug.so
}

copy_headers() {
    echo "Copying headers"
    cp -r releases/libdatadog-aarch64-alpine-linux-musl/include include
}

download_windows_artifacts() {
    url="https://globalcdn.nuget.org/packages/libdatadog.$version.nupkg?packageVersion=$version"
    echo "Downloading $url to releases/libdatadog.$version.nupkg"
    curl -L -o releases/libdatadog.$version.nupkg $url

    echo "Extracting releases/libdatadog.$version.nupkg"
    unzip -o releases/libdatadog.$version.nupkg -d releases/libdatadog.$version
}

copy_windows_artifacts() {
    echo "Copying to runtimes"
    mkdir -p runtimes/win-x64/native
    cp releases/libdatadog.$version/build/native/lib/x64/release/datadog_profiling_ffi.dll runtimes/win-x64/native/libdatadog_profiling.dll
    cp releases/libdatadog.$version/build/native/lib/x64/release/datadog_profiling_ffi.pdb runtimes/win-x64/native/libdatadog_profiling.pdb

    mkdir -p runtimes/win-x86/native
    cp releases/libdatadog.$version/build/native/lib/x86/release/datadog_profiling_ffi.dll runtimes/win-x86/native/libdatadog_profiling.dll
    cp releases/libdatadog.$version/build/native/lib/x86/release/datadog_profiling_ffi.pdb runtimes/win-x86/native/libdatadog_profiling.pdb
}

create_nuget_package() {
    dotnet pack libdatadog.csproj -p:NuspecFile=libdatadog.nuspec --output $output_dir
    echo "Size of the package"
    du -sh $output_dir/libdatadog.1.0.0.nupkg
}

clean_up() {
    if [ "$clean" = true ]; then
        echo "Cleaning up local files"
        rm -rf releases
        rm -rf include
        rm -rf runtimes

        echo "Cleaning up ~/.nuget/packages/libdatadog global cache"
        rm ~/.nuget/packages/libdatadog/* -rf
    fi
}

main() {
    original_dir=$(pwd)
    echo "Original directory: $original_dir"
    echo "Changing directory to $(dirname "$0")"
    cd "$(dirname "$0")"
    trap 'cd "$original_dir"' EXIT
    parse_args "$@"
    prepare_output_dir
    download_artifacts
    extract_artifacts
    copy_to_bin
    copy_headers
    download_windows_artifacts
    copy_windows_artifacts
    create_nuget_package
    clean_up
}

main "$@"