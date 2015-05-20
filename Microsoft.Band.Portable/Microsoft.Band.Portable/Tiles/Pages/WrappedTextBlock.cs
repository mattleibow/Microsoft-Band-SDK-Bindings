#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeWrappedTextBlock = Microsoft.Band.Tiles.Pages.WrappedTextBlock;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.BandColor;

    public class WrappedTextBlock : TextBlockBase
    {
        private static readonly bool DefaultAutoHeight = true;
        private static readonly BandColor DefaultColor = BandColor.Empty;
        private static readonly ElementColorSource DefaultColorSource = ElementColorSource.Custom;
        private static readonly WrappedTextBlockFont DefaultFont = WrappedTextBlockFont.Small;

        public WrappedTextBlock()
        {
            AutoHeight = DefaultAutoHeight;
            Color = DefaultColor;
            ColorSource = DefaultColorSource;
            Font = DefaultFont;
        }
        
        public WrappedTextBlock(BandColor color)
        {
            AutoHeight = DefaultAutoHeight;
            Color = color;
            ColorSource = DefaultColorSource;
            Font = DefaultFont;
        }

        public WrappedTextBlock(ElementColorSource colorSource)
        {
            AutoHeight = DefaultAutoHeight;
            Color = DefaultColor;
            ColorSource = colorSource;
            Font = DefaultFont;
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