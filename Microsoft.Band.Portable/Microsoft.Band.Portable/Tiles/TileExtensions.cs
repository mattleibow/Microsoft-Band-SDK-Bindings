using Microsoft.Band.Portable.Tiles;
using Microsoft.Band.Portable.Notifications;
using Microsoft.Band.Portable.Personalization;

#if __ANDROID__
using NativeBandTile = Microsoft.Band.Tiles.BandTile;
using NativeBandIcon = Microsoft.Band.Tiles.BandIcon;
#elif __IOS__
using NativeBandTile = Microsoft.Band.Tiles.BandTile;
using NativeBandIcon = Microsoft.Band.Tiles.BandIcon;
#elif WINDOWS_PHONE_APP
using NativeBandTile = Microsoft.Band.Tiles.BandTile;
using NativeBandIcon = Microsoft.Band.Tiles.BandIcon;
#endif

namespace Microsoft.Band.Portable
{
    internal static class TileExtensions
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        public static NativeBandTile ToNative(this BandTile tile)
        {
#if __ANDROID__
            var icon = NativeBandIcon.ToBandIcon(tile.Icon.ToNative());
            using (var builder = new NativeBandTile.Builder(tile.Id.ToNative(), tile.Name, icon))
            {
                if (tile.Theme != null)
                {
                    builder.SetTheme(tile.Theme.ToNative());
                }
                if (tile.SmallIcon != null)
                {
                    icon = NativeBandIcon.ToBandIcon(tile.SmallIcon.ToNative());
                    builder.SetTileSmallIcon(icon);
                }
                return builder.Build();
            }
#elif __IOS__
            // TODO: iOS - SmallIcon may not be optional
            Foundation.NSError error;
            var icon = NativeBandIcon.FromUIImage(tile.Icon.ToNative(), out error);
            var smallIcon = tile.SmallIcon == null ? null : NativeBandIcon.FromUIImage(tile.SmallIcon.ToNative(), out error);
            var bandTile = NativeBandTile.Create(tile.Id.ToNative(), tile.Name, icon, smallIcon, out error);
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.ToNative();
            }
            return bandTile;
#elif WINDOWS_PHONE_APP
            var bandTile = new NativeBandTile(tile.Id.ToNative())
            {
                Name = tile.Name,
                TileIcon = tile.Icon.ToNative().ToBandIcon()
            };
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.ToNative();
            }
            if (tile.SmallIcon != null)
            {
                bandTile.SmallIcon = tile.SmallIcon.ToNative().ToBandIcon();
                bandTile.IsBadgingEnabled = true;
            }
            return bandTile;
#endif
        }

        public static BandTile FromNative(this NativeBandTile tile)
        {
#if __ANDROID__
            var bandTile = new BandTile(tile.TileId.FromNative())
            {
                Name = tile.TileName,
                Icon = BandImage.FromBitmap(tile.TileIcon.Icon)
            };
            if (tile.TileSmallIcon != null)
            {
                bandTile.SmallIcon = BandImage.FromBitmap(tile.TileSmallIcon.Icon);
            }
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.FromNative();
            }
            return bandTile;
#elif __IOS__
            var bandTile = new BandTile(tile.TileId.FromNative())
            {
                Name = tile.Name,
                Icon = BandImage.FromUIImage(tile.TileIcon.UIImage)
            };
            if (tile.SmallIcon != null)
            {
                bandTile.SmallIcon = BandImage.FromUIImage(tile.SmallIcon.UIImage);
            }
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.FromNative();
            }
            return bandTile;
#elif WINDOWS_PHONE_APP
            var bandTile = new BandTile(tile.TileId.FromNative())
            {
                Name = tile.Name,
                Icon = BandImage.FromWriteableBitmap(tile.TileIcon.ToWriteableBitmap())
            };
            if (tile.SmallIcon != null)
            {
                bandTile.SmallIcon = BandImage.FromWriteableBitmap(tile.SmallIcon.ToWriteableBitmap());
            }
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.FromNative();
            }
            return bandTile;
#endif
        }

#endif
    }
}
