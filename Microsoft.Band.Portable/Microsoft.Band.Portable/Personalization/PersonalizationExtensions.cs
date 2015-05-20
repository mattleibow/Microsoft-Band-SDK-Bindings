#if __ANDROID__
using NativeBandTheme = Microsoft.Band.BandTheme;
using NativeBandColor = Android.Graphics.Color;
#elif __IOS__
using NativeBandTheme = Microsoft.Band.BandTheme;
using NativeBandColor = Microsoft.Band.BandColor;
#elif WINDOWS_PHONE_APP
using NativeBandTheme = Microsoft.Band.BandTheme;
using NativeBandColor = Microsoft.Band.BandColor;
#endif
using PortableBandTheme = Microsoft.Band.Portable.BandTheme;
using PortableBandColor = Microsoft.Band.Portable.BandColor;

namespace Microsoft.Band.Portable
{
    internal static class PersonalizationExtensions
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        public static NativeBandTheme ToNative(this BandTheme theme)
        {
            return new NativeBandTheme
            {
                Base = theme.Base.ToNative(),
                HighContrast = theme.HighContrast.ToNative(),
                Highlight = theme.Highlight.ToNative(),
                Lowlight = theme.Lowlight.ToNative(),
                Muted = theme.Muted.ToNative(),
                SecondaryText = theme.SecondaryText.ToNative()
            };
        }

        public static PortableBandTheme FromNative(this NativeBandTheme theme)
        {
            return new PortableBandTheme {
                Base = theme.Base.FromNative(),
                HighContrast = theme.HighContrast.FromNative(),
                Highlight = theme.Highlight.FromNative(),
                Lowlight = theme.Lowlight.FromNative(),
                Muted = theme.Muted.FromNative(),
                SecondaryText = theme.SecondaryText.FromNative(),
            };
        }

        public static NativeBandColor ToNative(this PortableBandColor color)
        {
#if __ANDROID__ || WINDOWS_PHONE_APP
            return new NativeBandColor(color.R, color.G, color.B);
#elif __IOS__
            return NativeBandColor.FromRgb(color.R, color.G, color.B);
#endif
        }

        public static PortableBandColor FromNative(this NativeBandColor color)
        {
#if __ANDROID__ || WINDOWS_PHONE_APP
            return new PortableBandColor
            {
                R = color.R,
                G = color.G,
                B = color.B
            };
#elif __IOS__
            System.nfloat red, green, blue, alpha;
            color.UIColor.GetRGBA(out red, out green, out blue, out alpha);
            return new PortableBandColor {
                R = (byte)(red*255),
                G = (byte)(green*255),
                B = (byte)(blue*255)
            };
#endif
        }

#endif
    }
}
