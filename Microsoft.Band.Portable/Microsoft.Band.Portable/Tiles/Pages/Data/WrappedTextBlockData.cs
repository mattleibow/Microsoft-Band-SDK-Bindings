#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeWrappedTextBlockData = Microsoft.Band.Tiles.Pages.WrappedTextBlockData;
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public class WrappedTextBlockData : TextBlockBaseData
    {
        public WrappedTextBlockData()
        {
        }

        public override string Text { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal WrappedTextBlockData(NativeWrappedTextBlockData native)
            : base(native)
        {
        }

        internal override NativeElementData ToNative()
        {
            NativeWrappedTextBlockData native = null;
#if __ANDROID__
            native = new NativeWrappedTextBlockData(ElementId, Text);
#elif __IOS__
            Foundation.NSError error;
            native = NativeWrappedTextBlockData.Create((ushort)ElementId, Text, out error);
#elif WINDOWS_PHONE_APP
            native = new NativeWrappedTextBlockData(ElementId, Text);
#endif
            return native;
        }
#endif
    }
}