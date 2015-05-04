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
    public class BandTileClosedEventArgs : BandTileEventArgs
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal BandTileClosedEventArgs(NativeTileEvent tileEvent)
            : this(tileEvent.TileId.FromNative())
        {
        }
#endif

        public BandTileClosedEventArgs(Guid tileId)
            : base(TileActionType.TileClosed, tileId)
        {
        }
    }
}
