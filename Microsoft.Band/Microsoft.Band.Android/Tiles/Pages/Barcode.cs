namespace Microsoft.Band.Tiles.Pages
{
    public partial class Barcode
    {
        public BarcodeType BarcodeType
        {
            get { return GetBarcodeType(); }
            set { SetBarcodeType(value); }
        }
    }
}