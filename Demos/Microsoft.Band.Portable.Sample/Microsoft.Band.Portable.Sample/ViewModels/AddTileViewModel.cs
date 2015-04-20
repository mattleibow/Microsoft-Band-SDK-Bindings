using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

using Media.Plugin;
using Xamarin.Forms;

using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Tiles;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class AddTileViewModel : BaseClientViewModel
    {
        private BandTileManager tileManager;

        private Guid tileId;
        private string tileName;
        private bool allowBadging;
        private bool useCustomTheme;
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
                    Name = TileName
                };
                if (AllowBadging)
                {
                    tile.SmallIcon = TileBadge;
                }
                if (UseCustomTheme)
                {
                    tile.Theme = TileTheme;
                }
                await tileManager.AddTileAsync(tile);
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
            AllowBadging = tile.SmallIcon != null;
            TileBadge = tile.SmallIcon;
            if (tile.Theme != default(BandTheme)) 
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

        public bool AllowBadging
        {
            get { return allowBadging; }
            set
            {
                if (allowBadging != value)
                {
                    allowBadging = value;
                    OnPropertyChanged("AllowBadging");
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
    }
}
