using System.Collections.Generic;

namespace Microsoft.Band.Pages
{
	public partial class BandPagePanel
	{
		private object childrenLock = new object();
		private BandCollection<BandPageElement> children;

		public IList<BandPageElement> Children { 
			get {
				// check outside to avoid unnecessary locking
				if (children == null) {
					// we lock for thread safety
					lock (childrenLock) {
						// do a check before init
						if (children == null) {
							children = new BandCollection<BandPageElement> (ChildrenInternal);
						}
					}
				}

				return children;
			} 
		}
	}
}
