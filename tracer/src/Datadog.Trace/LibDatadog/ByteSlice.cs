// <copyright file="ByteSlice.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Runtime.InteropServices;

namespace Datadog.Trace.LibDatadog;

/// <summary>
/// Represents a slice of a byte array in memory.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct ByteSlice
{
    /// <summary>
    /// Pointer to the start of the slice.
    /// </summary>
    internal IntPtr Ptr;

    /// <summary>
    /// Length of the slice.
    /// </summary>
    internal UIntPtr Len;

    /// <summary>
    /// Initializes a new instance of the <see cref="ByteSlice"/> struct.
    /// </summary>
    internal ByteSlice(byte[] bytes)
    {
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        Ptr = handle.AddrOfPinnedObject();
        Len = (UIntPtr)bytes.Length;
    }

    /// <summary>
    /// Frees the memory allocated for the slice.
    /// </summary>
    public void Free()
    {
        var handle = GCHandle.FromIntPtr(Ptr);
        handle.Free();
    }
}
