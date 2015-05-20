namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct PageSize
    {
        public readonly static PageSize Empty = new PageSize();

        public PageSize(short width, short height)
            : this()
        {
            Width = width;
            Height = height;
        }

        public short Width { get; set; }

        public short Height { get; set; }

        public bool IsEmpty
        {
            get { return Width == 0 && Height == 0; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PageSize))
            {
                return false;
            }
            PageSize size = (PageSize)obj;
            return size == this;
        }

        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        public override string ToString()
        {
            return string.Format("[Width={0}, Height={1}]", Width, Height);
        }

        public static bool operator ==(PageSize left, PageSize right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(PageSize left, PageSize right)
        {
            return !(left == right);
        }
    }
}