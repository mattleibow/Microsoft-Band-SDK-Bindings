#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public abstract class ButtonBaseData : ElementData
    {
        public ButtonBaseData()
        {
        }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal ButtonBaseData(NativeElementData native)
            : base(native)
        {
        }
#endif
    }
}