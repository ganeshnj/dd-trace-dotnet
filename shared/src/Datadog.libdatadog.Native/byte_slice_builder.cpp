#include "pch.h"
#include "byte_slice_builder.h"

ddog_ByteSlice ByteSliceBuilder::from_bytes(const std::uint8_t* bytes, std::size_t size)
{
    ddog_ByteSlice slice{};
    slice.ptr = bytes;
    slice.len = size;
    return slice;
}