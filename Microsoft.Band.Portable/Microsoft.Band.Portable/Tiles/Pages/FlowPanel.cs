#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeFlowPanel = Microsoft.Band.Tiles.Pages.FlowPanel;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    public class FlowPanel : Panel
    {
        private static readonly FlowPanelOrientation DefaultOrientation = FlowPanelOrientation.Vertical;

        public FlowPanel()
        {
            Orientation = DefaultOrientation;
        }

        public FlowPanel(FlowPanelOrientation orientation)
        {
            Orientation = orientation;
        }

        public FlowPanelOrientation Orientation { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal FlowPanel(NativeFlowPanel native)
            : base(native)
        {
            Orientation = native.Orientation.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeFlowPanel>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeFlowPanel(Rect.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeFlowPanel();
#endif
            }
            native.Orientation = Orientation.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}