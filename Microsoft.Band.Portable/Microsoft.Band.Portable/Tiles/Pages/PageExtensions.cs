using System;
using System.Linq;
using System.Reflection;

using Microsoft.Band.Portable.Tiles.Pages;
using Microsoft.Band.Portable.Tiles.Pages.Data;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeBarcodeType = Microsoft.Band.Tiles.Pages.BarcodeType;
using NativeHorizontalAlignment = Microsoft.Band.Tiles.Pages.HorizontalAlignment;
using NativeVerticalAlignment = Microsoft.Band.Tiles.Pages.VerticalAlignment;
using NativeRectangle = Microsoft.Band.Tiles.Pages.PageRect;
using NativeMargins = Microsoft.Band.Tiles.Pages.Margins;
using NativeElementColorSource = Microsoft.Band.Tiles.Pages.ElementColorSource;
using NativeFlowPanelOrientation = Microsoft.Band.Tiles.Pages.FlowPanelOrientation;
using NativeTextBlockBaselineAlignment = Microsoft.Band.Tiles.Pages.TextBlockBaselineAlignment;
using NativeTextBlockFont = Microsoft.Band.Tiles.Pages.TextBlockFont;
using NativeWrappedTextBlockFont = Microsoft.Band.Tiles.Pages.WrappedTextBlockFont;
using NativeElementData = Microsoft.Band.Tiles.Pages.PageElementData;
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
using NativePanel = Microsoft.Band.Tiles.Pages.PagePanel;
using ConstructorCollection = System.Collections.Concurrent.ConcurrentDictionary<System.Type, System.Reflection.ConstructorInfo>;
#endif

namespace Microsoft.Band.Portable
{
    internal static class PageExtensions
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        private readonly static ConstructorCollection elementConstructors;
        private readonly static ConstructorCollection dataConstructors;
        private readonly static TypeInfo[] elementTypes;
        private readonly static TypeInfo[] elementDataTypes;

        static PageExtensions()
        {
            elementConstructors = new ConstructorCollection();
            dataConstructors = new ConstructorCollection();

            // get all the Element types
            var assembly = typeof(PageExtensions).GetTypeInfo().Assembly;
            var elementType = typeof(Element).GetTypeInfo();
            elementTypes = assembly.DefinedTypes.Where(t => !t.IsAbstract && elementType.IsAssignableFrom(t)).ToArray();
            var elementDataType = typeof(ElementData).GetTypeInfo();
            elementDataTypes = assembly.DefinedTypes.Where(t => !t.IsAbstract && elementDataType.IsAssignableFrom(t)).ToArray();
        }

        internal static NativeBarcodeType ToNative(this BarcodeType barcodeType)
        {
            // can't use switch on Android as this is not an enum
            if (barcodeType == BarcodeType.Code39)
                return NativeBarcodeType.Code39;
            if (barcodeType == BarcodeType.Pdf417)
                return NativeBarcodeType.Pdf417;
            throw new ArgumentOutOfRangeException("barcodeType", "Invalid BarcodeType specified.");
        }
        internal static BarcodeType FromNative(this NativeBarcodeType barcodeType)
        {
            // can't use switch on Android as this is not an enum
            if (barcodeType == NativeBarcodeType.Code39)
                return BarcodeType.Code39;
            if (barcodeType == NativeBarcodeType.Pdf417)
                return BarcodeType.Pdf417;
            throw new ArgumentOutOfRangeException("barcodeType", "Invalid BarcodeType specified.");
        }

        internal static NativeFlowPanelOrientation ToNative(this FlowPanelOrientation orientation)
        {
            // can't use switch on Android as this is not an enum
            if (orientation == FlowPanelOrientation.Horizontal)
                return NativeFlowPanelOrientation.Horizontal;
            if (orientation == FlowPanelOrientation.Vertical)
                return NativeFlowPanelOrientation.Vertical;
            throw new ArgumentOutOfRangeException("orientation", "Invalid Orientation specified.");
        }
        internal static FlowPanelOrientation FromNative(this NativeFlowPanelOrientation orientation)
        {
            // can't use switch on Android as this is not an enum
            if (orientation == NativeFlowPanelOrientation.Horizontal)
                return FlowPanelOrientation.Horizontal;
            if (orientation == NativeFlowPanelOrientation.Vertical)
                return FlowPanelOrientation.Vertical;
            throw new ArgumentOutOfRangeException("orientation", "Invalid FlowPanelOrientation specified.");
        }

        internal static NativeElementColorSource ToNative(this ElementColorSource elementColorSource)
        {
            // can't use switch on Android as this is not an enum
            if (elementColorSource == ElementColorSource.Custom)
                return NativeElementColorSource.Custom;

            if (elementColorSource == ElementColorSource.BandBase)
                return NativeElementColorSource.BandBase;
            if (elementColorSource == ElementColorSource.BandHighContrast)
                return NativeElementColorSource.BandHighContrast;
            if (elementColorSource == ElementColorSource.BandHighlight)
                return NativeElementColorSource.BandHighlight;
            if (elementColorSource == ElementColorSource.BandLowlight)
                return NativeElementColorSource.BandLowlight;
            if (elementColorSource == ElementColorSource.BandMuted)
                return NativeElementColorSource.BandMuted;
            if (elementColorSource == ElementColorSource.BandSecondaryText)
                return NativeElementColorSource.BandSecondaryText;

            if (elementColorSource == ElementColorSource.TileBase)
                return NativeElementColorSource.TileBase;
            if (elementColorSource == ElementColorSource.TileHighContrast)
                return NativeElementColorSource.TileHighContrast;
            if (elementColorSource == ElementColorSource.TileHighlight)
                return NativeElementColorSource.TileHighlight;
            if (elementColorSource == ElementColorSource.TileLowlight)
                return NativeElementColorSource.TileLowlight;
            if (elementColorSource == ElementColorSource.TileMuted)
                return NativeElementColorSource.TileMuted;
            if (elementColorSource == ElementColorSource.TileSecondaryText)
                return NativeElementColorSource.TileSecondaryText;

            throw new ArgumentOutOfRangeException("elementColorSource", "Invalid ElementColorSource specified.");
        }
        internal static ElementColorSource FromNative(this NativeElementColorSource elementColorSource)
        {
            // can't use switch on Android as this is not an enum
            if (elementColorSource == NativeElementColorSource.Custom)
                return ElementColorSource.Custom;

            if (elementColorSource == NativeElementColorSource.BandBase)
                return ElementColorSource.BandBase;
            if (elementColorSource == NativeElementColorSource.BandHighContrast)
                return ElementColorSource.BandHighContrast;
            if (elementColorSource == NativeElementColorSource.BandHighlight)
                return ElementColorSource.BandHighlight;
            if (elementColorSource == NativeElementColorSource.BandLowlight)
                return ElementColorSource.BandLowlight;
            if (elementColorSource == NativeElementColorSource.BandMuted)
                return ElementColorSource.BandMuted;
            if (elementColorSource == NativeElementColorSource.BandSecondaryText)
                return ElementColorSource.BandSecondaryText;

            if (elementColorSource == NativeElementColorSource.TileBase)
                return ElementColorSource.TileBase;
            if (elementColorSource == NativeElementColorSource.TileHighContrast)
                return ElementColorSource.TileHighContrast;
            if (elementColorSource == NativeElementColorSource.TileHighlight)
                return ElementColorSource.TileHighlight;
            if (elementColorSource == NativeElementColorSource.TileLowlight)
                return ElementColorSource.TileLowlight;
            if (elementColorSource == NativeElementColorSource.TileMuted)
                return ElementColorSource.TileMuted;
            if (elementColorSource == NativeElementColorSource.TileSecondaryText)
                return ElementColorSource.TileSecondaryText;

            throw new ArgumentOutOfRangeException("elementColorSource", "Invalid ElementColorSource specified.");
        }

        internal static NativeRectangle ToNative(this PageRect rectangle)
        {
#if __ANDROID__ || WINDOWS_PHONE_APP
            return new NativeRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
#elif __IOS__
            return NativeRectangle.Create((ushort)rectangle.X, (ushort)rectangle.Y, (ushort)rectangle.Width, (ushort)rectangle.Height);
#endif
        }
        internal static PageRect FromNative(this NativeRectangle rectangle)
        {
            return new PageRect((short)rectangle.X, (short)rectangle.Y, (short)rectangle.Width, (short)rectangle.Height);
        }

        internal static NativeMargins ToNative(this Margins margins)
        {
#if __ANDROID__ || WINDOWS_PHONE_APP
            return new NativeMargins(margins.Left, margins.Top, margins.Right, margins.Bottom);
#elif __IOS__
            return NativeMargins.Create(margins.Left, margins.Top, margins.Right, margins.Bottom);
#endif
        }
        internal static Margins FromNative(this NativeMargins margins)
        {
            return new Margins((short)margins.Left, (short)margins.Top, (short)margins.Right, (short)margins.Bottom);
        }

        internal static NativeTextBlockBaselineAlignment ToNative(this TextBlockBaselineAlignment baselineAlignment)
        {
            // can't use switch on Android as this is not an enum
            if (baselineAlignment == TextBlockBaselineAlignment.Absolute)
                return NativeTextBlockBaselineAlignment.Absolute;
            if (baselineAlignment == TextBlockBaselineAlignment.Relative)
                return NativeTextBlockBaselineAlignment.Relative;
            return NativeTextBlockBaselineAlignment.Automatic;
        }
        internal static TextBlockBaselineAlignment FromNative(this NativeTextBlockBaselineAlignment baselineAlignment)
        {
            // can't use switch on Android as this is not an enum
            if (baselineAlignment == NativeTextBlockBaselineAlignment.Absolute)
                return TextBlockBaselineAlignment.Absolute;
            if (baselineAlignment == NativeTextBlockBaselineAlignment.Relative)
                return TextBlockBaselineAlignment.Relative;
            return TextBlockBaselineAlignment.Automatic;
        }

        internal static NativeTextBlockFont ToNative(this TextBlockFont font)
        {
            // can't use switch on Android as this is not an enum
            if (font == TextBlockFont.Small)
                return NativeTextBlockFont.Small;
            if (font == TextBlockFont.Medium)
                return NativeTextBlockFont.Medium;
            if (font == TextBlockFont.Large)
                return NativeTextBlockFont.Large;
            if (font == TextBlockFont.ExtraLargeNumbers)
                return NativeTextBlockFont.ExtraLargeNumbers;
            if (font == TextBlockFont.ExtraLargeNumbersBold)
                return NativeTextBlockFont.ExtraLargeNumbersBold;
            throw new ArgumentOutOfRangeException("font", "Invalid TextBlockFont specified.");
        }
        internal static TextBlockFont FromNative(this NativeTextBlockFont font)
        {
            // can't use switch on Android as this is not an enum
            if (font == NativeTextBlockFont.Small)
                return TextBlockFont.Small;
            if (font == NativeTextBlockFont.Medium)
                return TextBlockFont.Medium;
            if (font == NativeTextBlockFont.Large)
                return TextBlockFont.Large;
            if (font == NativeTextBlockFont.ExtraLargeNumbers)
                return TextBlockFont.ExtraLargeNumbers;
            if (font == NativeTextBlockFont.ExtraLargeNumbersBold)
                return TextBlockFont.ExtraLargeNumbersBold;
            throw new ArgumentOutOfRangeException("font", "Invalid TextBlockFont specified.");
        }

        internal static NativeWrappedTextBlockFont ToNative(this WrappedTextBlockFont font)
        {
            // can't use switch on Android as this is not an enum
            if (font == WrappedTextBlockFont.Small)
                return NativeWrappedTextBlockFont.Small;
            if (font == WrappedTextBlockFont.Medium)
                return NativeWrappedTextBlockFont.Medium;
            throw new ArgumentOutOfRangeException("font", "Invalid WrappedTextBlockFont specified.");
        }
        internal static WrappedTextBlockFont FromNative(this NativeWrappedTextBlockFont font)
        {
            // can't use switch on Android as this is not an enum
            if (font == NativeWrappedTextBlockFont.Small)
                return WrappedTextBlockFont.Small;
            if (font == NativeWrappedTextBlockFont.Medium)
                return WrappedTextBlockFont.Medium;
            throw new ArgumentOutOfRangeException("font", "Invalid WrappedTextBlockFont specified.");
        }

        internal static NativeHorizontalAlignment ToNative(this HorizontalAlignment horizontalAlignment)
        {
            // can't use switch on Android as this is not an enum
            if (horizontalAlignment == HorizontalAlignment.Center)
                return NativeHorizontalAlignment.Center;
            if (horizontalAlignment == HorizontalAlignment.Left)
                return NativeHorizontalAlignment.Left;
            if (horizontalAlignment == HorizontalAlignment.Right)
                return NativeHorizontalAlignment.Right;
            throw new ArgumentOutOfRangeException("horizontalAlignment", "Invalid HorizontalAlignment specified.");
        }
        internal static HorizontalAlignment FromNative(this NativeHorizontalAlignment horizontalAlignment)
        {
            // can't use switch on Android as this is not an enum
            if (horizontalAlignment == NativeHorizontalAlignment.Center)
                return HorizontalAlignment.Center;
            if (horizontalAlignment == NativeHorizontalAlignment.Left)
                return HorizontalAlignment.Left;
            if (horizontalAlignment == NativeHorizontalAlignment.Right)
                return HorizontalAlignment.Right;
            throw new ArgumentOutOfRangeException("horizontalAlignment", "Invalid HorizontalAlignment specified.");
        }

        internal static NativeVerticalAlignment ToNative(this VerticalAlignment verticalAlignment)
        {
            // can't use switch on Android as this is not an enum
            if (verticalAlignment == VerticalAlignment.Center)
                return NativeVerticalAlignment.Center;
            if (verticalAlignment == VerticalAlignment.Top)
                return NativeVerticalAlignment.Top;
            if (verticalAlignment == VerticalAlignment.Bottom)
                return NativeVerticalAlignment.Bottom;
            throw new ArgumentOutOfRangeException("verticalAlignment", "Invalid VerticalAlignment specified.");
        }
        internal static VerticalAlignment FromNative(this NativeVerticalAlignment verticalAlignment)
        {
            // can't use switch on Android as this is not an enum
            if (verticalAlignment == NativeVerticalAlignment.Center)
                return VerticalAlignment.Center;
            if (verticalAlignment == NativeVerticalAlignment.Top)
                return VerticalAlignment.Top;
            if (verticalAlignment == NativeVerticalAlignment.Bottom)
                return VerticalAlignment.Bottom;
            throw new ArgumentOutOfRangeException("verticalAlignment", "Invalid VerticalAlignment specified.");
        }

        private static TPortable FromNative<TPortable, TNative>(this TNative native, ConstructorCollection constructors, TypeInfo[] types)
            where TPortable : class
            where TNative : class
        {
            var nativeType = native.GetType();
            var constructor = constructors.GetOrAdd(nativeType, t => GetConstructor(types, t));
            if (constructor == null)
            {
                throw new ArgumentException(string.Format("No matching portable type was found for {0}", nativeType.FullName), "native");
            }
            return (TPortable)constructor.Invoke(new[] { native });
        }

        private static ConstructorInfo GetConstructor(TypeInfo[] types, Type nativeParameterType)
        {
            foreach (var elem in types)
            {
                foreach (var constructor in elem.DeclaredConstructors)
                {
                    var parameters = constructor.GetParameters();
                    if (parameters.Length == 1 && parameters[0].ParameterType == nativeParameterType)
                    {
                        return constructor;
                    }
                }
            }

            return null;
        }

        public static ElementData FromNative(this NativeElementData native)
        {
            return FromNative<ElementData, NativeElementData>(native, dataConstructors, elementDataTypes);
        }
        public static Element FromNative(this NativeElement native)
        {
            return FromNative<Element, NativeElement>(native, elementConstructors, elementTypes);
        }
        public static Panel FromNative(this NativePanel native)
        {
            return FromNative<Panel, NativePanel>(native, elementConstructors, elementTypes);
        }
#endif
    }
}
