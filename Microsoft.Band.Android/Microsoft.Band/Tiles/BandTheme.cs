using Android.Graphics;

namespace Microsoft.Band.Tiles
{
    public partial class BandTheme
    {
        public Color BaseColor
        {
            get { return GetColor(GetBaseColor()); }
            set { SetBaseColor(value); }
        }
    
        public Color MutedColor
        {
            get { return GetColor(GetMutedColor()); }
            set { SetMutedColor(value); }
        }
    
        public Color HighContrastColor
        {
            get { return GetColor(GetHighContrastColor()); }
            set { SetHighContrastColor(value); }
        }
    
        public Color HighlightColor
        {
            get { return GetColor(GetHighlightColor()); }
            set { SetHighlightColor(value); }
        }
    
        public Color LowlightColor
        {
            get { return GetColor(GetLowlightColor()); }
            set { SetLowlightColor(value); }
        }
    
        public Color SecondaryTextColor
        {
            get { return GetColor(GetSecondaryTextColor()); }
            set { SetSecondaryTextColor(value); }
        }

        private static Color GetColor(int color)
        {
            return Color.Rgb(
                Color.GetRedComponent(color),
                Color.GetGreenComponent(color), 
                Color.GetBlueComponent(color));
        }
    }
}
