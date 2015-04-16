using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class SensorsPage : BaseClientContentPage
    {
        public SensorsPage(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            InitializeComponent();
            
            ViewModel = new SensorsViewModel(BandInfo, BandClient);
        }
    }
}
