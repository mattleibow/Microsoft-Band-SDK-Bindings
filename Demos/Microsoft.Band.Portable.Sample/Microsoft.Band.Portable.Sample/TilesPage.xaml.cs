using System;

using Xamarin.Forms;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class TilesPage : BaseClientContentPage
    {
        public TilesPage(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            InitializeComponent();

			var addTile = new ToolbarItem
			{
				Text = "Add",
                Icon = OnPlatform(
                    iOS: (FileImageSource)FileImageSource.FromFile("Icons/Add.png"),
                    Android: (FileImageSource)FileImageSource.FromFile("Add.png"),
                    Windows: (FileImageSource)FileImageSource.FromFile("Assets/Icons/Add.png"))
            };
			addTile.Clicked += AddTileButtonClicked;
			ToolbarItems.Add(addTile);

            ViewModel = new TilesViewModel(info, bandClient);
        }

        private void AddTileButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTilePage(BandInfo, BandClient));
        }

        private void SendNotificationButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var tile = (TileViewModel)button.CommandParameter;
            Navigation.PushAsync(new NotificationsPage(BandInfo, BandClient, tile.Tile));
        }

        private void DetailsButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var tile = (TileViewModel)button.CommandParameter;
            Navigation.PushAsync(new AddTilePage(BandInfo, BandClient, tile.Tile));
        }
    }
}
