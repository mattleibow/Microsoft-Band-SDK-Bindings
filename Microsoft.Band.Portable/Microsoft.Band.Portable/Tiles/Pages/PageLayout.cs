#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativePageLayout = Microsoft.Band.Tiles.Pages.PageLayout;
using NativePanel = Microsoft.Band.Tiles.Pages.PagePanel;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    public class PageLayout
    {
        public PageLayout()
        {
            Root = null;
        }

        public PageLayout(Panel root)
        {
            Root = root;
        }

        public Panel Root { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal PageLayout(NativePageLayout native)
        {
            Root = native.Root.FromNative();
        }

        internal NativePageLayout ToNative()
        {
            return new NativePageLayout((NativePanel)Root.ToNative());
        }
#endif
    }
}