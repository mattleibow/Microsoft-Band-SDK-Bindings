using Android.Graphics;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class FilledButtonData
    {
        public FilledButtonData(int id, Color color)
            : this(id, (int)color)
        {
        }

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