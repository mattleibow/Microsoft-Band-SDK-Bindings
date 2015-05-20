using Android.Graphics;

namespace Microsoft.Band
{
    public partial class BandTheme
    {
        public Color Base
        {
            get { return GetColor(GetBaseColor()); }
            set { SetBaseColor(value); }
        }
    
        public Color Muted
        {
            get { return GetColor(GetMutedColor()); }
            set { SetMutedColor(value); }
        }
    
        public Color HighContrast
        {
            get { return GetColor(GetHighContrastColor()); }
            set { SetHighContrastColor(value); }
        }
    
        public Color Highlight
        {
            get { return GetColor(GetHighlightColor()); }
            set { SetHighlightColor(value); }
        }
    
        public Color Lowlight
        {
            get { return GetColor(GetLowlightColor()); }
            set { SetLowlightColor(value); }
        }
    
        public Color SecondaryText
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
