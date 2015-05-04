using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class TextBlock
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

        public TextBlockFont Font
        {
            get { return GetFont(); }
            set { SetFont(value); }
        }

        public int Baseline
        {
            get { return GetBaseline(); }
            set { SetBaseline(value); }
        }

        public TextBlockBaselineAlignment BaselineAlignment
        {
            get { return GetBaselineAlignment(); }
            set { SetBaselineAlignment(value); }
        }

        public bool AutoWidth
        {
            get { return IsAutoWidthEnabled(); }
            set { SetAutoWidthEnabled(value); }
        }
    }
}