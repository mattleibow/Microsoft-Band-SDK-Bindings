using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class ClientViewModel : BaseClientViewModel
    {
        private bool isConnecting;

        public ClientViewModel(BandDeviceInfo info)
            : base(info, null)
        {
        }
        
        public string FirmwareVersion { get; private set; }

        public string HardwareVersion { get; private set; }

        public override async Task Prepare()
        {
            if (BandClient == null)
            {
                BandClient = await BandClientManager.Instance.ConnectAsync(BandInfo);
            }

            await base.Prepare();

            FirmwareVersion = await BandClient.GetFirmwareVersionAsync();
            OnPropertyChanged("FirmwareVersion");

            HardwareVersion = await BandClient.GetHardwareVersionAsync();
            OnPropertyChanged("HardwareVersion");
        }

        public override async Task Destroy()
        {
            await base.Destroy();

            await BandClient.DisconnectAsync();
        }
    }
}
