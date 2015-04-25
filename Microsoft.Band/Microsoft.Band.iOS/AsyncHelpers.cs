using System;
using System.Threading.Tasks;

using Foundation;

namespace Microsoft.Band
{
	internal static class AsyncHelpers
	{
		internal static Action<NSError> AttachCompletionHandler (this TaskCompletionSource<object> tcs)
		{
			return delegate (NSError error) {
				if (error == null) {
					tcs.SetResult (null);
				} else {
					tcs.SetException (new BandException (error));
				}
			};
		}

		internal static Action<T, NSError> AttachCompletionHandler<T> (this TaskCompletionSource<T> tcs)
		{
			return delegate (T data, NSError error) {
				if (error == null) {
					tcs.SetResult (data);
				} else {
					tcs.SetException (new BandException (error));
				}
			};
		}

		internal static Action<NSString, NSError> AttachCompletionHandler (this TaskCompletionSource<string> tcs)
		{
			return delegate (NSString data, NSError error) {
				if (error == null) {
					tcs.SetResult ((string) data);
				} else {
					tcs.SetException (new BandException (error));
				}
			};
		}
	}
}
