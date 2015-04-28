#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public abstract class ElementData
    {
        public ElementData()
        {
        }

        public short ElementId { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal ElementData(NativeElementData native)
        {
            ElementId = (short)native.ElementId;
        }

        internal abstract NativeElementData ToNative();
#endif
    }
}