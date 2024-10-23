#pragma once
#include <string>
#include "datadog/data-pipeline.h"

class TraceExporter
{
public:
    TraceExporter(const std::string url, const std::string tracer_version, const std::string language,
                  const std::string language_version, const std::string language_interpreter,
                  const std::string hostname, const std::string env, const std::string version,
                  const std::string service, const ddog_TraceExporterInputFormat input_format,
                  const ddog_TraceExporterOutputFormat output_format, const bool compute_stats,
                  void (*agent_response_callback)(const char*));
    ~TraceExporter();
    void Send(std::uint8_t* buffer, std::size_t buffer_size, std::size_t trace_count) const;
private:
    ddog_TraceExporter* _trace_exporter;
};