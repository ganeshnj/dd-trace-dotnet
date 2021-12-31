﻿// <auto-generated/>
#nullable enable

namespace Datadog.Trace.Tagging
{
    partial class CouchbaseTags
    {
        private static readonly byte[] _bytesSpanKind = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("span.kind");
        private static readonly byte[] _bytesInstrumentationName = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("component");
        private static readonly byte[] _bytesOperationCode = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("couchbase.operation.code");
        private static readonly byte[] _bytesBucket = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("couchbase.operation.bucket");
        private static readonly byte[] _bytesKey = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("couchbase.operation.key");
        private static readonly byte[] _bytesHost = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("out.host");
        private static readonly byte[] _bytesPort = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("out.port");

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "couchbase.operation.code" => OperationCode,
                "couchbase.operation.bucket" => Bucket,
                "couchbase.operation.key" => Key,
                "out.host" => Host,
                "out.port" => Port,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "couchbase.operation.code": 
                    OperationCode = value;
                    break;
                case "couchbase.operation.bucket": 
                    Bucket = value;
                    break;
                case "couchbase.operation.key": 
                    Key = value;
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

            if (OperationCode != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesOperationCode, OperationCode);
            }

            if (Bucket != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesBucket, Bucket);
            }

            if (Key != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, _bytesKey, Key);
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

            if (OperationCode != null)
            {
                sb.Append("couchbase.operation.code (tag):")
                  .Append(OperationCode)
                  .Append(',');
            }

            if (Bucket != null)
            {
                sb.Append("couchbase.operation.bucket (tag):")
                  .Append(Bucket)
                  .Append(',');
            }

            if (Key != null)
            {
                sb.Append("couchbase.operation.key (tag):")
                  .Append(Key)
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
