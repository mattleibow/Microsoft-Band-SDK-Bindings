using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Tiles;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
#elif __IOS__
using Microsoft.Band.Tiles;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Tiles;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
#endif

namespace Microsoft.Band.Portable.Tiles
{
    public class BandTileManager
    {
        private readonly BandClient client;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandTileManager Native;

        internal BandTileManager(BandClient client, NativeBandTileManager tileManager)
        {
            this.Native = tileManager;

            this.client = client;
        }
#endif
        
        public async Task<bool> AddTileAsync(BandTile tile)
        {
            bool result = false;
#if __ANDROID__
            result = await Native.AddTileAsync(tile);
#elif __IOS__
            try
            {
                await Native.AddTileTaskAsync(tile.ToNative());
                result = true;
            }
            catch (BandException ex)
            {
                if (ex.Code == (int)BandNSErrorCodes.UserDeclinedTile)
                {
                    result = false;
                }
                else 
                {
                    throw;
                }
            }
#elif WINDOWS_PHONE_APP
            result = await Native.AddTileAsync(tile.ToNative());
#endif
            return result;
        }

        public async Task<int> GetRemainingTileCapacityAsync()
        {
#if __ANDROID__
            return await Native.GetRemainingTileCapacityTaskAsync();
#elif __IOS__
            return (int)await Native.RemainingTileCapacityTaskAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetRemainingTileCapacityAsync();
#else // PORTABLE
            return default(int);
#endif
        }

        public async Task<IEnumerable<BandTile>> GetTilesAsync()
        {
#if __ANDROID__
            return (await Native.GetTilesTaskAsync()).Select(x => x.FromNative());
#elif __IOS__
            return (await Native.GetTilesTaskAsync()).Select(x => x.FromNative());
#elif WINDOWS_PHONE_APP
            return (await Native.GetTilesAsync()).Select(x => x.FromNative());
#else // PORTABLE
            return null;
#endif
        }

        public async Task RemoveTileAsync(Guid tileId)
        {
#if __ANDROID__
            await Native.RemoveTileTaskAsync(tileId.ToNative());
#elif __IOS__
            await Native.RemoveTileTaskAsync(tileId.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.RemoveTileAsync(tileId.ToNative());
#endif
        }
    }
}
