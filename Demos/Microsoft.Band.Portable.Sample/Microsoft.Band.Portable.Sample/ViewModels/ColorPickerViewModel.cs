using Microsoft.Band.Portable.Personalization;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class ColorPickerViewModel : BaseViewModel
    {
        private BandColor color;

        public byte Red
        {
            get { return color.R; }
            set
            {
                if (color.R != value)
                {
                    color.R = value;
                    OnPropertyChanged("Red");
                    OnPropertyChanged("Color");
                }
            }
        }

        public byte Green
        {
            get { return color.G; }
            set
            {
                if (color.G != value)
                {
                    color.G = value;
                    OnPropertyChanged("Green");
                    OnPropertyChanged("Color");
                }
            }
        }

        public byte Blue
        {
            get { return color.B; }
            set
            {
                if (color.B != value)
                {
                    color.B = value;
                    OnPropertyChanged("Blue");
                    OnPropertyChanged("Color");
                }
            }
        }

        public BandColor Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                    OnPropertyChanged("Red");
                    OnPropertyChanged("Green");
                    OnPropertyChanged("Blue");
                }
            }
        }
    }
}
