namespace Microsoft.Band.Tiles.Pages
{
    public partial class PageElement
    {
        public PageRect Rect
        {
            get { return GetRect(); }
            set { SetBounds(value); }
        }
        public HorizontalAlignment HorizontalAlignment
        {
            get { return GetHorizontalAlignment(); }
            set { SetHorizontalAlignment(value); }
        }
        public int ElementId
        {
            get { return GetId(); }
            set { SetElementId(value); }
        }
        public Margins Margins
        {
            get { return GetMargins(); }
            set { SetMargins(value); }
        }
        public VerticalAlignment VerticalAlignment
        {
            get { return GetVerticalAlignment(); }
            set { SetVerticalAlignment(value); }
        }
        public bool Visible
        {
            get { return IsVisible(); }
            set { SetVisible(value); }
        }
    }
}