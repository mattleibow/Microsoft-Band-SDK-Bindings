using System;
using System.Threading.Tasks;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using Microsoft.Band.Notifications;
using NativeBandNotificationManager = Microsoft.Band.Notifications.IBandNotificationManager;
#endif

namespace Microsoft.Band.Portable.Notifications
{
    /// <summary>
    /// Represents the notification manager for a connected Band device.
    /// </summary>
    public class BandNotificationManager
    {
        private readonly BandClient client;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandNotificationManager Native;

        internal BandNotificationManager(BandClient client, NativeBandNotificationManager notificationManager)
        {
            this.Native = notificationManager;

            this.client = client;
        }
#endif

        /// <summary>
        /// Sends a message to a specific tile to the connected Band device with the provided tile ID, title,
        /// body and timestamp.
        /// </summary>
        /// <param name="tileId">The tile identifier.</param>
        /// <param name="title">The message title.</param>
        /// <param name="body">The message body.</param>
        /// <param name="timestamp">The message timestamp.</param>
        public async Task SendMessageAsync(Guid tileId, string title, string body, DateTime timestamp)
        {
            await SendMessageAsync(tileId, title, body, timestamp, false);
        }

        /// <summary>
        /// Sends a message to a specific tile to the connected Band device with the provided tile ID, title, body, 
        /// timestamp and, optionally, with a dialog.
        /// </summary>
        /// <param name="tileId">The tile identifier.</param>
        /// <param name="title">The message title.</param>
        /// <param name="body">The message body.</param>
        /// <param name="timestamp">The message timestamp.</param>
        /// <param name="showDialog">Display a dialog if set to <c>true</c>; otherwise, don't.</param>
        public async Task SendMessageAsync(Guid tileId, string title, string body, DateTime timestamp, bool showDialog)
        {
            await SendMessageAsync(tileId, title, body, timestamp, showDialog ? MessageFlags.ShowDialog : MessageFlags.None);
        }

        /// <summary>
        /// Sends a message to a specific tile to the connected Band device with the provided tile ID, title, body, 
        /// timestamp and with message flags to control how the message is provided.
        /// </summary>
        /// <param name="tileId">The tile identifier.</param>
        /// <param name="title">The message title.</param>
        /// <param name="body">The message body.</param>
        /// <param name="timestamp">The message timestamp.</param>
        /// <param name="messageFlags">The message flags to control how the message is provided to the Band device.</param>
        public async Task SendMessageAsync(Guid tileId, string title, string body, DateTime timestamp, MessageFlags messageFlags)
        {
#if __ANDROID__ || __IOS__
            await Native.SendMessageTaskAsync(tileId.ToNative(), title, body, timestamp, messageFlags.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.SendMessageAsync(tileId.ToNative(), title, body, timestamp, messageFlags.ToNative());
#endif
        }

        /// <summary>
        /// Shows a dialog on the connected Band device.
        /// </summary>
        /// <param name="tileId">The tile identifier.</param>
        /// <param name="title">The message title.</param>
        /// <param name="body">The message body.</param>
        public async Task ShowDialogAsync(Guid tileId, string title, string body)
        {
#if __ANDROID__ || __IOS__
            await Native.ShowDialogTaskAsync(tileId.ToNative(), title, body);
#elif WINDOWS_PHONE_APP
            await Native.ShowDialogAsync(tileId.ToNative(), title, body);
#endif
        }

        /// <summary>
        /// Vibrates the connected Band device using the specified vibration type.
        /// </summary>
        /// <param name="vibrationType">Type of vibration to use.</param>
        public async Task VibrateAsync(VibrationType vibrationType)
        {
#if __ANDROID__ || __IOS__
            await Native.VibrateTaskAsync(vibrationType.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.VibrateAsync(vibrationType.ToNative());
#endif
        }
    }
}
