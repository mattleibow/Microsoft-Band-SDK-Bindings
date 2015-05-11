#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeWrappedTextBlock = Microsoft.Band.Tiles.Pages.WrappedTextBlock;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.BandColor;

    public class WrappedTextBlock : TextBlockBase
    {
        public WrappedTextBlock()
        {
            AutoHeight = true;
            Color = BandColor.Empty;
            ColorSource = ElementColorSource.Custom;
            Font = WrappedTextBlockFont.Small;
        }

        public bool AutoHeight { get; set; }
        public override BandColor Color { get; set; }
        public override ElementColorSource ColorSource { get; set; }
        public WrappedTextBlockFont Font { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal WrappedTextBlock(NativeWrappedTextBlock native)
            : base(native)
        {
            AutoHeight = native.AutoHeight;
            Color = native.Color.FromNative();
            ColorSource = native.ColorSource.FromNative();
            Font = native.Font.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeWrappedTextBlock>(element);
            if (native == null)
            {
#if __ANDROID__ || __IOS__
                native = new NativeWrappedTextBlock(Rect.ToNative(), Font.ToNative());
#elif WINDOWS_PHONE_APP
                native = new NativeWrappedTextBlock();
                native.Font = Font.ToNative();
#endif
            }
            native.AutoHeight = AutoHeight;
            if (Color != BandColor.Empty)
            {
                native.Color = Color.ToNative();
            }
            native.ColorSource = ColorSource.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}