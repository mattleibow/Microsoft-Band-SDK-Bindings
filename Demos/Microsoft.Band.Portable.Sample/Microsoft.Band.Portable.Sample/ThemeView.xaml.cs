using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Sample.ValueConverters;

namespace Microsoft.Band.Portable.Sample
{
	public partial class ThemeView : ContentView
	{
		public static readonly BindableProperty ThemeProperty = BindableProperty.Create(
			"Theme", typeof(BandTheme), typeof(ThemeView), default(BandTheme), BindingMode.OneWay, null, OnThemeChanged);

		public ThemeView()
		{
			InitializeComponent();
		}

		public BandTheme Theme
		{
			get { return (BandTheme)GetValue(ThemeProperty); }
			set { SetValue(ThemeProperty, value); }
		}

		private static void OnThemeChanged(BindableObject bindable, object old, object newValue)
		{
			var themeView = (ThemeView)bindable;
			var theme = (BandTheme)newValue;

			themeView.Base.Color = BandColorToColorConverter.Convert(theme.Base);
			themeView.HighContrast.Color = BandColorToColorConverter.Convert(theme.HighContrast);
			themeView.Highlight.Color = BandColorToColorConverter.Convert(theme.Highlight);
			themeView.Lowlight.Color = BandColorToColorConverter.Convert(theme.Lowlight);
			themeView.Muted.Color = BandColorToColorConverter.Convert(theme.Muted);
			themeView.SecondaryText.Color = BandColorToColorConverter.Convert(theme.SecondaryText);
		}
	}
}

