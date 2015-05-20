namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct Margins
    {
        public static Margins Empty = new Margins();

        public Margins(short all)
            : this()
        {
            Left = all;
            Top = all;
            Right = all;
            Bottom = all;
        }

        public Margins(short horizontal, short vertical)
            : this()
        {
            Left = horizontal;
            Top = vertical;
            Right = horizontal;
            Bottom = vertical;
        }

        public Margins(short left, short top, short right, short bottom)
            : this()
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public short Left { get; set; }
        public short Top { get; set; }
        public short Right { get; set; }
        public short Bottom { get; set; }

        public int Horizontal
        {
            get { return Left + Right; }
        }

        public int Vertical
        {
            get { return Top + Bottom; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Margins))
            {
                return false;
            }
            Margins margins = (Margins)obj;
            return margins == this;
        }

        public override int GetHashCode()
        {
            return Left ^ Top ^ Right ^ Bottom;
        }

        public override string ToString()
        {
            return string.Format("[Left={0}, Top={1}, Right={2}, Bottom={3}]", Left, Top, Right, Bottom);
        }

        public static bool operator ==(Margins left, Margins right)
        {
            return left.Left == right.Left && left.Top == right.Top && left.Right == right.Right && left.Bottom == right.Bottom;
        }

        public static bool operator !=(Margins left, Margins right)
        {
            return !(left == right);
        }
    }
}