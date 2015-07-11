using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class TextButton
    {
        public Color PressedColor
        {
            get { return new Color(GetPressedColor()); }
            set { SetPressedColor(value); }
        }

        public ElementColorSource PressedColorSource
        {
            get { return GetPressedColorSource(); }
            set { SetPressedColorSource(value); }
        }
    }
}