using System;
using System.Threading.Tasks;

using Foundation;

using Microsoft.Band.Tiles;

namespace Microsoft.Band.Personalization
{
	public static class BandPersonalizationManagerExtensions
	{
		public static Task UpdateMeTileImageAsync (this IBandPersonalizationManager manager, BandImage image)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.UpdateMeTileImageAsync (image, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task<BandImage> GetMeTileImageTaskAsync (this IBandPersonalizationManager manager)
		{
			var tcs = new TaskCompletionSource<BandImage> ();
			manager.GetMeTileImageAsync (tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task UpdateThemeAsync (this IBandPersonalizationManager manager, BandTheme theme)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.UpdateThemeAsync (theme, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task<BandTheme> GetThemeTaskAsync (this IBandPersonalizationManager manager)
		{
			var tcs = new TaskCompletionSource<BandTheme> ();
			manager.GetThemeAsync (tcs.AttachCompletionHandler ());
			return tcs.Task;
		}
	}
}
