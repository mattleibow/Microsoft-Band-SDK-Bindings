using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Runtime;
using Microsoft.Band.Tiles.Pages;

namespace Microsoft.Band.Tiles
{
    public static class BandTileManagerExtensions
    {
        public static async Task<IEnumerable<BandTile>> GetTilesTaskAsync(this IBandTileManager manager)
        {
            var result = await manager.GetTilesAsync().AsTask();
            var tiles = result.JavaCast<Java.Util.ICollection>();
            var enumerable = tiles.ToEnumerable<BandTile>();
            return enumerable;
        }

        public static async Task<int> GetRemainingTileCapacityTaskAsync(this IBandTileManager manager)
        {
            return (int)await manager.GetRemainingTileCapacityAsync().AsTask();
        }

        public static async Task<bool> AddTileTaskAsync(this IBandTileManager manager, Activity activity, BandTile tile)
        {
            return (bool)await manager.AddTileAsync(activity, tile).AsTask();
        }

        public static async Task<bool> RemoveTileTaskAsync(this IBandTileManager manager, BandTile tile)
        {
            return (bool)await manager.RemoveTileAsync(tile).AsTask();
        }

        public static async Task<bool> RemoveTileTaskAsync(this IBandTileManager manager, Java.Util.UUID tileId)
        {
            return (bool)await manager.RemoveTileAsync(tileId).AsTask();
		}

		public static async Task<bool> RemovePagesTaskAsync(this IBandTileManager manager, Java.Util.UUID tileId)
		{
			return (bool)await manager.RemovePagesAsync(tileId).AsTask();
		}

		public static async Task<bool> SetPagesTaskAsync(this IBandTileManager manager, Java.Util.UUID tileId, params PageData[] pageDatas)
		{
			return (bool)await manager.SetPagesAsync(tileId, pageDatas).AsTask();
		}
    }
}