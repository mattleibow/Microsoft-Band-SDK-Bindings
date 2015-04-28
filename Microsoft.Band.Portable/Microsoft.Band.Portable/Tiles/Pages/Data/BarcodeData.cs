#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeBarcodeData = Microsoft.Band.Tiles.Pages.BarcodeData;
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public class BarcodeData : ElementData
    {
        public BarcodeData()
        {
        }

        public BarcodeType BarcodeType { get; set; }
        public string BarcodeValue { get; set; }
        // todo max data length ?

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal BarcodeData(NativeBarcodeData native)
            : base(native)
        {
            BarcodeType = native.BarcodeType.FromNative();
            BarcodeValue = native.Barcode;
        }

        internal override NativeElementData ToNative()
        {
            NativeBarcodeData native = null;
#if __ANDROID__
            native = new NativeBarcodeData(ElementId, BarcodeValue, BarcodeType.ToNative());
#elif __IOS__
            Foundation.NSError error = null;
            native = NativeBarcodeData.Create((ushort)ElementId, BarcodeType.ToNative(), BarcodeValue, out error);
#elif WINDOWS_PHONE_APP
            native = new NativeBarcodeData(BarcodeType.ToNative(), ElementId, BarcodeValue);
#endif
            return native;
        }
#endif
    }
}