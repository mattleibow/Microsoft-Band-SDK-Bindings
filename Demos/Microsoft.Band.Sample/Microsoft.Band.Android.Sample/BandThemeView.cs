//Copyright (c) Microsoft Corporation All rights reserved.  
// 
//MIT License: 
// 
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//documentation files (the  "Software"), to deal in the Software without restriction, including without limitation
//the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// 
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of
//the Software. 
// 
//THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//IN THE SOFTWARE.

using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using Genetics;
using Genetics.Attributes;

using Microsoft.Band.Sample.ColorPicker;

namespace Microsoft.Band.Sample
{
    public class BandThemeView : TableLayout
    {
        private const string BASE_COLOR_NAME = "Base";
        private const string HIGHLIGHT_COLOR_NAME = "Highlight";
        private const string LOWLIGHT_COLOR_NAME = "Lowlight";
        private const string SECONDARY_TEXT_COLOR_NAME = "SecondaryText";
        private const string HIGH_CONTRAST_COLOR_NAME = "HighContrast";
        private const string MUTED_COLOR_NAME = "Muted";

        private BandTheme mTheme;

        public BandThemeView(Context context) 
            : base(context)
        {
            Init();
        }

        public BandThemeView(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
            Init();
        }

        private void Init()
        {
            var inflater = LayoutInflater.From(Context);
            inflater.Inflate(Resource.Layout.view_bandtheme, this);

            Geneticist.Splice(this);
        }

        [SpliceClick(Resource.Id.buttonChangeBase)]
        [SpliceClick(Resource.Id.buttonChangeHighlight)]
        [SpliceClick(Resource.Id.buttonChangeLowlight)]
        [SpliceClick(Resource.Id.buttonChangeSecondaryText)]
        [SpliceClick(Resource.Id.buttonChangeHighContrast)]
        [SpliceClick(Resource.Id.buttonChangeMuted)]
        private void OnButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            ShowColorPicker((string) button.Tag);
        }

        public virtual BandTheme Theme
        {
            set
            {
                mTheme = value;
                SetColorForThemeElement(BASE_COLOR_NAME, value.Base);
                SetColorForThemeElement(HIGHLIGHT_COLOR_NAME, value.Highlight);
                SetColorForThemeElement(LOWLIGHT_COLOR_NAME, value.Lowlight);
                SetColorForThemeElement(SECONDARY_TEXT_COLOR_NAME, value.SecondaryText);
                SetColorForThemeElement(HIGH_CONTRAST_COLOR_NAME, value.HighContrast);
                SetColorForThemeElement(MUTED_COLOR_NAME, value.Muted);
            }
            get { return mTheme; }
        }

        private void ShowColorPicker(string key)
        {
            var dialog = new ColorPickerDialog(Context);
                dialog.SetInitialColor(GetColorForThemeElement(key));
                dialog.ColorSelected += (sender, e) =>
                {
                    if (BASE_COLOR_NAME.Equals(key))
                    {
                        mTheme.Base = e.Color;
                    }
                    else if (HIGHLIGHT_COLOR_NAME.Equals(key))
                    {
                        mTheme.Highlight = e.Color;
                    }
                    else if (LOWLIGHT_COLOR_NAME.Equals(key))
                    {
                        mTheme.Lowlight = e.Color;
                    }
                    else if (HIGH_CONTRAST_COLOR_NAME.Equals(key))
                    {
                        mTheme.HighContrast = e.Color;
                    }
                    else if (SECONDARY_TEXT_COLOR_NAME.Equals(key))
                    {
                        mTheme.SecondaryText = e.Color;
                    }
                    else if (MUTED_COLOR_NAME.Equals(key))
                    {
                        mTheme.Muted = e.Color;
                    }

                    SetColorForThemeElement(key, e.Color);
                };
                dialog.Show();
        }

        // Get the background color for the named theme element (Base, Highlight, etc.)
        private Color GetColorForThemeElement(string element)
        {
            var grid = (GridLayout)FindViewWithTag("grid" + element);
            return ((ColorDrawable)grid.Background).Color;
        }

        // Set the background color for the named theme element (Base, Highlight, etc.)
        private void SetColorForThemeElement(string element, Color color)
        {
            var grid = (GridLayout)FindViewWithTag("grid" + element);
            if (grid != null)
            {
                grid.SetBackgroundColor(color);
            }
        }
    }
}