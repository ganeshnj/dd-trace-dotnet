#pragma once
#include <string>
#include "datadog/data-pipeline.h"

#ifdef DATADOGLIBDATADOGNATIVE_EXPORTS
#define TRACE_EXPORTER_API __declspec(dllexport)
#else
#define TRACE_EXPORTER_API __declspec(dllimport)
#endif

extern "C"
{
    enum TraceExporterInputFormat
    {
        TraceExporterInputFormat_Proxy,
        TraceExporterInputFormat_V04
    };

    enum TraceExporterOutputFormat
    {
        TraceExporterOutputFormat_V04,
        TraceExporterOutputFormat_V07
    };

    typedef void (*AgentResponseCallback)(const char*);

    TRACE_EXPORTER_API void* CreateTraceExporter(const char* url, const char* tracer_version, const char* language,
                                                 const char* language_version, const char* language_interpreter,
                                                 const char* hostname, const char* env, const char* version,
                                                 const char* service, TraceExporterInputFormat input_format,
                                                 TraceExporterOutputFormat output_format, bool compute_stats,
                                                 AgentResponseCallback callback);

    TRACE_EXPORTER_API void DestroyTraceExporter(void* exporter);

    TRACE_EXPORTER_API void SendTrace(void* exporter, std::uint8_t* buffer, std::size_t buffer_size,
                                      std::size_t trace_count);
}

class TraceExporter
{
public:
    TraceExporter(const std::string url, const std::string tracer_version, const std::string language,
                  const std::string language_version, const std::string language_interpreter,
                  const std::string hostname, const std::string env, const std::string version,
                  const std::string service, const TraceExporterInputFormat input_format,
                  const TraceExporterOutputFormat output_format, const bool compute_stats,
                  void (*agent_response_callback)(const char*));
    ~TraceExporter();
    void Send(std::uint8_t* buffer, std::size_t buffer_size, std::size_t trace_count) const;
private:
    ddog_TraceExporter* _trace_exporter;
};