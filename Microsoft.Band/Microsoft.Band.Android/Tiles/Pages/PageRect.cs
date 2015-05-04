namespace Microsoft.Band.Tiles.Pages
{
    public partial class PageRect
    {
        public int X
        {
            get { return GetOriginX(); }
            set { SetOriginX(value); }
        }
        public int Y
        {
            get { return GetOriginY(); }
            set { SetOriginY(value); }
        }
        public int Width
        {
            get { return GetWidth(); }
            set { SetWidth(value); }
        }
        public int Height
        {
            get { return GetHeight(); }
            set { SetHeight(value); }
        }
    }
}