using Microsoft.Band.Portable.Tiles;
using Microsoft.Band.Portable.Notifications;
using Microsoft.Band.Portable.Personalization;

#if __ANDROID__
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using NativeBandTile = Microsoft.Band.Tiles.BandTile;
using NativeBandIcon = Microsoft.Band.Tiles.BandIcon;
using BandTileManagerExtensions = Microsoft.Band.Tiles.BandTileManagerExtensions;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
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

#if __ANDROID__
        // delegate type that is the listener/callback
        private delegate Task AddTileDelegate(Activity activity);

        // extra key for the intent
        private const string TileIdExtra = "TILE_ID";

        // collection to hold the callbacks
        private static ConcurrentDictionary<string, AddTileDelegate> listeners =
            new ConcurrentDictionary<string, AddTileDelegate>();

        public static Task<bool> AddTileAsync(this NativeBandTileManager tileManager, BandTile tile)
        {
            var tcs = new TaskCompletionSource<bool>();

            // show the add tile dialog
            listeners.TryAdd(tile.Id.ToString(), async activity =>
            {
                try
                {
                    // show the dialog
                    if (activity != null)
                    {
                        // show the dialog and add the tile
                        var result = await BandTileManagerExtensions.AddTileTaskAsync(tileManager, activity, tile.ToNative());

                        // end the waiting
                        tcs.SetResult(result);
                    }
                    else
                    {
                        // end the waiting
                        tcs.SetResult(false);
                    }
                }
                catch (Exception ex)
                {
                    // end the waiting
                    tcs.SetException(ex);
                }
            });

            // start the activity
            var context = Application.Context;
            var intent = new Intent(context, typeof(AddTileActivity));
            intent.PutExtra(TileIdExtra, tile.Id.ToString());
            intent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);

            // wait for the delegate to be fired
            return tcs.Task;
        }

        // the empty activity that will show the dialog
        [Activity(Theme = "@android:style/Theme.DeviceDefault.Panel")]
        private class AddTileActivity : Activity
        {
            protected override async void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                // trigger the delegate
                await TriggerDelegate(this);

                // end the activity
                Finish();
            }

            protected override async void OnDestroy()
            {
                base.OnDestroy();

                // trigger the delegate
                await TriggerDelegate(this);
            }

            private async Task TriggerDelegate(Activity activity)
            {
                // get the tile ID
                var tileId = Intent.GetStringExtra(TileIdExtra);

                // get and fire the delegate
                AddTileDelegate target;
                if (listeners.TryRemove(tileId, out target))
                {
                    // show the dialog and complete the operation
                    await target(activity);
                }
            }
        }
#endif
    }
}
