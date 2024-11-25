// <copyright file="TraceExporter.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Datadog.Trace.Agent;
using Datadog.Trace.Configuration;

namespace Datadog.Trace.LibDatadog;

internal class TraceExporter : IApi, IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public TraceExporter(ImmutableTracerSettings settings)
    {
        var url = new CharSlice(settings.ExporterInternal.AgentUriInternal.ToString());
        var tracerVersion = new CharSlice(TracerConstants.AssemblyVersion);
        var language = new CharSlice(".NET");
        var languageVersion = new CharSlice(Environment.Version.ToString());
        var languageInterpreter = new CharSlice(".NET");
        var hostname = new CharSlice(settings.ExporterInternal.AgentUriInternal.ToString());
        var env = new CharSlice(settings.EnvironmentInternal);
        var service = new CharSlice(settings.ServiceNameInternal);
        var serviceVersion = new CharSlice(settings.ServiceVersionInternal);

        var error = Native.ddog_trace_exporter_new(
            outHandle: ref _handle,
            url: url,
            tracerVersion: tracerVersion,
            language: language,
            languageVersion: languageVersion,
            languageInterpreter: languageInterpreter,
            hostname: hostname,
            env: env,
            version: serviceVersion,
            service: service,
            inputFormat: TraceExporterInputFormat.V04,
            outputFormat: TraceExporterOutputFormat.V04,
            computeStats: false,
            agentResponseCallback: (IntPtr chars) =>
            {
                var response = Marshal.PtrToStringUni(chars);
                Console.WriteLine(response);
            });

        if (error.Tag == ErrorTag.Some)
        {
            throw new LibDatadogException(error);
        }
    }

    ~TraceExporter()
    {
        ReleaseUnmanagedResources();
    }

    public Task<bool> SendTracesAsync(ArraySegment<byte> traces, int numberOfTraces, bool statsComputationEnabled, long numberOfDroppedP0Traces, long numberOfDroppedP0Spans, bool appsecStandaloneEnabled)
    {
        var tracesSlice = new ByteSlice(traces.Array);
        try
        {
            var error = Native.ddog_trace_exporter_send(_handle, tracesSlice, (UIntPtr)numberOfTraces);
            if (error.Tag == ErrorTag.Some)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        finally
        {
            tracesSlice.Free();
        }
    }

    public Task<bool> SendStatsAsync(StatsBuffer stats, long bucketDuration)
    {
        throw new NotImplementedException();
    }

    private void ReleaseUnmanagedResources()
    {
        var handle = GCHandle.FromIntPtr(_handle);
        handle.Free();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

#pragma warning disable SA1300
    internal class Native
    {
        private const string DllName = "libdatadog_profiling";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern MaybeError ddog_trace_exporter_new(
            ref IntPtr outHandle,
            CharSlice url,
            CharSlice tracerVersion,
            CharSlice language,
            CharSlice languageVersion,
            CharSlice languageInterpreter,
            CharSlice hostname,
            CharSlice env,
            CharSlice version,
            CharSlice service,
            TraceExporterInputFormat inputFormat,
            TraceExporterOutputFormat outputFormat,
            bool computeStats,
            AgentResponseCallback agentResponseCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddog_MaybeError_drop(MaybeError error);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ddog_trace_exporter_free(IntPtr handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern MaybeError ddog_trace_exporter_send(IntPtr handle, ByteSlice trace, UIntPtr traceCount);
    }
#pragma warning restore SA1300
}
