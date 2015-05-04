using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Bands = new ObservableCollection<BandDeviceInfo>();

            LoadBandsCommand = new Command(async () => await LoadBands());
        }

        public ICommand LoadBandsCommand { get; private set; }

        public ObservableCollection<BandDeviceInfo> Bands { get; private set; }
        
        public override async Task Prepare()
        {
            await base.Prepare();

            // refresh the list
            LoadBandsCommand.Execute(null);
        }
        
        private async Task LoadBands()
        {
            Bands.Clear();
            IEnumerable<BandDeviceInfo> bands = await BandClientManager.Instance.GetPairedBandsAsync();
            foreach (var band in bands)
            {
                Bands.Add(band);
            }
        }
    }
}
