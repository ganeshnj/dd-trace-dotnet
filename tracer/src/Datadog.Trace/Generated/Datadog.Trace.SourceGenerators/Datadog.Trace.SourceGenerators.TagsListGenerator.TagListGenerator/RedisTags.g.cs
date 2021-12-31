﻿// <auto-generated/>
#nullable enable

namespace Datadog.Trace.ClrProfiler.AutoInstrumentation.Redis
{
    partial class RedisTags
    {
        private static readonly byte[] _bytesSpanKind = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("span.kind");
        private static readonly byte[] _bytesInstrumentationName = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("component");
        private static readonly byte[] _bytesRawCommand = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("redis.raw_command");
        private static readonly byte[] _bytesHost = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("out.host");
        private static readonly byte[] _bytesPort = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("out.port");

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "redis.raw_command" => RawCommand,
                "out.host" => Host,
                "out.port" => Port,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "redis.raw_command": 
                    RawCommand = value;
                    break;
                case "out.host": 
                    Host = value;
                    break;
                case "out.port": 
                    Port = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        protected override int WriteAdditionalTags(ref byte[] bytes, ref int offset)
        {
            var count = 0;
            if (SpanKind != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesSpanKind, SpanKind);
            }

            if (InstrumentationName != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesInstrumentationName, InstrumentationName);
            }

            if (RawCommand != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesRawCommand, RawCommand);
            }

            if (Host != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesHost, Host);
            }

            if (Port != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesPort, Port);
            }

            return count + base.WriteAdditionalTags(ref bytes, ref offset);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (SpanKind != null)
            {
                sb.Append("span.kind (tag):")
                  .Append(SpanKind)
                  .Append(',');
            }

            if (InstrumentationName != null)
            {
                sb.Append("component (tag):")
                  .Append(InstrumentationName)
                  .Append(',');
            }

            if (RawCommand != null)
            {
                sb.Append("redis.raw_command (tag):")
                  .Append(RawCommand)
                  .Append(',');
            }

            if (Host != null)
            {
                sb.Append("out.host (tag):")
                  .Append(Host)
                  .Append(',');
            }

            if (Port != null)
            {
                sb.Append("out.port (tag):")
                  .Append(Port)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}
