namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct PageRect
    {
        public readonly static PageRect Empty = new PageRect();

        public PageRect(PagePoint location, PageSize size)
            : this()
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public PageRect(short x, short y, short width, short height)
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

        public PagePoint Location
        {
            get { return new PagePoint(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public PageSize Size
        {
            get { return new PageSize(Width, Height); }
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
            if (!(obj is PageRect))
            {
                return false;
            }
            PageRect Rectangle = (PageRect)obj;
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

        public static bool operator ==(PageRect left, PageRect right)
        {
            return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(PageRect left, PageRect right)
        {
            return !(left == right);
        }
    }
}