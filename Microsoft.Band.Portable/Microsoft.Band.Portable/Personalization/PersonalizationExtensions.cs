using Microsoft.Band.Portable.Personalization;

#if __ANDROID__
using NativeBandTheme = Microsoft.Band.Tiles.BandTheme;
using NativeBandColor = Android.Graphics.Color;
#elif __IOS__
using NativeBandTheme = Microsoft.Band.Personalization.BandTheme;
using NativeBandColor = Microsoft.Band.Personalization.BandColor;
#elif WINDOWS_PHONE_APP
using NativeBandTheme = Microsoft.Band.Personalization.BandTheme;
using NativeBandColor = Microsoft.Band.Personalization.BandColor;
#endif

namespace Microsoft.Band.Portable
{
    internal static class PersonalizationExtensions
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        public static NativeBandTheme ToNative(this BandTheme theme)
        {
            return new NativeBandTheme
            {
#if __ANDROID__
                BaseColor = theme.Base.ToNative(),
                HighContrastColor = theme.HighContrast.ToNative(),
                HighlightColor = theme.Highlight.ToNative(),
                LowlightColor = theme.Lowlight.ToNative(),
                MutedColor = theme.Muted.ToNative(),
                SecondaryTextColor = theme.SecondaryText.ToNative()
#elif __IOS__ || WINDOWS_PHONE_APP
                Base = theme.Base.ToNative(),
                HighContrast = theme.HighContrast.ToNative(),
                Highlight = theme.Highlight.ToNative(),
                Lowlight = theme.Lowlight.ToNative(),
                Muted = theme.Muted.ToNative(),
                SecondaryText = theme.SecondaryText.ToNative()
#endif
            };
        }

        public static BandTheme FromNative(this NativeBandTheme theme)
        {
            return new BandTheme {
#if __ANDROID__
                Base = theme.BaseColor.FromNative(),
                HighContrast = theme.HighContrastColor.FromNative(),
                Highlight = theme.HighlightColor.FromNative(),
                Lowlight = theme.LowlightColor.FromNative(),
                Muted = theme.MutedColor.FromNative(),
                SecondaryText = theme.SecondaryTextColor.FromNative(),
#elif __IOS__ || WINDOWS_PHONE_APP
                Base = theme.Base.FromNative(),
                HighContrast = theme.HighContrast.FromNative(),
                Highlight = theme.Highlight.FromNative(),
                Lowlight = theme.Lowlight.FromNative(),
                Muted = theme.Muted.FromNative(),
                SecondaryText = theme.SecondaryText.FromNative(),
#endif
            };
        }

        public static NativeBandColor ToNative(this BandColor color)
        {
#if __ANDROID__ || WINDOWS_PHONE_APP
            return new NativeBandColor(color.R, color.G, color.B);
#elif __IOS__
            return NativeBandColor.FromRgb(color.R, color.G, color.B);
#endif
        }

        public static BandColor FromNative(this NativeBandColor color)
        {
#if __ANDROID__ || WINDOWS_PHONE_APP
            return new BandColor {
                R = color.R,
                G = color.G,
                B = color.B
            };
#elif __IOS__
            System.nfloat red, green, blue, alpha;
            color.UIColor.GetRGBA(out red, out green, out blue, out alpha);
            return new BandColor {
                R = (byte)(red*255),
                G = (byte)(green*255),
                B = (byte)(blue*255)
            };
#endif
        }

#endif
    }
}
