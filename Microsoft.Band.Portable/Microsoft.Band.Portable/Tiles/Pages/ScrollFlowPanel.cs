#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeScrollFlowPanel = Microsoft.Band.Tiles.Pages.ScrollFlowPanel;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.Personalization.BandColor;

    public class ScrollFlowPanel : FlowPanel
    {
        public ScrollFlowPanel()
        {
        }

        public BandColor ScrollbarColor { get; set; }
        public ElementColorSource ScrollbarColorSource { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal ScrollFlowPanel(NativeScrollFlowPanel native)
            : base(native)
        {
            ScrollbarColor = native.ScrollBarColor.FromNative();
            ScrollbarColorSource = native.ScrollBarColorSource.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeScrollFlowPanel>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeScrollFlowPanel(Rectangle.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeScrollFlowPanel();
#endif
            }
            native.ScrollBarColor = ScrollbarColor.ToNative();
            native.ScrollBarColorSource = ScrollbarColorSource.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}