using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class ScrollFlowPanel
    {
        public Color ScrollBarColor
        {
            get { return new Color(GetScrollBarColor()); }
            set { SetScrollBarColor(value); }
        }

        public ElementColorSource ScrollBarColorSource
        {
            get { return GetScrollBarColorSource(); }
            set { SetScrollBarColorSource(value); }
        }
    }
}