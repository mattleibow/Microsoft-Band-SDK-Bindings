using System.Collections.Generic;
using System.Linq;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativePanel = Microsoft.Band.Tiles.Pages.PagePanel;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    public abstract class Panel : Element
    {
        public Panel()
        {
            Elements = new List<Element>();
        }

        public Panel(IEnumerable<Element> elements)
        {
            Elements = new List<Element>(elements);
        }

        public List<Element> Elements { get; private set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal Panel(NativePanel native)
            : base(native)
        {
            Elements = native.Elements.Select(e => e.FromNative()).ToList();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativePanel>(element, false);
#if __ANDROID__ || __IOS__
            native.AddElements(Elements.Select(e => e.ToNative()).ToArray());
#elif WINDOWS_PHONE_APP
            foreach (var childElement in Elements)
            {
                native.Elements.Add(childElement.ToNative());
            }
#endif
            return base.ToNative(native);
        }
#endif
    }
}
