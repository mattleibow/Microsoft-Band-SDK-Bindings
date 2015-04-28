using System;

#if __ANDROID__
using NativeTileEvent = Microsoft.Band.Tiles.TileEvent;
#elif __IOS__
using NativeTileEvent = Microsoft.Band.Tiles.BandTileEvent;
#elif WINDOWS_PHONE_APP
using NativeTileEvent = Microsoft.Band.Tiles.IBandTileEvent;
#endif

namespace Microsoft.Band.Portable.Tiles
{
    public class BandTileOpenedEventArgs : BandTileEventArgs
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal BandTileOpenedEventArgs(NativeTileEvent tileEvent)
            : this(tileEvent.TileId.FromNative())
        {
        }
#endif

        public BandTileOpenedEventArgs(Guid tileId)
            : base(TileActionType.TileOpened, tileId)
        {
        }
    }
}
