using System;
using System.Globalization;

namespace Microsoft.Band.Portable
{
    public struct BandColor
    {
        private bool isInitialized;
        private byte r;
        private byte g;
        private byte b;

        public static readonly BandColor Empty = new BandColor();

        public BandColor(byte r, byte g, byte b)
            : this()
        {
            this.isInitialized = true;

            this.r = r;
            this.g = g;
            this.b = b;
        }

        public bool IsEmpty 
        { 
            get { return !isInitialized; } 
        }

        public byte R 
        {
            get { return r; }
            set 
            {
                r = value;
                isInitialized = true;
            }
        }
        
        public byte G 
        {
            get { return g; }
            set 
            {
                g = value;
                isInitialized = true;
            }
        }

        public byte B 
        {
            get { return b; }
            set 
            {
                b = value;
                isInitialized = true;
            }
        }

        public string Hex
        {
            get { return string.Format("#{0:X2}{1:X2}{2:X2}", R, G, B); }
        }

        public static BandColor FromHex(string hex)
        {
            hex = hex.Replace("#", "");

            if (hex.Length != 3 && hex.Length != 6)
            {
                throw new ArgumentException(string.Format("'{0}' is not a valid RGB hex color.", hex), "hex");
            }

            var len = hex.Length == 3 ? 1 : 2;
            var r = byte.Parse(hex.Substring(0, len), NumberStyles.HexNumber);
            var g = byte.Parse(hex.Substring(len, len), NumberStyles.HexNumber);
            var b = byte.Parse(hex.Substring(len + len, len), NumberStyles.HexNumber);

            return new BandColor(r, g, b);
        }

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
                color1.isInitialized == color2.isInitialized && 
                color1.r == color2.r && 
                color1.g == color2.g && 
                color1.b == color2.b;
        }

        public override string ToString ()
        {
			if (isInitialized)
			{
				return string.Format("[R={0}, G={1}, B={2}]", r, g, b);
			}
			else
			{
				return "[Empty]";		
			}
        }
    }
}
