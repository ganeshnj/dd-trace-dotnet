//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0023, SYSLIB0032
#if NETFRAMEWORK
using System;
using System.Runtime.Serialization;

namespace Datadog.Trace.Vendors.ICSharpCode.SharpZipLib
{
	/// <summary>
	/// Indicates that the input stream could not decoded due to known library incompability or missing features
	/// </summary>
	[Serializable]
	internal class StreamUnsupportedException : StreamDecodingException
	{
		private const string GenericMessage = "Input stream is in a unsupported format";

		/// <summary>
		/// Initializes a new instance of the StreamUnsupportedException with a generic message
		/// </summary>
		public StreamUnsupportedException() : base(GenericMessage) { }

		/// <summary>
		/// Initializes a new instance of the StreamUnsupportedException class with a specified error message.
		/// </summary>
		/// <param name="message">A message describing the exception.</param>
		public StreamUnsupportedException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the StreamUnsupportedException class with a specified
		/// error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A message describing the exception.</param>
		/// <param name="innerException">The inner exception</param>
		public StreamUnsupportedException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the StreamUnsupportedException class with serialized data.
		/// </summary>
		/// <param name="info">
		/// The System.Runtime.Serialization.SerializationInfo that holds the serialized
		/// object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The System.Runtime.Serialization.StreamingContext that contains contextual information
		/// about the source or destination.
		/// </param>
		protected StreamUnsupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

#endif