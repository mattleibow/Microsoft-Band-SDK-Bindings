#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeFilledButton = Microsoft.Band.Tiles.Pages.FilledButton;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.Personalization.BandColor;

    public class FilledButton : ButtonBase
    {
        public FilledButton()
        {
            BackgroundColor = BandColor.Empty;
        }

        public BandColor BackgroundColor { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal FilledButton(NativeFilledButton native)
            : base(native)
        {
            BackgroundColor = native.BackgroundColor.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeFilledButton>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeFilledButton(Rectangle.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeFilledButton();
#endif
            }
            if (BackgroundColor != BandColor.Empty)
            {
                native.BackgroundColor = BackgroundColor.ToNative();
            }
            return base.ToNative(native);
        }
#endif
    }
}