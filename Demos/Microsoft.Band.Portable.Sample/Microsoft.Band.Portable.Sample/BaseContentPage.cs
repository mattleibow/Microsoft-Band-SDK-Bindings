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
            // we can't await
            ViewModel.Destroy();

            return base.OnBackButtonPressed();
        }

        public static T OnPlatform<T>(T iOS, T Android, T Windows)
        {
            if (Device.OS == TargetPlatform.Windows)
            {
                return Windows;
            }

            return Device.OnPlatform(
                iOS: iOS, 
                Android: Android,
                WinPhone: Windows);
        }
    }
}
