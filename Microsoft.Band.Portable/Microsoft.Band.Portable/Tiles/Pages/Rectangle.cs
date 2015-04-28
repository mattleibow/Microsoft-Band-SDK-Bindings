namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct Rectangle
    {
        public readonly static Rectangle Empty = new Rectangle();

        public Rectangle(short x, short y, short width, short height)
            : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public short Y { get; set; }
        public short X { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }

        public Point Location
        {
            get { return new Point(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public bool IsEmpty
        {
            get { return X == 0 && Y == 0 && Width == 0 && Height == 0; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }
            Rectangle Rectangle = (Rectangle)obj;
            return Rectangle == this;
        }

        public override int GetHashCode()
        {
            return X ^ Y ^ Width ^ Height;
        }

        public override string ToString()
        {
            return string.Format("[X={0}, Y={1}, Width={2}, Height={3}]", X, Y, Width, Height);
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }
    }
}