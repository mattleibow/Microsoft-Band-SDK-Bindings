using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class WrappedTextBlock
    {
        public Color Color
        {
            get { return new Color(GetColor()); }
            set { SetColor(value); }
        }

        public ElementColorSource ColorSource
        {
            get { return GetColorSource(); }
            set { SetColorSource(value); }
        }

        public WrappedTextBlockFont Font
        {
            get { return GetFont(); }
            set { SetFont(value); }
        }

        public bool AutoHeight
        {
            get { return IsAutoHeightEnabled(); }
            set { SetAutoHeightEnabled(value); }
        }
    }
}