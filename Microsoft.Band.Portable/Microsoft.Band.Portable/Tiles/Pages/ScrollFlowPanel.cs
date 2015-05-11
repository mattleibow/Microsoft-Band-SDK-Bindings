#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeScrollFlowPanel = Microsoft.Band.Tiles.Pages.ScrollFlowPanel;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.BandColor;

    public class ScrollFlowPanel : FlowPanel
    {
        private static readonly BandColor DefaultScrollBarColor = BandColor.Empty;
        private static readonly ElementColorSource DefaultScrollBarColorSource = ElementColorSource.Custom;

        public ScrollFlowPanel()
        {
            ScrollBarColor = DefaultScrollBarColor;
            ScrollBarColorSource = DefaultScrollBarColorSource;
        }

        public ScrollFlowPanel(BandColor scrollBarColor)
        {
            ScrollBarColor = scrollBarColor;
            ScrollBarColorSource = DefaultScrollBarColorSource;
        }

        public ScrollFlowPanel(ElementColorSource scrollBarColorSource)
        {
            ScrollBarColor = DefaultScrollBarColor;
            ScrollBarColorSource = scrollBarColorSource;
        }

        public BandColor ScrollBarColor { get; set; }
        public ElementColorSource ScrollBarColorSource { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal ScrollFlowPanel(NativeScrollFlowPanel native)
            : base(native)
        {
            ScrollBarColor = native.ScrollBarColor.FromNative();
            ScrollBarColorSource = native.ScrollBarColorSource.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeScrollFlowPanel>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeScrollFlowPanel(Rect.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeScrollFlowPanel();
#endif
            }
            if (ScrollBarColor != BandColor.Empty)
            {
                native.ScrollBarColor = ScrollBarColor.ToNative();
            }
            native.ScrollBarColorSource = ScrollBarColorSource.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}