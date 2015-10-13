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
    /// <summary>
    /// Represents a connected Band device.
    /// </summary>
    public class BandClient
    {
        private readonly Lazy<BandSensorManager> sensors;
        private readonly Lazy<BandTileManager> tiles;
        private readonly Lazy<BandNotificationManager> notifications;
        private readonly Lazy<BandPersonalizationManager> personalization;

        private BandClient()
        {
        }

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

        /// <summary>
        /// Gets the value representing the current instance of the sensor manager.
        /// </summary>
        /// <value>
        /// The current instance of the sensor manager.
        /// </value>
        public BandSensorManager SensorManager
        {
            get
            {
                CheckDisposed();

                return sensors.Value;
            }
        }

        /// <summary>
        /// Gets the value representing the current instance of the notification manager.
        /// </summary>
        /// <value>
        /// The current instance of the notification manager.
        /// </value>
        public BandNotificationManager NotificationManager
        {
            get
            {
                CheckDisposed();

                return notifications.Value;
            }
        }

        /// <summary>
        /// Gets the value representing the current instance of the tile manager.
        /// </summary>
        /// <value>
        /// The current instance of the tile manager.
        /// </value>
        public BandTileManager TileManager
        {
            get
            {
                CheckDisposed();

                return tiles.Value;
            }
        }

        /// <summary>
        /// Gets the value representing the current instance of the personalization manager.
        /// </summary>
        /// <value>
        /// The current instance of the personalization manager.
        /// </value>
        public BandPersonalizationManager PersonalizationManager
        {
            get
            {
                CheckDisposed();

                return personalization.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is connected to a Band device.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected to a Band device; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get
            {
                CheckDisposed();

#if __ANDROID__ || __IOS__
                return Native.IsConnected;
#elif WINDOWS_PHONE_APP
                return true;
#else // PORTABLE
                return false;
#endif
            }
        }

        /// <summary>
        /// Returns the firmware version of the Band.
        /// </summary>
        /// <returns>A string representing the firmware version of the Band.</returns>
        public async Task<string> GetFirmwareVersionAsync()
        {
            CheckDisposed();

#if __ANDROID__
            return await Native.GetFirmwareVersionTaskAsync();
#elif __IOS__
            return (string) await Native.GetFirmwareVersionTaskAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetFirmwareVersionAsync();
#else // PORTABLE
            return null;
#endif
        }

        /// <summary>
        /// Returns the hardware version of the Band.
        /// </summary>
        /// <returns>A string representing the hardware version of the Band.</returns>
        public async Task<string> GetHardwareVersionAsync()
        {
            CheckDisposed();

#if __ANDROID__
            return await Native.GetHardwareVersionTaskAsync();
#elif __IOS__
            return (string) await Native.GetHardwareVersionTaskAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetHardwareVersionAsync();
#else // PORTABLE
            return null;
#endif
        }

        /// <summary>
        /// Disconnects from the current Band device.
        /// </summary>
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
