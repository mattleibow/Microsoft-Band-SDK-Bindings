using System;

using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
	public partial class ThemePickerPage : BasePickerPage
	{
		private ThemePickerViewModel themePickerViewModel;

		public ThemePickerPage()
		{
			InitializeComponent();

			themePickerViewModel = new ThemePickerViewModel();
			ViewModel = themePickerViewModel;
		}

		public BandTheme Theme
		{
			get { return themePickerViewModel.Theme; }
			set { themePickerViewModel.Theme = value; }
		}

		public async void ChangeColorButtonClicked(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var themeColor = (BandThemeColorViewModel)button.CommandParameter;

			var picker = new ColorPickerPage { Color = themeColor.Color };
			picker.Picked += delegate { themeColor.Color = picker.Color; };

			await Navigation.PushAsync(picker);
		}
	}
}

