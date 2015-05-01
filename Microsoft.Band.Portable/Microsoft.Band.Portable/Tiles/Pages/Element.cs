using System;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeElement = Microsoft.Band.Tiles.Pages.PageElement;
#endif

namespace Microsoft.Band.Portable.Tiles.Pages
{
    public abstract class Element
    {
        private Rectangle rectangle;

        public Element()
        {
            ElementId = -1;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Visible = true;
            Margins = Margins.Empty;
            Rectangle = Rectangle.Empty;
        }

        public short ElementId { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public bool Visible { get; set; }
        public Margins Margins { get; set; }
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Point Location
        {
            get { return rectangle.Location; }
            set { rectangle.Location = value; }
        }
        public Size Size
        {
            get { return rectangle.Size; }
            set { rectangle.Size = value; }
        }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal Element(NativeElement native)
        {
            ElementId = (short)native.ElementId;
            Rectangle = native.Rect.FromNative();
            HorizontalAlignment = native.HorizontalAlignment.FromNative();
            Margins = native.Margins.FromNative();
            VerticalAlignment = native.VerticalAlignment.FromNative();
            Visible = native.Visible;
        }

        internal NativeElement ToNative()
        {
            return ToNative(null);
        }
        internal virtual NativeElement ToNative(NativeElement element)
        {
            var native = EnsureDerived<NativeElement>(element, false);
            if (ElementId > 0)
            {
#if __ANDROID__ || __IOS__
                native.ElementId = (ushort)ElementId;
#elif WINDOWS_PHONE_APP
                native.ElementId = ElementId;
#endif
            }
            if (Rectangle != Rectangle.Empty)
            {
                native.Rect = Rectangle.ToNative();
            }
            native.HorizontalAlignment = HorizontalAlignment.ToNative();
            if (Margins != Margins.Empty)
            {
                native.Margins = Margins.ToNative();
            }
            native.VerticalAlignment = VerticalAlignment.ToNative();
            native.Visible = Visible;
            return native;
        }

        protected T EnsureDerived<T>(NativeElement element)
            where T : NativeElement
        {
            return EnsureDerived<T>(element, true);
        }
        protected T EnsureDerived<T>(NativeElement element, bool allowNull)
            where T : NativeElement
        {
            if (element == null)
            {
                if (allowNull)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentNullException("element");
                }
            }

            var specific = element as T;
            if (specific == null)
            {
                throw new ArgumentException("element", string.Format("element must be of type {0} or a derived type.", typeof(T).FullName));
            }

            return specific;
        }
#endif
    }
}