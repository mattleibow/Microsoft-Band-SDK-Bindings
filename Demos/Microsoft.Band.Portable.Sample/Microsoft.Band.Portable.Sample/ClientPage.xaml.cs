using System;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class ClientPage : BaseContentPage
    {
        private ClientViewModel clientViewModel;

        public ClientPage(BandDeviceInfo info)
        {
            InitializeComponent();

            clientViewModel = new ClientViewModel(info);

            ViewModel = clientViewModel;
        }

        private void SensorsButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SensorsPage(clientViewModel.BandInfo, clientViewModel.BandClient));
        }

        private void TilesButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TilesPage(clientViewModel.BandInfo, clientViewModel.BandClient));
		}

		private void VibrationsButtonClick(object sender, EventArgs e)
		{
			Navigation.PushAsync(new VibrationsPage(clientViewModel.BandInfo, clientViewModel.BandClient));
		}

        private void PersonalizationButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PersonalizationPage(clientViewModel.BandInfo, clientViewModel.BandClient));
        }
    }
}
