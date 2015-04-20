using System;
using System.Globalization;

using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;

namespace Microsoft.Band.Portable.Sample.ValueConverters
{
    public class BandColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BandColor)
            {
				return Convert((BandColor)value);
            }
            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //if (value is Color)
            //{
            //    var color = (Color)value;
            //    return new BandColor((byte)color.R, (byte)color.G, (byte)color.B);
            //}
            //return new BandColor();
        }

		public static Color Convert(BandColor color)
		{
			return Color.FromRgb(color.R, color.G, color.B);
		}
    }
}
