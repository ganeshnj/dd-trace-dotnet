#include "pch.h"
#include "char_slice_builder.h"

ddog_CharSlice CharSliceBuilder::from_string(const std::string& str)
{
    return ddog_CharSlice{str.c_str(), static_cast<uint32_t>(str.size())};
}