using System;
using System.Threading.Tasks;

namespace Microsoft.Band
{
	public static class BandClientManagerExtensions
	{
		public static Task ConnectTaskAsync (this BandClientManager manager, BandClient client)
		{
			var tcs = new TaskCompletionSource<object> ();

			// setup the completed event
			EventHandler<ClientManagerConnectedEventArgs> onConnected = null;
			onConnected = (sender, args) => {
				if (args.Client == client) {
					manager.Connected -= onConnected;

					// we are finished
					tcs.SetResult (null);
				}
			};
			manager.Connected += onConnected;

			// setup the canceled event
			EventHandler<ClientManagerDisconnectedEventArgs> onDisconnect = null;
			onDisconnect = (sender, args) => {
				if (args.Client == client) {
					manager.Disconnected -= onDisconnect;

					// we were canceled
					tcs.SetCanceled();
				}
			};
			manager.Disconnected += onDisconnect;

			// setup the failed event
			EventHandler<ClientManagerFailedToConnectEventArgs> onFailed = null;
			onFailed = (sender, args) => {
				if (args.Client == client) {
					manager.ConnectionFailed -= onFailed;

					// we failed
					tcs.SetException (new BandException(args.Error));
				}
			};
			manager.ConnectionFailed += onFailed;

			// run async
			manager.ConnectAsync (client);

			return tcs.Task;
		}

		public static Task DisconnectTaskAsync (this BandClientManager manager, BandClient client)
		{
			var tcs = new TaskCompletionSource<object> ();

			// setup the disconnected event
			EventHandler<ClientManagerDisconnectedEventArgs> onDisconnect = null;
			onDisconnect = (sender, args) => {
				if (args.Client == client) {
					manager.Disconnected -= onDisconnect;

					// we were disconnected (success)
					tcs.SetResult(null);
				}
			};
			manager.Disconnected += onDisconnect;

			// run async
			manager.ConnectAsync (client);

			return tcs.Task;
		}
	}
}
