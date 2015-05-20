#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeFilledPanel = Microsoft.Band.Tiles.Pages.FilledPanel;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.BandColor;

    public class FilledPanel : Panel
    {
        private static readonly BandColor DefaultBackgroundColor = BandColor.Empty;
        private static readonly ElementColorSource DefaultBackgroundColorSource = ElementColorSource.Custom;

        public FilledPanel()
        {
            BackgroundColor = DefaultBackgroundColor;
            BackgroundColorSource = DefaultBackgroundColorSource;
        }

        public FilledPanel(BandColor backgroundColor)
        {
            BackgroundColor = backgroundColor;
            BackgroundColorSource = DefaultBackgroundColorSource;
        }

        public FilledPanel(ElementColorSource backgroundColorSource)
        {
            BackgroundColor = DefaultBackgroundColor;
            BackgroundColorSource = backgroundColorSource;
        }

        public BandColor BackgroundColor { get; set; }
        public ElementColorSource BackgroundColorSource { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal FilledPanel(NativeFilledPanel native)
            : base(native)
        {
            BackgroundColor = native.BackgroundColor.FromNative();
            BackgroundColorSource = native.BackgroundColorSource.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeFilledPanel>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeFilledPanel(Rect.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeFilledPanel();
#endif
            }
            if (BackgroundColor != BandColor.Empty)
            {
                native.BackgroundColor = BackgroundColor.ToNative();
            }
            native.BackgroundColorSource = BackgroundColorSource.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}