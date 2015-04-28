namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct Point
    {
        public readonly static Point Empty = new Point();

        public Point(short x, short y)
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
            if (!(obj is Point))
            {
                return false;
            }
            Point point = (Point)obj;
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

        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}