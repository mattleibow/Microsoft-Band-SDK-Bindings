using System.Collections.ObjectModel;

using Microsoft.Band.Portable.Personalization;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
	public class ThemePickerViewModel : BaseViewModel
    {
		private BandTheme theme;

		public ThemePickerViewModel()
		{
			ThemeItems = new ObservableCollection<BandThemeColorViewModel>();
		}

		public ObservableCollection<BandThemeColorViewModel> ThemeItems { get; private set; }

		public BandTheme Theme
        {
            get { return theme; }
            set
            {
                if (theme != value)
                {
                    theme = value;
					UpdateThemeItems();
                    OnPropertyChanged("Theme");
                }
            }
        }

		private void UpdateThemeItems()
		{
			ThemeItems.Clear();
			ThemeItems.Add(new BandThemeColorViewModel("Base", () => theme.Base, color => theme.Base = color));
			ThemeItems.Add(new BandThemeColorViewModel("High Contrast", () => theme.HighContrast, color => theme.HighContrast = color));
			ThemeItems.Add(new BandThemeColorViewModel("Highlight", () => theme.Highlight, color => theme.Highlight = color));
			ThemeItems.Add(new BandThemeColorViewModel("Lowlight", () => theme.Lowlight, color => theme.Lowlight = color));
			ThemeItems.Add(new BandThemeColorViewModel("Muted", () => theme.Muted, color => theme.Muted = color));
			ThemeItems.Add(new BandThemeColorViewModel("Secondary Text", () => theme.SecondaryText, color => theme.SecondaryText = color));
		}
    }
}
