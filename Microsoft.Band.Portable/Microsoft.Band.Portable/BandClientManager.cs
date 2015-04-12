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
    public class BandClientManager
    {
        private static Lazy<BandClientManager> instance;

        static BandClientManager()
        {
            instance = new Lazy<BandClientManager>(() => new BandClientManager());
        }

        public static BandClientManager Instance
        {
            get { return instance.Value; }
        }

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

        public async Task<BandClient> ConnectAsync(BandDeviceInfo info)
        {
#if __ANDROID__
            // create a custom context
            if (bandContextWrapper == null)
            {
                bandContextWrapper = new BandContextWrapper(Application.Context);
            }
            // continue normally
            var nativeClient = NativeBandClientManager.Instance.Create(bandContextWrapper, info.Native);
            var result = await nativeClient.ConnectTaskAsync();
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

#if __ANDROID__
        private static BandContextWrapper bandContextWrapper;

        private class BandContextWrapper : ContextWrapper
        {
            public BandContextWrapper(Context baseContext)
                : base (baseContext)
            {
            }

            // make the intent explicit
            public override bool BindService(Intent service, IServiceConnection conn, Bind flags)
            {
                var action = service.Action;
                if (!string.IsNullOrEmpty(action) && action.StartsWith("com.microsoft.band"))
                {
                    service.SetPackage("com.microsoft.kapp");
                }
                return base.BindService(service, conn, flags);
            }
        }
#endif
    }
}
