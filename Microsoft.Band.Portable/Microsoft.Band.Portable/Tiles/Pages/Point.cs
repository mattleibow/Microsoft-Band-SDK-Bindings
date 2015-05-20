namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct PagePoint
    {
        public readonly static PagePoint Empty = new PagePoint();

        public PagePoint(short x, short y)
            : this()
        {
            X = x;
            Y = y;
        }

        public short X { get; set; }

        public short Y { get; set; }

        public bool IsEmpty
        {
            get { return X == 0 && Y == 0; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PagePoint))
            {
                return false;
            }
            PagePoint point = (PagePoint)obj;
            return point == this;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            return string.Format("[X={0}, Y={1}]", X, Y);
        }

        public static bool operator ==(PagePoint left, PagePoint right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(PagePoint left, PagePoint right)
        {
            return !(left == right);
        }
    }
}