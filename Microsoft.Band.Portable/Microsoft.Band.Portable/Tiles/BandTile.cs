using System;

using Microsoft.Band.Portable.Personalization;

namespace Microsoft.Band.Portable.Tiles
{
    public class BandTile
    {
        public BandTile(Guid tileId)
        {
            Id = tileId;
        }

        public BandTile(Guid tileId, string name, BandImage icon)
        {
            Id = tileId;
            Name = name;
            Icon = icon;
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public BandImage Icon { get; set; }

        public BandImage SmallIcon { get; set; }

        public BandTheme Theme { get; set; }

        public bool IsBadgingEnabled { get { return SmallIcon != null; } }
    }
}
