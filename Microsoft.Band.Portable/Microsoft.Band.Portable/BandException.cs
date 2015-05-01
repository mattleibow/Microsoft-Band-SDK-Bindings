using System;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(Microsoft.Band.BandException))]
#else
namespace Microsoft.Band
{
    public sealed class BandException : Exception
    {
        private BandException()
        {
        }
    }
}
#endif
