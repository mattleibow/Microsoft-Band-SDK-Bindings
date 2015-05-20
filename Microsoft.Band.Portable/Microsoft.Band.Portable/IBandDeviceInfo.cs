
#if __ANDROID__
using NativeBandDeviceInfo = Microsoft.Band.IBandInfo;
#elif __IOS__
using NativeBandDeviceInfo = Microsoft.Band.BandClient;
#elif WINDOWS_PHONE_APP
using NativeBandDeviceInfo = Microsoft.Band.IBandInfo;
#endif

namespace Microsoft.Band.Portable
{
    /// <summary>
    /// Represents a paired device.
    /// </summary>
    public class BandDeviceInfo
    {
        private BandDeviceInfo()
        {
        }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandDeviceInfo Native;
         
        internal BandDeviceInfo(NativeBandDeviceInfo info)
        {
            this.Native = info;
        }
#endif

        /// <summary>
        /// Gets the name of the Band device this instance represents.
        /// </summary>
        /// <value>
        /// The name of the Band device.
        /// </value>
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
