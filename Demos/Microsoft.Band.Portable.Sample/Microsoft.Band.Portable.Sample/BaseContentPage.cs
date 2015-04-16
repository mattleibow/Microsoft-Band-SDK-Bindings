using Xamarin.Forms;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public class BaseContentPage : ContentPage
    {
        protected BaseViewModel ViewModel;

        public BaseContentPage()
        {
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // we must await
            await ViewModel.Prepare();
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // no need to await
            ViewModel.CleanUp();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();

            // we can't await
            ViewModel.Destroy();
        }
    }
}
