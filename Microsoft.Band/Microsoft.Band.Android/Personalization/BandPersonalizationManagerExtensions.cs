using System.Threading.Tasks;
using Android.Graphics;
using Microsoft.Band.Tiles;

namespace Microsoft.Band.Personalization
{
    public static class BandPersonalizationManagerExtensions
    {
        public static Task SetMeTileImageTaskAsync(this IBandPersonalizationManager manager, Bitmap bitmap)
        {
            return manager.SetMeTileImageAsync(bitmap).AsTask();
        }

        public static async Task<Bitmap> GetMeTileImageTaskAsync(this IBandPersonalizationManager manager)
        {
            return (Bitmap)await manager.GetMeTileImageAsync().AsTask();
        }

        public static Task SetThemeTaskAsync(this IBandPersonalizationManager manager, BandTheme theme)
        {
            return manager.SetThemeAsync(theme).AsTask();
        }

        public static async Task<BandTheme> GetThemeTaskAsync(this IBandPersonalizationManager manager)
        {
            return (BandTheme)await manager.GetThemeAsync().AsTask();
        }
    }
}