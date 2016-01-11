using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

using Plugin.Media;
using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Tiles;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class PersonalizationViewModel : BaseClientViewModel
    {
        private BandPersonalizationManager personalizationManager;

        private BandImage meTileImage;
        private BandTheme bandTheme;

        public PersonalizationViewModel(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            personalizationManager = bandClient.PersonalizationManager;

            GetBandThemeCommand = new Command(async () =>
            {
                BandTheme = await personalizationManager.GetThemeAsync();
            });
            DefaultBandThemeCommand = new Command(() =>
            {
                BandTheme = App.DefaultTheme;
            });
            SetBandThemeCommand = new Command(async () =>
            {
                await personalizationManager.SetThemeAsync(BandTheme);
            });

            GetMeTileImageCommand = new Command(async () =>
            {
                MeTileImage = await personalizationManager.GetMeTileImageAsync();
            });
            DefaultMeTileImageCommand = new Command(async () =>
            {
                MeTileImage = await App.LoadImageResourceAsync("Resources/metile.png");
            });
            SelectMeTileImageCommand = new Command(async () =>
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                {
                    MeTileImage = await BandImage.FromStreamAsync(photo.GetStream());
                }
            }, () => CrossMedia.Current.IsPickPhotoSupported);
            SetMeTileImageCommand = new Command(async () =>
            {
                await personalizationManager.SetMeTileImageAsync(MeTileImage);
            });

			Init();
        }

        public async void Init()
        {
			IsLoading = true;

			BandTheme = await personalizationManager.GetThemeAsync();

			MeTileImage = await personalizationManager.GetMeTileImageAsync();

			IsLoading = false;
        }
        
        public BandImage MeTileImage
        {
            get { return meTileImage; }
            set
            {
                if (meTileImage != value)
                {
                    meTileImage = value;
                    OnPropertyChanged("MeTileImage");
                }
            }
        }
        
        public BandTheme BandTheme
        {
            get { return bandTheme; }
            set
            {
                if (bandTheme != value)
                {
                    bandTheme = value;
                    OnPropertyChanged("BandTheme");
                }
            }
        }

        public ICommand GetBandThemeCommand { get; private set; }
        public ICommand DefaultBandThemeCommand { get; private set; }
        public ICommand SetBandThemeCommand { get; private set; }
        public ICommand GetMeTileImageCommand { get; private set; }
        public ICommand DefaultMeTileImageCommand { get; private set; }
        public ICommand SelectMeTileImageCommand { get; private set; }
        public ICommand SetMeTileImageCommand { get; private set; }
    }
}
