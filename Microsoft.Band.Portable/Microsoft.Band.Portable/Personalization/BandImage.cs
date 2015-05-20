using System;
using System.IO;
using System.Threading.Tasks;

#if __ANDROID__
using Android.Graphics;
using NativeBitmap = Android.Graphics.Bitmap;
#elif __IOS__
using Foundation;
using NativeBitmap = UIKit.UIImage;
#elif WINDOWS_PHONE_APP
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using NativeBitmap = Windows.UI.Xaml.Media.Imaging.WriteableBitmap;
#endif

namespace Microsoft.Band.Portable
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
        
#if __ANDROID__
        public NativeBitmap ToBitmap()
        {
            return nativeBitmap;
        }
#elif __IOS__
		public NativeBitmap ToUIImage()
        {
			return nativeBitmap;
        }
#elif WINDOWS_PHONE_APP
        public NativeBitmap ToWriteableBitmap()
        {
            return nativeBitmap;
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

        public async Task<Stream> ToStreamAsync()
        {
#if __ANDROID__
            var native = ToBitmap();
            var stream = new MemoryStream();
            if (await native.CompressAsync(NativeBitmap.CompressFormat.Png, 0, stream))
            {
                stream.Position = 0;
                return stream;
            }
            return null;
#elif __IOS__
			var native = ToUIImage();
            return native.AsPNG().AsStream();
#elif WINDOWS_PHONE_APP
			var native = ToWriteableBitmap();
            var stream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            using (Stream pixelStream = native.PixelBuffer.AsStream())
            {
                byte[] pixels = new byte[pixelStream.Length];
                await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight,
                                    (uint)native.PixelWidth,
                                    (uint)native.PixelHeight,
                                    96.0,
                                    96.0,
                                    pixels);
                await encoder.FlushAsync();
            }
            return stream.AsStream();
#else // PORTABLE
            return null;
#endif
        }
    }
}
