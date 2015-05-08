using System;
using System.Threading.Tasks;

namespace Microsoft.Band
{
	public static class BandClientManagerExtensions
	{
		public static Task ConnectTaskAsync (this BandClientManager manager, BandClient client)
		{
			var tcs = new TaskCompletionSource<object> ();

			EventHandler<ClientManagerConnectedEventArgs> onConnected = null;
			EventHandler<ClientManagerDisconnectedEventArgs> onDisconnect = null;
			EventHandler<ClientManagerFailedToConnectEventArgs> onFailed = null;

			// setup the completed event
			onConnected = (sender, args) => {
				if (args.Client == client) {
					manager.Connected -= onConnected;
					manager.Disconnected -= onDisconnect;
					manager.ConnectionFailed -= onFailed;

					// we are finished
					tcs.SetResult (null);
				}
			};
			manager.Connected += onConnected;

			// setup the canceled event
			onDisconnect = (sender, args) => {
				if (args.Client == client) {
					manager.Connected -= onConnected;
					manager.Disconnected -= onDisconnect;
					manager.ConnectionFailed -= onFailed;

					// we were canceled
					tcs.SetCanceled();
				}
			};
			manager.Disconnected += onDisconnect;

			// setup the failed event
			onFailed = (sender, args) => {
				if (args.Client == client) {
					manager.Connected -= onConnected;
					manager.Disconnected -= onDisconnect;
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

			EventHandler<ClientManagerDisconnectedEventArgs> onDisconnect = null;
			EventHandler<ClientManagerFailedToConnectEventArgs> onFailed = null;

			// setup the disconnected event
			onDisconnect = (sender, args) => {
				if (args.Client == client) {
					manager.Disconnected -= onDisconnect;
					manager.ConnectionFailed -= onFailed;

					// we were disconnected (success)
					tcs.SetResult(null);
				}
			};
			manager.Disconnected += onDisconnect;

			// setup the failed event
			onFailed = (sender, args) => {
				if (args.Client == client) {
					manager.Disconnected -= onDisconnect;
					manager.ConnectionFailed -= onFailed;

					// we failed
					tcs.SetException (new BandException(args.Error));
				}
			};
			manager.ConnectionFailed += onFailed;

			// run async
			manager.DisconnectAsync (client);

			return tcs.Task;
		}
	}
}
