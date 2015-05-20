#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeTextBlock = Microsoft.Band.Tiles.Pages.TextBlock;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.BandColor;

    public class TextBlock : TextBlockBase
    {
        private static readonly bool DefaultAutoWidth = true;
        private static readonly short DefaultBaseline = 0;
        private static readonly TextBlockBaselineAlignment DefaultBaselineAlignment = TextBlockBaselineAlignment.Automatic;
        private static readonly BandColor DefaultColor = BandColor.Empty;
        private static readonly ElementColorSource DefaultColorSource = ElementColorSource.Custom;
        private static readonly TextBlockFont DefaultFont = TextBlockFont.Small;

        public TextBlock()
        {
            AutoWidth = DefaultAutoWidth;
            Baseline = DefaultBaseline;
            BaselineAlignment = DefaultBaselineAlignment;
            Color = DefaultColor;
            ColorSource = DefaultColorSource;
            Font = DefaultFont;
        }
        
        public TextBlock(BandColor color)
        {
            AutoWidth = DefaultAutoWidth;
            Baseline = DefaultBaseline;
            BaselineAlignment = DefaultBaselineAlignment;
            Color = color;
            ColorSource = DefaultColorSource;
            Font = DefaultFont;
        }

        public TextBlock(ElementColorSource colorSource)
        {
            AutoWidth = DefaultAutoWidth;
            Baseline = DefaultBaseline;
            BaselineAlignment = DefaultBaselineAlignment;
            Color = DefaultColor;
            ColorSource = colorSource;
            Font = DefaultFont;
        }

        public bool AutoWidth { get; set; }
        public short Baseline { get; set; }
        public TextBlockBaselineAlignment BaselineAlignment { get; set; }
        public override BandColor Color { get; set; }
        public override ElementColorSource ColorSource { get; set; }
        public TextBlockFont Font { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal TextBlock(NativeTextBlock native)
            : base(native)
        {
            AutoWidth = native.AutoWidth;
            Baseline = (short)native.Baseline;
            BaselineAlignment = native.BaselineAlignment.FromNative();
            Color = native.Color.FromNative();
            ColorSource = native.ColorSource.FromNative();
            Font = native.Font.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeTextBlock>(element);
            if (native == null)
            {
#if __ANDROID__
                native = new NativeTextBlock(Rect.ToNative(), Font.ToNative(), Baseline);
#elif __IOS__
                native = new NativeTextBlock(Rect.ToNative(), Font.ToNative());
                native.Baseline = (ushort)Baseline;
#elif WINDOWS_PHONE_APP
                native = new NativeTextBlock();
                native.Font = Font.ToNative();
                native.Baseline = Baseline;
#endif
            }
            native.AutoWidth = AutoWidth;
            native.BaselineAlignment = BaselineAlignment.ToNative();
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