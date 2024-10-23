// <copyright file="TraceExporter.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Datadog.Trace.Agent;
using Datadog.Trace.Configuration;

namespace Datadog.Trace.Libdatadog
{
    internal delegate void AgentResponseCallback(string response);

    internal enum TraceExporterInputFormat
    {
        Proxy,
        V04
    }

    internal enum TraceExporterOutputFormat
    {
        V04,
        V07
    }

    internal class TraceExporter : IDisposable, IApi
    {
        private IntPtr _nativeExporter;

        public TraceExporter(
            string url,
            string tracerVersion,
            string language,
            string languageVersion,
            string languageInterpreter,
            string hostname,
            string env,
            string version,
            string service,
            TraceExporterInputFormat inputFormat,
            TraceExporterOutputFormat outputFormat,
            bool computeStats,
            AgentResponseCallback callback)
        {
            _nativeExporter = Native.CreateTraceExporter(
                url,
                tracerVersion,
                language,
                languageVersion,
                languageInterpreter,
                hostname,
                env,
                version,
                service,
                inputFormat,
                outputFormat,
                computeStats,
                callback);
        }

        internal TraceExporter(
            ImmutableTracerSettings settings)
        {
            _nativeExporter = Native.CreateTraceExporter(
                "localhost:8127",
                TracerConstants.AssemblyVersion,
                ".NET",
                FrameworkDescription.Instance.ProductVersion,
                FrameworkDescription.Instance.Name,
                settings.ExporterInternal.AgentUriInternal.ToString(),
                settings.EnvironmentInternal,
                settings.ServiceVersionInternal,
                settings.ServiceNameInternal,
                TraceExporterInputFormat.V04,
                TraceExporterOutputFormat.V07,
                false,
                (response) =>
                {
                    Console.WriteLine(response);
                });
        }

        public void Send(byte[] buffer, int bufferSize, int traceCount)
        {
            Native.SendTrace(_nativeExporter, buffer, bufferSize, traceCount);
        }

        public void Dispose()
        {
            if (_nativeExporter != IntPtr.Zero)
            {
                Native.DestroyTraceExporter(_nativeExporter);
                _nativeExporter = IntPtr.Zero;
            }
        }

        public async Task<bool> SendTracesAsync(ArraySegment<byte> traces, int numberOfTraces, bool statsComputationEnabled, long numberOfDroppedP0Traces, long numberOfDroppedP0Spans, bool appsecStandaloneEnabled)
        {
            // Convert ArraySegment<byte> to byte array
            var buffer = traces.Array;
            var bufferSize = traces.Count;

            // Call the native SendTrace method
            await Task.Run(() => Send(buffer, bufferSize, numberOfTraces)).ConfigureAwait(false);

            // For simplicity, assume the operation is always successful
            return true;
        }

        public async Task<bool> SendStatsAsync(StatsBuffer stats, long bucketDuration)
        {
            // Implement the logic to send stats
            // For now, just return true to indicate success
            await Task.CompletedTask.ConfigureAwait(false);
            return true;
        }

        private class Native
        {
            [DllImport("Datadog.libdatadog.Native.dll", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr CreateTraceExporter(
              string url,
              string tracerVersion,
              string language,
              string languageVersion,
              string languageInterpreter,
              string hostname,
              string env,
              string version,
              string service,
              TraceExporterInputFormat inputFormat,
              TraceExporterOutputFormat outputFormat,
              bool computeStats,
              AgentResponseCallback callback);

            [DllImport("Datadog.libdatadog.Native.dll", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void DestroyTraceExporter(IntPtr exporter);

            [DllImport("Datadog.libdatadog.Native.dll", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SendTrace(IntPtr exporter, byte[] buffer, int bufferSize, int traceCount);
        }
    }
}
