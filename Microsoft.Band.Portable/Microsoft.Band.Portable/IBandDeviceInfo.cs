
#if __ANDROID__
using NativeBandDeviceInfo = Microsoft.Band.IBandInfo;
#elif __IOS__
using NativeBandDeviceInfo = Microsoft.Band.BandClient;
#elif WINDOWS_PHONE_APP
using NativeBandDeviceInfo = Microsoft.Band.IBandInfo;
#endif

namespace Microsoft.Band.Portable
{
    public class BandDeviceInfo
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandDeviceInfo Native;
         
        internal BandDeviceInfo(NativeBandDeviceInfo info)
        {
            this.Native = info;
        }
#endif

        public string Name
        {
            get
            {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
                return Native.Name; 
#else // PORTABLE
                return null;
#endif
            }
        }
    }
}
