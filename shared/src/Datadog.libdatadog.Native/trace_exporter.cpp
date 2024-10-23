#include "pch.h"
#include "trace_exporter.h"

#include "datadog/common.h"
#include "datadog/profiling.h"
#include "datadog/data-pipeline.h"
#include <cassert>
#include "char_slice_builder.h"
#include "byte_slice_builder.h"

TraceExporter::TraceExporter(const std::string url, const std::string tracer_version, const std::string language,
                             const std::string language_version, const std::string language_interpreter,
                             const std::string hostname, const std::string env, const std::string version,
                             const std::string service, const ddog_TraceExporterInputFormat input_format,
                             const ddog_TraceExporterOutputFormat output_format, const bool compute_stats,
                             void (*agent_response_callback)(const char*))
{
    ddog_TraceExporter* exporter = nullptr;
    auto error = ddog_trace_exporter_new(
        &exporter, CharSliceBuilder::from_string(url), CharSliceBuilder::from_string(tracer_version),
        CharSliceBuilder::from_string(language), CharSliceBuilder::from_string(language_version),
        CharSliceBuilder::from_string(language_interpreter), CharSliceBuilder::from_string(hostname),
        CharSliceBuilder::from_string(env), CharSliceBuilder::from_string(version),
        CharSliceBuilder::from_string(service), input_format, output_format, compute_stats,
        agent_response_callback);

    if (error.tag == DDOG_OPTION_ERROR_SOME_ERROR)
    {
        assert(false, "Error creating TraceExporter");
    }

    _trace_exporter = exporter;
}

TraceExporter::~TraceExporter()
{
    ddog_trace_exporter_free(_trace_exporter);
}

void TraceExporter::Send(std::uint8_t* buffer, std::size_t buffer_size, std::size_t trace_count) const
{
    auto slice = ByteSliceBuilder::from_bytes(buffer, buffer_size);
    auto error = ddog_trace_exporter_send(_trace_exporter, slice, trace_count);
    if (error.tag == DDOG_OPTION_ERROR_SOME_ERROR)
    {   
        assert(false, "Error sending trace");
    }
}