#if __ANDROID__
using Android.Graphics;
using NativeBitmap = Android.Graphics.Bitmap;
#elif __IOS__
using Foundation;
using NativeBitmap = UIKit.UIImage;
#elif WINDOWS_PHONE_APP
using Windows.UI.Xaml.Media.Imaging;
using NativeBitmap = Windows.UI.Xaml.Media.Imaging.WriteableBitmap;
#endif

using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Personalization
{
    public sealed class BandImage
    {
        private BandImage()
        {

        }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        private NativeBitmap nativeBitmap;

        internal BandImage(NativeBitmap bitmap, int width, int height)
        {
            this.nativeBitmap = bitmap;

            this.Width = width;
            this.Height = height;
        }

        internal NativeBitmap ToNative()
        {
            return nativeBitmap;
        }
#endif

        public int Width { get; private set; }

        public int Height { get; private set; }

#if __ANDROID__
        public static BandImage FromBitmap(NativeBitmap bitmap)
        {
            return new BandImage(bitmap, bitmap.Width, bitmap.Height);
        }
#elif __IOS__
        public static BandImage FromUIImage(NativeBitmap uiimage)
        {
            return new BandImage(uiimage, (int)uiimage.Size.Width, (int)uiimage.Size.Height);
        }
#elif WINDOWS_PHONE_APP
        public static BandImage FromWriteableBitmap(NativeBitmap writeableBitmap)
        {
            return new BandImage(writeableBitmap, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight);
        }
#endif
        
        public static async Task<BandImage> FromStreamAsync(Stream stream)
        {
#if __ANDROID__
            var image = await BitmapFactory.DecodeStreamAsync(stream);
            return FromBitmap(image);
#elif __IOS__
            var image = await Task.Run(() =>
            {
                using (var data = NSData.FromStream(stream))
                {
                    return NativeBitmap.LoadFromData(data);
                }
            });
            return FromUIImage(image);
#elif WINDOWS_PHONE_APP
            using (var fileStream = stream.AsRandomAccessStream())
            {
                var bitmap = new NativeBitmap(1, 1);
                await bitmap.SetSourceAsync(fileStream);
                return FromWriteableBitmap(bitmap);
            }
#else // PORTABLE
            return null;
#endif
        }
    }
}
