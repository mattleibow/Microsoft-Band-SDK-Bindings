using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation;

using Microsoft.Band.Tiles.Pages;
using Microsoft.Band.Tiles;

namespace Microsoft.Band.Tiles
{
	public static class BandTileManagerExtensions
	{
		public static Task<BandTile[]> GetTilesTaskAsync (this IBandTileManager manager)
		{
			var tcs = new TaskCompletionSource<BandTile[]> ();
			manager.GetTilesAsync (tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task AddTileTaskAsync (this IBandTileManager manager, BandTile tile)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.AddTileAsync (tile, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task RemoveTileTaskAsync (this IBandTileManager manager, BandTile tile)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.RemoveTileAsync (tile, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task RemoveTileTaskAsync (this IBandTileManager manager, NSUuid tileId)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.RemoveTileAsync (tileId, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

        public static Task<nuint> GetRemainingTileCapacityTaskAsync (this IBandTileManager manager)
		{
			var tcs = new TaskCompletionSource<nuint> ();
            manager.GetRemainingTileCapacityAsync (tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task SetPagesTaskAsync (this IBandTileManager manager, PageData[] pageData, NSUuid tileId)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.SetPagesAsync (pageData, tileId, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}

		public static Task RemovePagesTaskAsync (this IBandTileManager manager, NSUuid tileId)
		{
			var tcs = new TaskCompletionSource<object> ();
			manager.RemovePagesAsync (tileId, tcs.AttachCompletionHandler ());
			return tcs.Task;
		}
	}
}
