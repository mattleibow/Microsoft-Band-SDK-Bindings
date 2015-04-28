using System;

#if __ANDROID__
using NativeTileButtonEvent = Microsoft.Band.Tiles.TileButtonEvent;
#elif __IOS__
using NativeTileButtonEvent = Microsoft.Band.Tiles.BandTileButtonEvent;
#elif WINDOWS_PHONE_APP
using NativeTileButtonEvent = Microsoft.Band.Tiles.IBandTileButtonPressedEvent;
#endif

namespace Microsoft.Band.Portable.Tiles
{
    public class BandTileButtonPressedEventArgs : BandTileEventArgs
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal BandTileButtonPressedEventArgs(NativeTileButtonEvent tileButtonEvent)
            : this(tileButtonEvent.ElementId, tileButtonEvent.PageId.FromNative(), tileButtonEvent.TileId.FromNative())
        {
        }
#endif

        public BandTileButtonPressedEventArgs(int elementId, Guid pageId, Guid tileId)
            : base(TileActionType.ButtonPressed, tileId)
        {
            ElementId = elementId;
            PageId = pageId;
        }

        public int ElementId { get; private set; }

        public Guid PageId { get; private set; }
    }
}
