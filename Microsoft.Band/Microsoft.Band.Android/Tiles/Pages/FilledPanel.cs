using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class FilledPanel
    {
        public Color BackgroundColor
        {
            get { return new Color(GetBackgroundColor()); }
            set { SetBackgroundColor(value); }
        }

        public ElementColorSource BackgroundColorSource
        {
            get { return GetBackgroundColorSource(); }
            set { SetBackgroundColorSource(value); }
        }
    }
}