using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class Icon
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
    }
}