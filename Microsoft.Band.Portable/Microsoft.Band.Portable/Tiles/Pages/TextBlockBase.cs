#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.BandColor;

    public abstract class TextBlockBase : Element
    {
        public TextBlockBase()
        {
        }

        public abstract BandColor Color { get; set; }
        public abstract ElementColorSource ColorSource { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal TextBlockBase(NativeElement native)
            : base(native)
        {
        }
#endif
    }
}