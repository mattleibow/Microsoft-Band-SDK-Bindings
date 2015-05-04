using System;
using System.Threading.Tasks;

using Foundation;

namespace Microsoft.Band.Notifications
{
	public static class BandClientManagerExtensions
	{
		public static Task VibrateTaskAsync (this IBandNotificationManager manager, VibrationType vibrationType)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.VibrateAsync (vibrationType, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task ShowDialogTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.ShowDialogAsync (tileID, title, body, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task SendMessageTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body, NSDate timeStamp, MessageFlags flags)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.SendMessageAsync (tileID, title, body, timeStamp, flags, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task SendMessageTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body, NSDate timeStamp, bool showDialog)
		{
			return manager.SendMessageTaskAsync (tileID, title, body, timeStamp, showDialog ? MessageFlags.ShowDialog : MessageFlags.None);
		}

		public static Task SendMessageTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body, NSDate timeStamp)
		{
			return manager.SendMessageTaskAsync (tileID, title, body, timeStamp, false);
		}

		public static Task SendMessageTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body, DateTime timeStamp, MessageFlags flags)
		{
			return manager.SendMessageTaskAsync (tileID, title, body, (NSDate)timeStamp, flags);
		}

		public static Task SendMessageTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body, DateTime timeStamp, bool showDialog)
		{
			return manager.SendMessageTaskAsync (tileID, title, body, (NSDate)timeStamp, showDialog);
		}

		public static Task SendMessageTaskAsync (this IBandNotificationManager manager, NSUuid tileID, string title, string body, DateTime timeStamp)
		{
			return manager.SendMessageTaskAsync (tileID, title, body, (NSDate)timeStamp);
		}

		public static Task RegisterNotificationTaskAsync (this IBandNotificationManager manager, NSUuid tileID)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.RegisterNotificationAsync (tileID, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task RegisterNotificationTaskAsync (this IBandNotificationManager manager)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.RegisterNotificationAsync (tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task UnregisterNotificationTaskAsync (this IBandNotificationManager manager)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.UnregisterNotificationAsync (tcs.AttachCompletionHandler ());
			return tcs.Task;
		}
	}

}
