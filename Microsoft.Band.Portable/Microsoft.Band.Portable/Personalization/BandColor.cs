namespace Microsoft.Band.Portable.Personalization
{
    public struct BandColor
    {
        public BandColor(byte r, byte g, byte b)
			: this()
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public static bool operator ==(BandColor color1, BandColor color2)
        {
            return color1.Equals(color2);
        }

        public static bool operator !=(BandColor color1, BandColor color2)
        {
            return !color1.Equals(color2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BandColor))
            {
                return this.Equals(obj);
            }

            BandColor color1 = this;
            BandColor color2 = (BandColor)obj;

            return 
                color1.R == color2.R && 
                color1.G == color2.G && 
                color1.B == color2.B;
        }
    }
}
