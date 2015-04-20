using System;
using System.Globalization;

using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample.ValueConverters
{
    public class BandThemeToItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			if (value is BandTheme)
            {
				var theme = (BandTheme)value;
                return new[]
                {
                    new BandThemeColorViewModel("Base", () => theme.Base, color => theme.Base = color),
                    new BandThemeColorViewModel("High Contrast", () => theme.HighContrast, color => theme.HighContrast = color),
                    new BandThemeColorViewModel("Highlight", () => theme.Highlight, color => theme.Highlight = color),
                    new BandThemeColorViewModel("Lowlight", () => theme.Lowlight, color => theme.Lowlight = color),
                    new BandThemeColorViewModel("Muted", () => theme.Muted, color => theme.Muted = color),
                    new BandThemeColorViewModel("Secondary Text", () => theme.SecondaryText, color => theme.SecondaryText = color),
                };
            }
            return new BandThemeColorViewModel[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
