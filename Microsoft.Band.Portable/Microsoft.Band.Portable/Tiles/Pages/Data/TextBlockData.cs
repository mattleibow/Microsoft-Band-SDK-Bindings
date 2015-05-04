#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeTextBlockData = Microsoft.Band.Tiles.Pages.TextBlockData;
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public class TextBlockData : TextBlockBaseData
    {
        public TextBlockData()
        {
        }

        public override string Text { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal TextBlockData(NativeTextBlockData native)
            : base(native)
        {
        }

        internal override NativeElementData ToNative()
        {
            NativeTextBlockData native = null;
#if __ANDROID__
            native = new NativeTextBlockData(ElementId, Text);
#elif __IOS__
            Foundation.NSError error;
            native = NativeTextBlockData.Create((ushort)ElementId, Text, out error);
#elif WINDOWS_PHONE_APP
            native = new NativeTextBlockData(ElementId, Text);
#endif
            return native;
        }
#endif
    }
}
