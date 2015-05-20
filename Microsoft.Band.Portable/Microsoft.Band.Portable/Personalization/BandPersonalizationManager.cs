using System;
using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Personalization;
using NativeBandPersonalizationManager = Microsoft.Band.Personalization.IBandPersonalizationManager;
#elif __IOS__
using Microsoft.Band.Personalization;
using NativeBandPersonalizationManager = Microsoft.Band.Personalization.IBandPersonalizationManager;
using NativeBandImage = Microsoft.Band.BandImage;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Personalization;
using NativeBandPersonalizationManager = Microsoft.Band.Personalization.IBandPersonalizationManager;
#endif

namespace Microsoft.Band.Portable.Personalization
{
    public class BandPersonalizationManager
    {
        private readonly BandClient client;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandPersonalizationManager Native;

        internal BandPersonalizationManager(BandClient client, NativeBandPersonalizationManager personalizationManager)
        {
            this.Native = personalizationManager;

            this.client = client;
        }
#endif

        public async Task<BandImage> GetMeTileImageAsync()
        {
#if __ANDROID__
            var image = await Native.GetMeTileImageTaskAsync();
            if (image != null)
            {
                return new BandImage(image, image.Width, image.Height);
            }
#elif __IOS__
            var image = await Native.GetMeTileImageTaskAsync();
            if (image != null)
            {
                return new BandImage(image.UIImage, (int)image.Size.Width, (int)image.Size.Height);
            }
#elif WINDOWS_PHONE_APP
            var image = await Native.GetMeTileImageAsync();
            if (image != null)
            {
                return new BandImage(image.ToWriteableBitmap(), image.Width, image.Height);
            }
#endif
            return null;
        }

        public async Task<BandTheme> GetThemeAsync()
        {
#if __ANDROID__
            var theme = await Native.GetThemeTaskAsync();
            return theme.FromNative();
#elif __IOS__
            var theme = await Native.GetThemeTaskAsync();
            return theme.FromNative();
#elif WINDOWS_PHONE_APP
            var theme = await Native.GetThemeAsync();
            return theme.FromNative();
#else // PORTABLE
            return BandTheme.Empty;
#endif
        }

        public async Task SetMeTileImageAsync(BandImage image)
        {
#if __ANDROID__
            await Native.SetMeTileImageTaskAsync(image.ToBitmap());
#elif __IOS__
            await Native.SetMeTileImageTaskAsync(image.ToUIImage().ToBandImage());
#elif WINDOWS_PHONE_APP
            await Native.SetMeTileImageAsync(image.ToWriteableBitmap().ToBandImage());
#endif
        }

        public async Task SetThemeAsync(BandTheme theme)
        {
#if __ANDROID__ || __IOS__
            await Native.SetThemeTaskAsync(theme.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.SetThemeAsync(theme.ToNative());
#endif
        }
    }
}
