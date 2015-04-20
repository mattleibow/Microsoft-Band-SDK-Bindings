using Microsoft.Band.Portable.Tiles;

using Xamarin.Forms;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class NotificationsPage : BaseClientContentPage
    {
        public NotificationsPage(BandDeviceInfo info, BandClient bandClient, BandTile tile)
            : base(info, bandClient)
        {
            InitializeComponent();

            ViewModel = new NotificationsViewModel(info, bandClient, tile);
        }
    }
}
