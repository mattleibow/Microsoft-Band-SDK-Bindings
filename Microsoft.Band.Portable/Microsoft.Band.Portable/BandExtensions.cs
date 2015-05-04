#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeUserConsent = Microsoft.Band.UserConsent;
#endif

namespace Microsoft.Band.Portable
{
    internal static class BandExtensions
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal static NativeUserConsent ToNative(this UserConsent consent)
        {
            // can't use switch on Android as this is not an enum
            if (consent == UserConsent.Granted)
                return NativeUserConsent.Granted;
            if (consent == UserConsent.Declined)
                return NativeUserConsent.Declined;
            return NativeUserConsent.NotSpecified;
        }

        internal static UserConsent FromNative(this NativeUserConsent consent)
        {
            // can't use switch on Android as this is not an enum
            if (consent == NativeUserConsent.Granted)
                return UserConsent.Granted;
            if (consent == NativeUserConsent.Declined)
                return UserConsent.Declined;
            return UserConsent.Unspecified;
        }
#endif
    }
}
