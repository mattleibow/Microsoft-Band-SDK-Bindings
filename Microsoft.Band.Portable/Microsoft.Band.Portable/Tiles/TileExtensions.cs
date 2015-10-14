using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Band.Portable.Tiles;
using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Tiles.Pages;

#if __ANDROID__
using Android.App;
using Android.Content;
using Android.OS;
#endif

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
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
            var icon = NativeBandIcon.ToBandIcon(tile.Icon.ToBitmap());
            using (var builder = new NativeBandTile.Builder(tile.Id.ToNative(), tile.Name, icon))
            {
                var smallIcon = NativeBandIcon.ToBandIcon(tile.SmallIcon.ToBitmap());
                builder.SetTileSmallIcon(smallIcon);
                var pageIcons = tile.PageImages.Select(pi => NativeBandIcon.ToBandIcon(pi.ToBitmap())).ToArray();
                builder.SetPageIcons(pageIcons);
                var pageLayouts = tile.PageLayouts.Select(pl => pl.ToNative()).ToArray();
                builder.SetPageLayouts(pageLayouts);
                if (tile.IsCustomThemeEnabled)
                {
                    builder.SetTheme(tile.Theme.ToNative());
                }
                builder.SetScreenTimeoutDisabled(tile.IsScreenTimeoutDisabled);
                return builder.Build();
            }
#elif __IOS__
            // TODO: iOS - SmallIcon may not be optional
            Foundation.NSError error;
            var icon = NativeBandIcon.FromUIImage(tile.Icon.ToUIImage(), out error);
            var smallIcon = NativeBandIcon.FromUIImage(tile.SmallIcon.ToUIImage(), out error);
            var bandTile = NativeBandTile.Create(tile.Id.ToNative(), tile.Name, icon, smallIcon, out error);
            bandTile.BadgingEnabled = true;
            foreach (var image in tile.PageImages)
            {
                var pageIcon = NativeBandIcon.FromUIImage(image.ToUIImage(), out error);
                bandTile.PageIcons.Add(pageIcon);
            }
            foreach (var layout in tile.PageLayouts)
            {
                bandTile.PageLayouts.Add(layout.ToNative());
            }
            if (tile.IsCustomThemeEnabled)
            {
                bandTile.Theme = tile.Theme.ToNative();
            }
            bandTile.ScreenTimeoutDisabled = tile.IsScreenTimeoutDisabled;
            return bandTile;
#elif WINDOWS_PHONE_APP
            var bandTile = new NativeBandTile(tile.Id.ToNative())
            {
                Name = tile.Name,
                TileIcon = tile.Icon.ToWriteableBitmap().ToBandIcon(),
                SmallIcon = tile.SmallIcon.ToWriteableBitmap().ToBandIcon(),
                IsBadgingEnabled = true
            };
            foreach (var icon in tile.PageImages)
            {
                bandTile.AdditionalIcons.Add(icon.ToWriteableBitmap().ToBandIcon());
            }
            foreach (var layout in tile.PageLayouts)
            {
                bandTile.PageLayouts.Add(layout.ToNative());
            }
            if (tile.IsCustomThemeEnabled)
            {
                bandTile.Theme = tile.Theme.ToNative();
            }
            bandTile.IsScreenTimeoutDisabled = tile.IsScreenTimeoutDisabled;
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
            if (tile.PageIcons != null)
            {
                bandTile.PageImages.AddRange(tile.PageIcons.Select(pi => BandImage.FromBitmap(pi.Icon)));
            }
            if (tile.PageLayouts != null)
            {
                bandTile.PageLayouts.AddRange(tile.PageLayouts.Select(pl => new PageLayout(pl)));
            }
            if (tile.TileSmallIcon != null)
            {
                bandTile.SmallIcon = BandImage.FromBitmap(tile.TileSmallIcon.Icon);
            }
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.FromNative();
            }
            bandTile.IsScreenTimeoutDisabled = tile.IsScreenTimeoutDisabled;
            return bandTile;
#elif __IOS__
            var bandTile = new BandTile(tile.TileId.FromNative())
            {
                Name = tile.Name,
                Icon = BandImage.FromUIImage(tile.TileIcon.UIImage)
            };
            if (tile.PageIcons != null)
            {
                bandTile.PageImages.AddRange(tile.PageIcons.Select(pi => BandImage.FromUIImage(pi.UIImage)));
            }
            if (tile.PageLayouts != null)
            {
                bandTile.PageLayouts.AddRange(tile.PageLayouts.Select(pl => new PageLayout(pl)));
            }
            if (tile.SmallIcon != null)
            {
                bandTile.SmallIcon = BandImage.FromUIImage(tile.SmallIcon.UIImage);
            }
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.FromNative();
            }
            bandTile.IsScreenTimeoutDisabled = tile.ScreenTimeoutDisabled;
            return bandTile;
#elif WINDOWS_PHONE_APP
            var bandTile = new BandTile(tile.TileId.FromNative())
            {
                Name = tile.Name,
                Icon = BandImage.FromWriteableBitmap(tile.TileIcon.ToWriteableBitmap())
            };
            if (tile.AdditionalIcons != null)
            {
                bandTile.PageImages.AddRange(tile.AdditionalIcons.Select(pi => BandImage.FromWriteableBitmap(pi.ToWriteableBitmap())));
            }
            if (tile.PageLayouts != null)
            {
                bandTile.PageLayouts.AddRange(tile.PageLayouts.Select(pl => new PageLayout(pl)));
            }
            if (tile.SmallIcon != null)
            {
                bandTile.SmallIcon = BandImage.FromWriteableBitmap(tile.SmallIcon.ToWriteableBitmap());
            }
            if (tile.Theme != null)
            {
                bandTile.Theme = tile.Theme.FromNative();
            }
            bandTile.IsScreenTimeoutDisabled = tile.IsScreenTimeoutDisabled;
            return bandTile;
#endif
        }

#endif
    }

#if __ANDROID__
    internal class ActivityWrappedActionExtensions
    {
        // delegate type that is the listener/callback
        public delegate Task WrappedActionDelegate(Activity activity);
        public delegate Task<bool> ActionDelegate(Activity activity);

        // extra key for the intent
        private const string ActionIdExtra = "ACTION_ID";

        // collection to hold the callbacks
        private static ConcurrentDictionary<string, WrappedActionDelegate> listeners =
            new ConcurrentDictionary<string, WrappedActionDelegate>();

        public static Task<bool> WrapActionAsync(ActionDelegate action)
        {
            var tcs = new TaskCompletionSource<bool>();

            var actionId = Guid.NewGuid().ToString();

            // show the add tile dialog
            listeners.TryAdd(actionId, async activity =>
            {
                try
                {
                    // show the dialog
                    if (activity != null)
                    {
                        // show the dialog and add the tile
                        var result = await action(activity);

                        // end the waiting
                        tcs.SetResult(result);
                    }
                    else
                    {
                        // end the waiting
                        tcs.SetException(new ArgumentNullException("activity", "Specified action requires an Activity, which became unavailable."));
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
            var intent = new Intent(context, typeof(WrappedActionActivity));
            intent.PutExtra(ActionIdExtra, actionId);
            intent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);

            // wait for the delegate to be fired
            return tcs.Task;
        }

        // the empty activity that will show the dialog
        [Activity(Theme = "@android:style/Theme.DeviceDefault.Panel")]
        private class WrappedActionActivity : Activity
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
                // get the action ID
                var actionId = Intent.GetStringExtra(ActionIdExtra);

                // get and fire the delegate
                WrappedActionDelegate target;
                if (listeners.TryRemove(actionId, out target))
                {
                    // show the dialog and complete the operation
                    await target(activity);
                }
            }
        }
    }
#endif
}
