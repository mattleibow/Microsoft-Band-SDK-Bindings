#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativeTextBlock = Microsoft.Band.Tiles.Pages.TextBlock;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    using BandColor = Microsoft.Band.Portable.Personalization.BandColor;

    public class TextBlock : TextBlockBase
    {
        public TextBlock()
        {
            AutoWidth = true;
            Baseline = 0;
            BaselineAlignment = TextBlockBaselineAlignment.Automatic;
            TextColor = BandColor.Empty;
            TextColorSource = ElementColorSource.Custom;
            Font = TextBlockFont.Small;
        }

        public bool AutoWidth { get; set; }
        public short Baseline { get; set; }
        public TextBlockBaselineAlignment BaselineAlignment { get; set; }
        public override BandColor TextColor { get; set; }
        public override ElementColorSource TextColorSource { get; set; }
        public TextBlockFont Font { get; set; }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal TextBlock(NativeTextBlock native)
            : base(native)
        {
            AutoWidth = native.AutoWidth;
            Baseline = (short)native.Baseline;
            BaselineAlignment = native.BaselineAlignment.FromNative();
            TextColor = native.Color.FromNative();
            TextColorSource = native.ColorSource.FromNative();
            Font = native.Font.FromNative();
        }

        internal override NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeTextBlock>(element);
            if (native == null)
            {
#if __ANDROID__
                native = new NativeTextBlock(Rectangle.ToNative(), Font.ToNative(), Baseline);
#elif __IOS__
                native = new NativeTextBlock(Rectangle.ToNative(), Font.ToNative());
                native.Baseline = (ushort)Baseline;
#elif WINDOWS_PHONE_APP
                native = new NativeTextBlock();
                native.Font = Font.ToNative();
                native.Baseline = Baseline;
#endif
            }
            native.AutoWidth = AutoWidth;
            native.BaselineAlignment = BaselineAlignment.ToNative();
            if (TextColor != BandColor.Empty)
            {
                native.Color = TextColor.ToNative();
            }
            native.ColorSource = TextColorSource.ToNative();
            return base.ToNative(native);
        }
#endif
    }
}