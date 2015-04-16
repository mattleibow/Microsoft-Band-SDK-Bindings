using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static async Task<BandImage> LoadImageResourceAsync(string resourcePath)
        {
            // get the resource stream from Embedded Resources
            var stream = await Task.Run(() =>
            {
                var assembly = typeof(ClientViewModel).GetTypeInfo().Assembly;
                resourcePath = "Microsoft.Band.Portable.Sample." + resourcePath.Replace("/", ".").Replace("\\", ".");
                return assembly.GetManifestResourceStream(resourcePath);
            });
            // load the image
            return await BandImage.FromStreamAsync(stream);
        }

        public static BandTheme DefaultTheme
        {
            get
            {
                return new BandTheme
                {
                    Base = new BandColor(52, 152, 219),
                    HighContrast = new BandColor(81, 162, 217),
                    Highlight = new BandColor(81, 162, 217),
                    Lowlight = new BandColor(41, 124, 181),
                    Muted = new BandColor(16, 79, 123),
                    SecondaryText = new BandColor(132, 151, 164)
                };
            }
        }
    }
}
