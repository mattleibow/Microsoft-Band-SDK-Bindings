using System.Collections.Generic;

using Microsoft.Band.Tiles.Pages;

namespace Microsoft.Band.Tiles.Pages
{
    public partial class PageRect
    {
        public PageRect(ushort x, ushort y, ushort width, ushort height)
            : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
