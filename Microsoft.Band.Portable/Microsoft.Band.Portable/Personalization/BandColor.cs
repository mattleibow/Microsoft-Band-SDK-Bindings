namespace Microsoft.Band.Portable.Personalization
{
    public struct BandColor
    {
        public BandColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }
    }
}
