using System;

using Microsoft.Band.Portable.Tiles;

using Xamarin.Forms;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class AddTilePage : BaseClientContentPage
    {
		private AddTileViewModel addTileViewModel;

        public AddTilePage(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            InitializeComponent();

			addTileViewModel = new AddTileViewModel(info, bandClient);

			Init();

			ViewModel = addTileViewModel;
        }

        public AddTilePage(BandDeviceInfo info, BandClient bandClient, BandTile tile)
            : base(info, bandClient)
        {
            InitializeComponent();

			addTileViewModel = new AddTileViewModel(info, bandClient, tile);

			Init();

			ViewModel = addTileViewModel;
        }

		private void Init()
		{
			var addTile = new ToolbarItem
			{
				Text = "Add",
                Icon = OnPlatform(
                    iOS: (FileImageSource)FileImageSource.FromFile("Icons/Done.png"),
                    Android: (FileImageSource)FileImageSource.FromFile("Done.png"),
                    Windows: (FileImageSource)FileImageSource.FromFile("Assets/Icons/Done.png"))
            };
			addTile.SetBinding(ToolbarItem.CommandProperty, new Binding("AddTileCommand"));
			ToolbarItems.Add(addTile);

			var removeTile = new ToolbarItem
			{
				Text = "Remove",
                Icon = OnPlatform(
                    iOS: (FileImageSource)FileImageSource.FromFile("Icons/Remove.png"),
                    Android: (FileImageSource)FileImageSource.FromFile("Remove.png"),
                    Windows: (FileImageSource)FileImageSource.FromFile("Assets/Icons/Remove.png"))
            };
			removeTile.SetBinding(ToolbarItem.CommandProperty, new Binding("RemoveTileCommand"));
			ToolbarItems.Add(removeTile);
		}

		public async void ChangeThemeButtonClicked(object sender, EventArgs e)
		{
			var picker = new ThemePickerPage { Theme = addTileViewModel.TileTheme };
			picker.Picked += delegate { addTileViewModel.TileTheme = picker.Theme; };

			await Navigation.PushAsync(picker);
		}
    }
}
