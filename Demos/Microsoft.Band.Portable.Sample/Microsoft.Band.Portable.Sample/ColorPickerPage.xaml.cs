using System;
using System.Threading.Tasks;

using Microsoft.Band.Portable.Personalization;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class ColorPickerPage : BaseContentPage
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

        public event EventHandler Picked;

        protected virtual void OnPicked()
        {
            var handler = Picked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler Canceled;

        protected virtual void OnCanceled()
        {
            var handler = Canceled;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private async void PickButtonClicked(object sender, EventArgs e)
        {
            OnPicked();

            await PopAsync();
        }

        private async void CancelButtonClicked(object sender, EventArgs e)
        {
            OnCanceled();

            await PopAsync();
        }

        private async Task PopAsync()
        {
            if (Navigation.ModalStack.Count > 0 &&
                Navigation.ModalStack[Navigation.ModalStack.Count - 1] == this)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}
