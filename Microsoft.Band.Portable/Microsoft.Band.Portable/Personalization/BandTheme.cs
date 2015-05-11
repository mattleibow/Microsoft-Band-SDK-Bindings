namespace Microsoft.Band.Portable
{
    public struct BandTheme
    {
        public static readonly BandTheme Empty = new BandTheme();

        public BandTheme(BandColor baseColor, BandColor highContrastColor, BandColor highlightColor, BandColor lowlightColor, BandColor mutedColor, BandColor secondaryTextColor)
            : this()
        {
            Base = baseColor;
            HighContrast = highContrastColor;
            Highlight = highlightColor;
            Lowlight = lowlightColor;
            Muted = mutedColor;
            SecondaryText = secondaryTextColor;
        }

        public bool IsEmpty 
        {
            get 
            { 
                return 
                    Base.IsEmpty && 
                    HighContrast.IsEmpty && 
                    Highlight.IsEmpty && 
                    Lowlight.IsEmpty && 
                    Muted.IsEmpty && 
                    SecondaryText.IsEmpty; 
            }    
        }

        public BandColor Base { get; set; }
        public BandColor HighContrast { get; set; }
        public BandColor Highlight { get; set; }
        public BandColor Lowlight { get; set; }
        public BandColor Muted { get; set; }
        public BandColor SecondaryText { get; set; }

        public static bool operator ==(BandTheme theme1, BandTheme theme2)
        {
            return theme1.Equals(theme2);
        }

        public static bool operator !=(BandTheme theme1, BandTheme theme2)
        {
            return !theme1.Equals(theme2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BandTheme))
            {
                return this.Equals(obj);
            }

            BandTheme theme1 = this;
            BandTheme theme2 = (BandTheme)obj;

            return 
                theme1.Base == theme2.Base && 
                theme1.HighContrast == theme2.HighContrast && 
                theme1.Highlight == theme2.Highlight && 
                theme1.Lowlight == theme2.Lowlight && 
                theme1.Muted == theme2.Muted && 
                theme1.SecondaryText == theme2.SecondaryText;
        }

        //public static BandTheme CORN_FLOWER_THEME = new BandTheme(3368652, 3832029, 3237306, 9017259, 3832029, 1590908);
        //public static BandTheme CYBER_THEME = new BandTheme(3784559, 4312698, 3517029, 10000790, 3662460, 3241807);
        //public static BandTheme JOULE_THEME = new BandTheme(16756480, 16756480, 16357891, 10197656, 16759808, 10708992);
        //public static BandTheme ORCHARD_THEME = new BandTheme(9930671, 11313089, 8287886, 9868953, 12035539, 174612850);
        //public static BandTheme STORM_THEME = new BandTheme(1381653, 3922687, 1118481, 8027260, 3158064, 34469);
        //public static BandTheme TUXEDO_THEME = new BandTheme(1381653, 12040119, 1118481, 8027260, 3158064, 4539717);
        //public static BandTheme PENGUIN_THEME = new BandTheme(1381653, 16756480, 1118481, 8027260, 3158064, 10708992);
    }
}
