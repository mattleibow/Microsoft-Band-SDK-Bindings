using System;
using System.Collections.Generic;
using System.Linq;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativePageData = Microsoft.Band.Tiles.Pages.PageData;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages.Data
{
    public class PageData
    {
        public PageData()
        {
            Data = new List<ElementData>();
        }

        public Guid PageId { get; set; }
        public int PageLayoutIndex { get; set; }
        public List<ElementData> Data { get; private set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal PageData(NativePageData native)
        {
            PageId = native.PageId.FromNative();
            PageLayoutIndex = PageLayoutIndex;
            Data = native.Values.Select(e => e.FromNative()).ToList();
        }

        internal NativePageData ToNative()
        {
            NativePageData native = null;
#if __ANDROID__
            native = new NativePageData(PageId.ToNative(), PageLayoutIndex);
            foreach (var data in Data)
            {
                native.Update(data.ToNative());
            }
#elif __IOS__
            native = NativePageData.Create(PageId.ToNative(), (nuint)PageLayoutIndex, Data.Select(d => d.ToNative()).ToArray());
#elif WINDOWS_PHONE_APP
            native = new NativePageData(PageId.ToNative(), PageLayoutIndex, Data.Select(d => d.ToNative()).ToArray());
#endif
            return native;
        }
#endif
    }
}
