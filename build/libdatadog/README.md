# README

This is a workaround to take dependency on libdatadog in a csproj project.

## Usage

1. Build the nuget package
```bash
./build.sh --clean --output ../../../../packages --version 14.2.0
```

2. The nuget.config file at the root of the respective project should have the following entry:
```xml
    <add key="local" value="packages" />
```
which is essentially the location where the nuget package is built.
