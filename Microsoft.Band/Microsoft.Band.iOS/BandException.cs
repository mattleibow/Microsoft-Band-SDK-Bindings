using System;

using Foundation;

namespace Microsoft.Band
{
	public class BandException : Exception
	{
		private NSError error;

		public BandException ()
			: base ()
		{

		}

		public BandException (string message)
			: base (message)
		{

		}

		public BandException (string message, Exception innerException)
			: base (message, innerException)
		{

		}

		public BandException (NSError error)
			: this (error.LocalizedDescription, error)
		{
		}

		public BandException (string message, NSError error)
			: base (message)
		{
			this.error = error;
		}

		public nint Code {
			get {
				return this.error.Code;
			}
		}

		public string Domain {
			get {
				return this.error.Domain;
			}
		}

		public NSError Error {
			get {
				return this.error;
			}
		}

		public NSDictionary UserInfo {
			get {
				return this.error.UserInfo;
			}
		}
	}
}
