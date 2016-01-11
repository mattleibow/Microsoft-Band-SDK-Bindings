using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

using Plugin.Media;
using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Tiles;

using Microsoft.Band.Portable.Tiles.Pages;
using Microsoft.Band.Portable.Tiles.Pages.Data;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class AddTileViewModel : BaseClientViewModel
    {
        private BandTileManager tileManager;

        private Guid tileId;
        private string tileName;
        private bool useCustomTheme;
        private bool disableScreenTimeout;
        private BandImage tileIcon;
        private BandImage tileBadge;
        private BandTheme tileTheme;

        public AddTileViewModel(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            tileManager = bandClient.TileManager;
            tileTheme = App.DefaultTheme;
            tileId = Guid.NewGuid();
            tileName = "New Tile";

            GenerateTileIdCommand = new Command(() =>
            {
                TileId = Guid.NewGuid().ToString("D");
            });

            DefaultTileIconCommand = new Command(async () =>
            {
                TileIcon = await App.LoadImageResourceAsync("Resources/tile.png");
            });
            SelectTileIconCommand = new Command(async () =>
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                {
                    TileIcon = await BandImage.FromStreamAsync(photo.GetStream());
                }
            }, () => CrossMedia.Current.IsPickPhotoSupported);

            DefaultTileBadgeCommand = new Command(async () =>
            {
                TileBadge = await App.LoadImageResourceAsync("Resources/badge.png");
            });
            SelectTileBadgeCommand = new Command(async () =>
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                {
                    TileBadge = await BandImage.FromStreamAsync(photo.GetStream());
                }
            }, () => CrossMedia.Current.IsPickPhotoSupported);

            DefaultThemeCommand = new Command(() => 
            {
                TileTheme = App.DefaultTheme;
            });

            AddTileCommand = new Command(async () =>
            {
                var tile = new BandTile(tileId)
                {
                    Icon = TileIcon,
                    Name = TileName,
                    SmallIcon = TileBadge,
                    IsScreenTimeoutDisabled = DisableScreenTimeout
                };
                if (UseCustomTheme)
                {
                    tile.Theme = TileTheme;
                }
                tile.PageImages.AddRange(new[]
                {
                    await App.LoadImageResourceAsync("Resources/star.png")
                });
                var layouts = CreatePageLayouts();
                tile.PageLayouts.AddRange(layouts);
                await tileManager.AddTileAsync(tile);
                var datas = CreatePageDatas();
                await tileManager.SetTilePageDataAsync(tile.Id, datas);
            });
            RemoveTileCommand = new Command(async () =>
            {
                await tileManager.RemoveTileAsync(tileId);
            });
        }

        public AddTileViewModel(BandDeviceInfo info, BandClient bandClient, BandTile tile)
            : this(info, bandClient)
        {
            TileId = tile.Id.ToString("D");
            TileName = tile.Name;
            TileIcon = tile.Icon;
            TileBadge = tile.SmallIcon;
            DisableScreenTimeout = tile.IsScreenTimeoutDisabled;
            if (tile.Theme != BandTheme.Empty) 
            {
                UseCustomTheme = true;
                TileTheme = tile.Theme;
            }
            else
            {
                TileTheme = App.DefaultTheme;
            }
        }

        public string TileId
        {
            get { return tileId.ToString("D"); }
            set
            {
                tileId = Guid.Parse(value);
                OnPropertyChanged("TileId");
            }
        }

        public string TileName
        {
            get { return tileName; }
            set
            {
                if (tileName != value)
                {
                    tileName = value;
                    OnPropertyChanged("TileName");
                }
            }
        }

        public bool DisableScreenTimeout
        {
            get { return disableScreenTimeout; }
            set
            {
                if (disableScreenTimeout != value)
                {
                    disableScreenTimeout = value;
                    OnPropertyChanged("DisableScreenTimeout");
                }
            }
        }

        public BandImage TileIcon
        {
            get { return tileIcon; }
            set
            {
                if (tileIcon != value)
                {
                    tileIcon = value;
                    OnPropertyChanged("TileIcon");
                }
            }
        }

        public BandImage TileBadge
        {
            get { return tileBadge; }
            set
            {
                if (tileBadge != value)
                {
                    tileBadge = value;
                    OnPropertyChanged("TileBadge");
                }
            }
        }

        public bool UseCustomTheme
        {
            get { return useCustomTheme; }
            set
            {
                if (useCustomTheme != value)
                {
                    useCustomTheme = value;
                    OnPropertyChanged("UseCustomTheme");
                }
            }
        }

        public BandTheme TileTheme
        {
            get { return tileTheme; }
            set
            {
                if (tileTheme != value)
                {
                    tileTheme = value;
                    OnPropertyChanged("TileTheme");
                }
            }
        }

        public ICommand GenerateTileIdCommand { get; private set; }
        public ICommand DefaultTileIconCommand { get; private set; }
        public ICommand SelectTileIconCommand { get; private set; }
        public ICommand DefaultTileBadgeCommand { get; private set; }
        public ICommand SelectTileBadgeCommand { get; private set; }
        public ICommand DefaultThemeCommand { get; private set; }
        public ICommand AddTileCommand { get; private set; }
        public ICommand RemoveTileCommand { get; private set; }

        private static PageLayout[] CreatePageLayouts()
        {
            return new[] {
                // page index layout index 0 - BARCODES
                new PageLayout {
                    Root = new ScrollFlowPanel {
                        Rect = new PageRect(0, 0, 245, 105),
                        Orientation = FlowPanelOrientation.Vertical,
                        Elements = {
                            new TextBlock {
                                ElementId = TilePages.BarcodePage.TextBlockTitleId,
                                Rect = new PageRect(0, 0, 230, 30),
                                ColorSource = ElementColorSource.BandBase,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new TextBlock {
                                ElementId = TilePages.BarcodePage.TextBlock39Id,
                                Rect = new PageRect(0, 0, 230, 30),
                                Color = new BandColor(255, 0, 0),
                                AutoWidth = false,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new Barcode {
                                ElementId = TilePages.BarcodePage.Barcode39Id,
                                Rect = new PageRect(0, 0, 230, 61),
                                BarcodeType = BarcodeType.Code39,
                            },
                            new TextBlock {
                                ElementId = TilePages.BarcodePage.TextBlock417Id,
                                Rect = new PageRect(0, 0, 230, 30),
                                Color = new BandColor(255, 0, 0),
                                AutoWidth = false,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new Barcode {
                                ElementId = TilePages.BarcodePage.Barcode417Id,
                                Rect = new PageRect(0, 0, 230, 61),
                                BarcodeType = BarcodeType.Pdf417,
                            }
                        }
                    }
                },
                //page layout index 1 - IMAGES
                new PageLayout {
                    Root = new FlowPanel {
                        Rect = new PageRect(15, 0, 245, 105),
                        Orientation = FlowPanelOrientation.Vertical,
                        Elements = {
                            new TextBlock {
                                ElementId = TilePages.ImagePage.TextBlockTitleId,
                                Rect = new PageRect(0, 0, 230, 30),
                                ColorSource = ElementColorSource.BandBase,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new FlowPanel {
                                Rect = new PageRect(0, 0, 230, 105),
                                Orientation = FlowPanelOrientation.Horizontal,
                                Elements = {
                                    new Icon {
                                        ElementId = TilePages.ImagePage.ImageId1,
                                        Rect = new PageRect(0, 0, 100, 70),
                                        Color = new BandColor(127, 127, 0),
                                        VerticalAlignment = VerticalAlignment.Center,
                                        HorizontalAlignment = HorizontalAlignment.Center
                                    },
                                    new Icon {
                                        ElementId = TilePages.ImagePage.ImageId2,
                                        Rect = new PageRect(0, 0, 100, 70),
                                        Color = new BandColor(127, 0, 127),
                                        VerticalAlignment = VerticalAlignment.Center,
                                        HorizontalAlignment = HorizontalAlignment.Center
                                    }
                                }
                            }
                        }
                    }
                },
                // page layout index 2 - BUTTONS
                new PageLayout {
                    Root = new ScrollFlowPanel {
                        Rect = new PageRect(0, 0, 245, 105),
                        Orientation = FlowPanelOrientation.Vertical,
                        Elements = {
                            new TextBlock {
                                ElementId = TilePages.ButtonPage.TextBlockTitleId,
                                Rect = new PageRect(0, 0, 229, 30),
                                ColorSource = ElementColorSource.BandBase,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new TextBlock {
                                ElementId = TilePages.ButtonPage.TextBlockId,
                                Rect = new PageRect(0, 0, 229, 30),
                                Color = new BandColor(127, 127, 0),
                            },
                            new TextButton {
                                ElementId = TilePages.ButtonPage.ButtonId,
                                Rect = new PageRect(0, 0, 229, 43),
                                PressedColor = new BandColor(0, 127, 0)
                            },
                            new TextBlock {
                                ElementId = TilePages.ButtonPage.TextBlockFilledId,
                                Rect = new PageRect(0, 0, 229, 30),
                                Color = new BandColor(0, 127, 127),
                            },
                            new FilledButton {
                                ElementId = TilePages.ButtonPage.ButtonFilledId,
                                Rect = new PageRect(0, 0, 229, 43),
                                BackgroundColor = new BandColor(0, 0, 127)
                            }
                        }
                    }
                }
            };
        }

        private static IEnumerable<PageData> CreatePageDatas()
        {
            return new[] {
                // page data index 0 - BARCODES
                new PageData {
                    PageId = TilePages.BarcodePage.Id,
                    PageLayoutIndex = 0,
                    Data = {
                        new TextBlockData {
                            ElementId = TilePages.BarcodePage.TextBlockTitleId,
                            Text = "Barcodes"
                        },
                        new TextBlockData {
                            ElementId = TilePages.BarcodePage.TextBlock39Id,
                            Text = "Code 39: 'HELLO'"
                        },
                        new BarcodeData {
                            ElementId = TilePages.BarcodePage.Barcode39Id,
                            BarcodeType = BarcodeType.Code39,
                            BarcodeValue = "HELLO"
                        },
                        new TextBlockData {
                            ElementId = TilePages.BarcodePage.TextBlock417Id,
                            Text = "Pdf 417: '0246810'"
                        },
                        new BarcodeData {
                            ElementId = TilePages.BarcodePage.Barcode417Id,
                            BarcodeType = BarcodeType.Pdf417,
                            BarcodeValue = "0246810"
                        }
                    }
                },
                // page data index 1 - IMAGES
                new PageData {
                    PageId = TilePages.ImagePage.Id,
                    PageLayoutIndex = 1,
                    Data = {
                        new TextBlockData {
                            ElementId = TilePages.ImagePage.TextBlockTitleId,
                            Text = "Page Images"
                        },
                        new ImageData {
                            ElementId = TilePages.ImagePage.ImageId1,
                            ImageIndex = 0
                        },
                        new ImageData {
                            ElementId = TilePages.ImagePage.ImageId2,
                            ImageIndex = 2
                        }
                    }
                },
                // page data index 2 - BUTTONS
                new PageData {
                    PageId = TilePages.ButtonPage.Id,
                    PageLayoutIndex = 2,
                    Data = {
                        new TextBlockData {
                            ElementId = TilePages.ButtonPage.TextBlockTitleId,
                            Text = "Buttons"
                        },
                        new TextBlockData { 
                            ElementId = TilePages.ButtonPage.TextBlockId,
                            Text = "Text button"
                        },
                        new TextButtonData {
                            ElementId = TilePages.ButtonPage.ButtonId,
                            Text = "Press Me!"
                        },
                        new TextBlockData { 
                            ElementId = TilePages.ButtonPage.TextBlockFilledId,
                            Text = "Filled button"
                        },
                        new FilledButtonData {
                            ElementId = TilePages.ButtonPage.ButtonFilledId,
                            PressedColor = new BandColor(0, 127, 127)
                        }
                    }
                }
            };
        }

        private static class TilePages
        {
            public static class ImagePage
            {
                public static Guid Id = Guid.NewGuid();
                public static short TextBlockTitleId = 49;
                public static short TextBlockId1 = 40;
                public static short ImageId1 = 41;
                public static short TextBlockId2 = 42;
                public static short ImageId2 = 43;
            }
            public static class BarcodePage
            {
                public static Guid Id = Guid.NewGuid();
                public static short TextBlockTitleId = 39;
                public static short TextBlock39Id = 30;
                public static short Barcode39Id = 31;
                public static short TextBlock417Id = 32;
                public static short Barcode417Id = 33;
            }
            public static class ButtonPage
            {
                public static Guid Id = Guid.NewGuid();
                public static short TextBlockTitleId = 29;
                public static short TextBlockId = 20;
                public static short ButtonId = 21;
                public static short TextBlockFilledId = 22;
                public static short ButtonFilledId = 23;
                public static short TextBlockImageId = 24;
                public static short ButtonImageId = 25;
                public static short ImageId = 26;
            }
        }
    }
}
