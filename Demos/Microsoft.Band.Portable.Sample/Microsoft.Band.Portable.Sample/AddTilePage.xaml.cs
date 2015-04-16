using System;

using Microsoft.Band.Portable.Tiles;

using Xamarin.Forms;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class AddTilePage : BaseClientContentPage
    {
        public AddTilePage(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            InitializeComponent();

            ViewModel = new AddTileViewModel(info, bandClient);
        }

        public AddTilePage(BandDeviceInfo info, BandClient bandClient, BandTile tile)
            : base(info, bandClient)
        {
            InitializeComponent();

            ViewModel = new AddTileViewModel(info, bandClient, tile);
        }

        public async void ChangeColorButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var themeColor = (BandThemeColorViewModel)button.CommandParameter;

            var picker = new ColorPickerPage { Color = themeColor.Color };
            picker.Picked += delegate { themeColor.Color = picker.Color; };

            await Navigation.PushModalAsync(picker);
        }
    }
}
