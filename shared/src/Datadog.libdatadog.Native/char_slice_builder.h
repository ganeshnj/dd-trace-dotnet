#pragma once

#include <string> 
#include "datadog/profiling.h"

class CharSliceBuilder
{
public:
    static ddog_CharSlice from_string(const std::string& str);
};
