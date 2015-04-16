using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Band;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Notifications;
using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Sensors;
using Microsoft.Band.Portable.Tiles;

#if __ANDROID__
using NativeBandClient = Microsoft.Band.IBandClient;
#elif __IOS__
using NativeBandClient = Microsoft.Band.BandClient;
#elif WINDOWS_PHONE_APP
using NativeBandClient = Microsoft.Band.IBandClient;
#endif

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeBandClientManager = Microsoft.Band.BandClientManager;
#endif

namespace Microsoft.Band.Portable
{
    public class BandClient
    {
        private readonly Lazy<BandSensorManager> sensors;
        private readonly Lazy<BandTileManager> tiles;
        private readonly Lazy<BandNotificationManager> notifications;
        private readonly Lazy<BandPersonalizationManager> personalization;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal NativeBandClient Native;

        internal BandClient(NativeBandClient client)
        {
            this.Native = client;

            this.sensors = new Lazy<BandSensorManager>(() => new BandSensorManager(this, Native.SensorManager));
            this.tiles = new Lazy<BandTileManager>(() => new BandTileManager(this, Native.TileManager));
            this.notifications = new Lazy<BandNotificationManager>(() => new BandNotificationManager(this, Native.NotificationManager));
            this.personalization = new Lazy<BandPersonalizationManager>(() => new BandPersonalizationManager(this, Native.PersonalizationManager));
        }
#endif

        public BandSensorManager SensorManager
        {
            get
            {
                CheckDisposed();

                return sensors.Value;
            }
        }

        public BandNotificationManager NotificationManager
        {
            get
            {
                CheckDisposed();

                return notifications.Value;
            }
        }

        public BandTileManager TileManager
        {
            get
            {
                CheckDisposed();

                return tiles.Value;
            }
        }

        public BandPersonalizationManager PersonalizationManager
        {
            get
            {
                CheckDisposed();

                return personalization.Value;
            }
        }

        public bool IsConnected
        {
            get
            {
                CheckDisposed();

#if __ANDROID__
                return Native.IsConnected;
#elif __IOS__
                return Native.IsDeviceConnected;
#elif WINDOWS_PHONE_APP
                return true;
#else // PORTABLE
                return false;
#endif
            }
        }

        public async Task<string> GetFirmwareVersionAsync()
        {
            CheckDisposed();

#if __ANDROID__
            return await Native.GetFirmwareVersionTaskAsync();
#elif __IOS__
            return (string) await Native.GetFirmwareVersionAsyncAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetFirmwareVersionAsync();
#else // PORTABLE
            return null;
#endif
        }

        public async Task<string> GetHardwareVersionAsync()
        {
            CheckDisposed();

#if __ANDROID__
            return await Native.GetHardwareVersionTaskAsync();
#elif __IOS__
            return (string) await Native.GetHardwareVersionAsycAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetHardwareVersionAsync();
#else // PORTABLE
            return null;
#endif
        }

        public async Task DisconnectAsync()
        {
            CheckDisposed();

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
            var nativeClient = Native;
            Native = null;
#endif

#if __ANDROID__
            await nativeClient.DisconnectTaskAsync();
#elif __IOS__
            await NativeBandClientManager.Instance.DisconnectTaskAsync(nativeClient);
#elif WINDOWS_PHONE_APP
            nativeClient.Dispose();
#endif
        }

        private void CheckDisposed()
        {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
            if (Native == null)
            {
                throw new ObjectDisposedException(null);
            }
#endif
        }
    }
}
