namespace Microsoft.Band.Portable.Sample
{
    public class BaseClientContentPage : BaseContentPage
    {
        protected readonly BandClient BandClient;
        protected readonly BandDeviceInfo BandInfo;

        public BaseClientContentPage(BandDeviceInfo info, BandClient bandClient)
        {
            BandInfo = info;
            BandClient = bandClient;
        }
    }
}
