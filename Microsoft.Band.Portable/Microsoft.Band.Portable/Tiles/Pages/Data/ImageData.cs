#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeImageData = Microsoft.Band.Tiles.Pages.IconData;
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public class ImageData : ElementData
    {
        public ImageData()
        {
        }

        public ushort ImageIndex { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal ImageData(NativeImageData native)
            : base(native)
        {
            ImageIndex = (ushort)native.IconIndex;
        }

        internal override NativeElementData ToNative()
        {
            NativeImageData native = null;
#if __ANDROID__
            native = new NativeImageData(ElementId, ImageIndex);
#elif __IOS__
            Foundation.NSError error;
            native = NativeImageData.Create((ushort)ElementId, ImageIndex, out error);
#elif WINDOWS_PHONE_APP
            native = new NativeImageData(ElementId, ImageIndex);
#endif
            return native;
        }
#endif
    }
}