using Microsoft.Band.Portable.Personalization;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
	public partial class ColorPickerPage : BasePickerPage
    {
        private ColorPickerViewModel colorPickerViewModel;

        public ColorPickerPage()
        {
            InitializeComponent();

            colorPickerViewModel = new ColorPickerViewModel();
            ViewModel = colorPickerViewModel;
        }

        public BandColor Color
        {
            get { return colorPickerViewModel.Color; }
            set { colorPickerViewModel.Color = value; }
        }
    }
}
