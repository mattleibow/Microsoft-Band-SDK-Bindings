using System;

namespace Microsoft.Band.Portable.Tiles
{
    public class BandTileEventArgs : EventArgs
    {
        public BandTileEventArgs(TileActionType actionType, Guid tileId)
        {
            ActionType = actionType;
            TileId = tileId;
        }

        public TileActionType ActionType { get; private set; }

        public Guid TileId { get; private set; }
    }
}
