namespace Microsoft.Band.Portable.Tiles.Pages
{
    public struct Size
    {
        public readonly static Size Empty = new Size();

        public Size(short width, short height)
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
            if (!(obj is Size))
            {
                return false;
            }
            Size size = (Size)obj;
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

        public static bool operator ==(Size left, Size right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }
    }
}