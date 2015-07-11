using System;
using System.Collections.Generic;

using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Tiles.Pages;

namespace Microsoft.Band.Portable.Tiles
{
    using BandTheme = Microsoft.Band.Portable.BandTheme;

    public class BandTile
    {
        public BandTile(Guid tileId)
        {
            Id = tileId;

            PageLayouts = new List<PageLayout>();
            PageImages = new List<BandImage>();
        }

        public BandTile(Guid tileId, string name, BandImage icon)
            : this(tileId)
        {
            Name = name;
            Icon = icon;
        }

        public BandTile(Guid tileId, string name, BandImage icon, BandImage smallIcon)
            : this(tileId, name, icon)
        {
            SmallIcon = smallIcon;
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public BandImage Icon { get; set; }

        public BandImage SmallIcon { get; set; }

		public BandTheme Theme { get; set; }
        
        public List<PageLayout> PageLayouts { get; private set; }

        public List<BandImage> PageImages { get; private set; }

        public bool IsScreenTimeoutDisabled { get; set; }

		public bool IsCustomThemeEnabled 
		{ 
			get { return Theme != BandTheme.Empty; }
		}
    }
}
