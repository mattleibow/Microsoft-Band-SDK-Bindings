using System;
using System.Globalization;

using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;

namespace Microsoft.Band.Portable.Sample.ValueConverters
{
    public class BandImageToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = value as BandImage;
            if (image != null)
            {
                return new StreamImageSource
                {
                    Stream = ct => image.ToStreamAsync()
                };
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
