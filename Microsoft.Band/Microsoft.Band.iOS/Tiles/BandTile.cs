using System.Collections.Generic;

using Microsoft.Band.Pages;

namespace Microsoft.Band.Tiles
{
	public partial class BandTile
	{
		private object pageIconsLock = new object();
		private BandCollection<BandIcon> pageIcons;

		public IList<BandIcon> PageIcons { 
			get {
				// check outside to avoid unnecessary locking
				if (pageIcons == null) {
					// we lock for thread safety
					lock (pageIconsLock) {
						// do a check before init
						if (pageIcons == null) {
							pageIcons = new BandCollection<BandIcon> (PageIconsInternal);
						}
					}
				}

				return pageIcons;
			} 
		}

		private object pageLayoutsLock = new object();
		private BandCollection<BandPageLayout> pageLayouts;

		public IList<BandPageLayout> PageLayouts { 
			get {
				// check outside to avoid unnecessary locking
				if (pageLayouts == null) {
					// we lock for thread safety
					lock (pageLayoutsLock) {
						// do a check before init
						if (pageLayouts == null) {
							pageLayouts = new BandCollection<BandPageLayout> (PageLayoutsInternal);
						}
					}
				}

				return pageLayouts;
			} 
		}
	}
}
