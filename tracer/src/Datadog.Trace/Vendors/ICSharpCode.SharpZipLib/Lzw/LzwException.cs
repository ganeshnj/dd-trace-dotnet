//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0023, SYSLIB0032
#if NETFRAMEWORK
using System;
using System.Runtime.Serialization;

namespace Datadog.Trace.Vendors.ICSharpCode.SharpZipLib.Lzw
{
	/// <summary>
	/// LzwException represents exceptions specific to LZW classes and code.
	/// </summary>
	[Serializable]
	internal class LzwException : SharpZipBaseException
	{
		/// <summary>
		/// Initialise a new instance of <see cref="LzwException" />.
		/// </summary>
		public LzwException()
		{
		}

		/// <summary>
		/// Initialise a new instance of <see cref="LzwException" /> with its message string.
		/// </summary>
		/// <param name="message">A <see cref="string"/> that describes the error.</param>
		public LzwException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initialise a new instance of <see cref="LzwException" />.
		/// </summary>
		/// <param name="message">A <see cref="string"/> that describes the error.</param>
		/// <param name="innerException">The <see cref="Exception"/> that caused this exception.</param>
		public LzwException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LzwException class with serialized data.
		/// </summary>
		/// <param name="info">
		/// The System.Runtime.Serialization.SerializationInfo that holds the serialized
		/// object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The System.Runtime.Serialization.StreamingContext that contains contextual information
		/// about the source or destination.
		/// </param>
		protected LzwException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

#endif