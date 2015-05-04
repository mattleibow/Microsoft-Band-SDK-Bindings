#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeImage = Microsoft.Band.Tiles.Pages.Icon;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.Personalization.BandColor;

    public class Image : Element
    {
        public Image()
        {
            Color = BandColor.Empty;
            ColorSource = ElementColorSource.Custom;
        }

        public BandColor Color { get; set; }
        public ElementColorSource ColorSource { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal Image(NativeImage native)
            : base(native)
        {
            Color = native.Color.FromNative();
            ColorSource = native.ColorSource.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeImage>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeImage(Rectangle.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeImage();
#endif
            }
            if (Color != BandColor.Empty)
            {
                native.Color = Color.ToNative();
            }
            native.ColorSource = ColorSource.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}