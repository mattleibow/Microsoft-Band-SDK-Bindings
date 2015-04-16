using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public abstract class BaseClientViewModel : BaseViewModel
    {
        public BaseClientViewModel(BandDeviceInfo info, BandClient bandClient)
        {
            BandClient = bandClient;
            BandInfo = info;
        }

        public override async Task Prepare()
        {
            await base.Prepare();

            BandName = BandInfo.Name;
            IsConnected = BandClient.IsConnected;
        }

        public BandDeviceInfo BandInfo { get; protected set; }

        public BandClient BandClient { get; protected set; }

        public string BandName { get; private set; }

        public bool IsConnected { get; private set; }
    }
}
