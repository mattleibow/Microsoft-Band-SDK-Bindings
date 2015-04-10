using System;
using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Notifications;
using NativeBandNotificationManager = Microsoft.Band.Notifications.IBandNotificationManager;
#elif __IOS__
using Microsoft.Band.Notifications;
using NativeBandNotificationManager = Microsoft.Band.Notifications.IBandNotificationManager;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Notifications;
using NativeBandNotificationManager = Microsoft.Band.Notifications.IBandNotificationManager;
#endif

namespace Microsoft.Band.Portable.Notifications
{
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

        public async Task SendMessageAsync(Guid tileId, string title, string body, DateTime timestamp)
        {
            await SendMessageAsync(tileId, title, body, timestamp, false);
        }

        public async Task SendMessageAsync(Guid tileId, string title, string body, DateTime timestamp, bool showDialog)
        {
            await SendMessageAsync(tileId, title, body, timestamp, showDialog ? MessageFlags.ShowDialog : MessageFlags.None);
        }

        public async Task SendMessageAsync(Guid tileId, string title, string body, DateTime timestamp, MessageFlags messageFlags)
        {
#if __ANDROID__
            await Native.SendMessageTaskAsync(tileId.ToNative(), title, body, timestamp, messageFlags.ToNative());
#elif __IOS__
            await Native.SendMessageTaskAsync(tileId.ToNative(), title, body, timestamp, messageFlags.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.SendMessageAsync(tileId.ToNative(), title, body, timestamp, messageFlags.ToNative());
#endif
        }

        public async Task ShowDialogAsync(Guid tileId, string title, string body)
        {
#if __ANDROID__
            await Native.ShowDialogTaskAsync(tileId.ToNative(), title, body);
#elif __IOS__
            await Native.ShowDialogTaskAsync(tileId.ToNative(), title, body);
#elif WINDOWS_PHONE_APP
            await Native.ShowDialogAsync(tileId.ToNative(), title, body);
#endif
        }

        public async Task VibrateAsync(VibrationType vibrationType)
        {
#if __ANDROID__
            await Native.VibrateTaskAsync(vibrationType.ToNative());
#elif __IOS__
            await Native.VibrateTaskAsync(vibrationType.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.VibrateAsync(vibrationType.ToNative());
#endif
        }
    }
}
