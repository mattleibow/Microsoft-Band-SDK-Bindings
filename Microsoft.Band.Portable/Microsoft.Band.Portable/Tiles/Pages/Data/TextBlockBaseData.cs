#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public abstract class TextBlockBaseData : ElementData
    {
        public TextBlockBaseData()
        {
        }

        public abstract string Text { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal TextBlockBaseData(NativeElementData native)
            : base(native)
        {
        }
#endif
    }
}
