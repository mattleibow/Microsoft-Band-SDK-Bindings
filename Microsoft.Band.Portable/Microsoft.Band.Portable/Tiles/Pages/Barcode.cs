#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeBarcode = Microsoft.Band.Tiles.Pages.Barcode;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    public class Barcode : Element
    {
        public Barcode()
        {
        }

        public BarcodeType BarcodeType { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal Barcode(NativeBarcode native)
            : base(native)
        {
            BarcodeType = native.BarcodeType.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeBarcode>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeBarcode(Rectangle.ToNative(), BarcodeType.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeBarcode(BarcodeType.ToNative());
#endif
            }
            return base.ToNative(native);
        }
#endif
    }
}