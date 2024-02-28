﻿// <copyright file="Obfuscator.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

// turns out strict formatting and optional compilation don't like each other
#pragma warning disable SA1001, SA1116, SA1118

using System;
using System.Text.RegularExpressions;
using Datadog.Trace.Logging;

namespace Datadog.Trace.Util.Http.QueryStringObfuscation
{
    internal class Obfuscator : ObfuscatorBase
    {
        private const string ReplacementString = "<redacted>";
        private readonly Regex _regex;
        private readonly TimeSpan _timeout;
        private readonly IDatadogLogger _logger;

        internal Obfuscator(string pattern, TimeSpan timeout, IDatadogLogger logger)
        {
            _timeout = timeout;
            _logger = logger;

            const RegexOptions options = RegexOptions.Compiled |
                                         RegexOptions.IgnoreCase |
                                         RegexOptions.IgnorePatternWhitespace;

            _regex = new Regex(pattern, options, _timeout);
        }

        /// <summary>
        /// WARNING: This regex cause crashes under netcoreapp2.1 / linux / arm64, dont use on manual instrumentation in this environment
        /// </summary>
        internal override string Obfuscate(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return queryString;
            }

            try
            {
                return _regex.Replace(queryString, ReplacementString);
            }
            catch (RegexMatchTimeoutException exception)
            {
                _logger.Error(exception, "Query string obfuscation timed out with timeout value of {TotalMilliseconds} ms and regex pattern {Pattern}", _timeout.TotalMilliseconds, _regex.ToString());
            }

            return string.Empty;
        }
    }
}
