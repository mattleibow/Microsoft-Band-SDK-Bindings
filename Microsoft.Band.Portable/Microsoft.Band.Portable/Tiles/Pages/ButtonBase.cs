#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    public abstract class ButtonBase : Element
    {
        public ButtonBase()
        {
        }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal ButtonBase(NativeElement native)
            : base(native)
        {
        }
#endif
    }
}