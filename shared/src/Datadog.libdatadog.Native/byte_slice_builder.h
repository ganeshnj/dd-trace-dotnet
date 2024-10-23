#pragma once
#include <datadog/common.h>
#include <cstdint>
#include <cstddef>

class ByteSliceBuilder
{
public:
    static ddog_ByteSlice from_bytes(const std::uint8_t* bytes, std::size_t size); 
};
