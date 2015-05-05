using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if __ANDROID__
using Android.App;
using Android.Content;
#endif

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeBandClientManager = Microsoft.Band.BandClientManager;
#endif

namespace Microsoft.Band.Portable
{
    /// <summary>
    /// Provides access to paired Band devices and the ability to connect to them.
    /// </summary>
    public class BandClientManager
    {
        private static Lazy<BandClientManager> instance;

        static BandClientManager()
        {
            instance = new Lazy<BandClientManager>(() => new BandClientManager());
        }

        /// <summary>
        /// Gets the value representing the current instance of the client manager.
        /// </summary>
        /// <value>
        /// The current instance of the client manager.
        /// </value>
        public static BandClientManager Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// Returns a collection of the Band devices that are paired with the current device.
        /// </summary>
        /// <returns>A collection of the paired Bands.</returns>
        public async Task<IEnumerable<BandDeviceInfo>> GetPairedBandsAsync()
        {
#if __ANDROID__
            return NativeBandClientManager.Instance.GetPairedBands().Select(b => new BandDeviceInfo(b));
#elif __IOS__
            return NativeBandClientManager.Instance.AttachedClients.Select(b => new BandDeviceInfo(b));
#elif WINDOWS_PHONE_APP
            return (await NativeBandClientManager.Instance.GetBandsAsync()).Select(b => new BandDeviceInfo(b));
#else
            return null;
#endif
        }

        /// <summary>
        /// Connects to the Band device specified by the device information, 
        /// and returns a client that is used for communication.
        /// </summary>
        /// <param name="info">The Band device information to connect to.</param>
        /// <returns>The client instance that is used to communicate with the connected Band device.</returns>
        public async Task<BandClient> ConnectAsync(BandDeviceInfo info)
        {
#if __ANDROID__
            var nativeClient = NativeBandClientManager.Instance.Create(Application.Context, info.Native);
            var result = await nativeClient.ConnectTaskAsync() == ConnectionState.Connected;
            return new BandClient(nativeClient);
#elif __IOS__
            await NativeBandClientManager.Instance.ConnectTaskAsync(info.Native);
            return new BandClient(info.Native);
#elif WINDOWS_PHONE_APP
            var nativeClient = await NativeBandClientManager.Instance.ConnectAsync(info.Native);
            return new BandClient(nativeClient);
#else
            return null;
#endif
        }
    }
}
