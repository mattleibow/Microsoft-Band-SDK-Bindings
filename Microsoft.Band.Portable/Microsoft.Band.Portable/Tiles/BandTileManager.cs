using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Band.Portable.Tiles.Pages;
using Microsoft.Band.Portable.Tiles.Pages.Data;

#if __ANDROID__
using Android.App;
using Android.Content;
using Microsoft.Band.Tiles;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
#elif __IOS__
using Microsoft.Band.Tiles;
using NativeBandClient = Microsoft.Band.BandClient;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Tiles;
using NativeBandTileManager = Microsoft.Band.Tiles.IBandTileManager;
using NativeTileButtonPressedEventArgs = Microsoft.Band.Tiles.BandTileEventArgs<Microsoft.Band.Tiles.IBandTileButtonPressedEvent>;
using NativeTileOpenedEventArgs = Microsoft.Band.Tiles.BandTileEventArgs<Microsoft.Band.Tiles.IBandTileOpenedEvent>;
using NativeTileClosedEventArgs = Microsoft.Band.Tiles.BandTileEventArgs<Microsoft.Band.Tiles.IBandTileClosedEvent>;
#endif

namespace Microsoft.Band.Portable.Tiles
{
    public class BandTileManager
    {
        private readonly BandClient client;
        private readonly object subscribedLock = new object();

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandTileManager Native;

#if __ANDROID__
        private readonly BandTileBroadcastReceiver tileReceiver;
#elif __IOS__
        private readonly BandTileDelegate tileDelegate;
#endif

        internal BandTileManager(BandClient client, NativeBandTileManager tileManager)
        {
            this.Native = tileManager;

            this.client = client;
            
#if __ANDROID__
            this.tileReceiver = new BandTileBroadcastReceiver(
                e => OnTileOpened(new BandTileOpenedEventArgs(e)), 
                e => OnTileClosed(new BandTileClosedEventArgs(e)),
                e => OnTileButtonPressed(new BandTileButtonPressedEventArgs(e)));
#elif __IOS__
            this.tileDelegate = new BandTileDelegate(
                e => OnTileOpened(new BandTileOpenedEventArgs(e)),
                e => OnTileClosed(new BandTileClosedEventArgs(e)),
                e => OnTileButtonPressed(new BandTileButtonPressedEventArgs(e)));
#elif WINDOWS_PHONE_APP
            this.Native.TileButtonPressed += OnNativeTileButtonPressed;
            this.Native.TileOpened += OnNativeTileOpened;
            this.Native.TileClosed += OnNativeTileClosed;
#endif
        }
#endif
        
        public async Task<bool> AddTileAsync(BandTile tile)
        {
            bool result = false;
#if __ANDROID__
            result = await ActivityWrappedActionExtensions.WrapActionAsync(activity =>
            {
                return Native.AddTileTaskAsync(activity, tile.ToNative());
            });
#elif __IOS__
            try
            {
                await Native.AddTileTaskAsync(tile.ToNative());
                result = true;
            }
            catch (BandException ex)
            {
                if (ex.Code == (int)BandNSErrorCodes.UserDeclinedTile)
                {
                    result = false;
                }
                else 
                {
                    throw;
                }
            }
#elif WINDOWS_PHONE_APP
            result = await Native.AddTileAsync(tile.ToNative());
#endif
            return result;
        }

        public async Task<int> GetRemainingTileCapacityAsync()
        {
#if __ANDROID__ || __IOS__
            return (int)await Native.GetRemainingTileCapacityTaskAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetRemainingTileCapacityAsync();
#else // PORTABLE
            return default(int);
#endif
        }

        public async Task<IEnumerable<BandTile>> GetTilesAsync()
        {
#if __ANDROID__ || __IOS__
            return (await Native.GetTilesTaskAsync()).Select(x => x.FromNative());
#elif WINDOWS_PHONE_APP
            return (await Native.GetTilesAsync()).Select(x => x.FromNative());
#else // PORTABLE
            return null;
#endif
        }

        public async Task RemoveTileAsync(Guid tileId)
        {
#if __ANDROID__ || __IOS__
            await Native.RemoveTileTaskAsync(tileId.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.RemoveTileAsync(tileId.ToNative());
#endif
        }

        public async Task RemoveTilePagesAsync(Guid tileId)
        {
#if __ANDROID__ || __IOS__
            await Native.RemovePagesTaskAsync(tileId.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.RemovePagesAsync(tileId.ToNative());
#endif
        }

        public async Task SetTilePageDataAsync(Guid tileId, IEnumerable<PageData> pageData)
        {
#if __ANDROID__
            await Native.SetPagesTaskAsync(tileId.ToNative(), pageData.Select(pd => pd.ToNative()).ToArray());
#elif __IOS__
            await Native.SetPagesTaskAsync(pageData.Select(pd => pd.ToNative()).ToArray(), tileId.ToNative());
#elif WINDOWS_PHONE_APP
            await Native.SetPagesAsync(tileId.ToNative(), pageData.Select(pd => pd.ToNative()).ToArray());
#endif
        }

        public Task SetTilePageDataAsync(Guid tileId, params PageData[] pageData)
        {
            return SetTilePageDataAsync(tileId, (IEnumerable<PageData>)pageData);
        }

        public async Task StartEventListenersAsync()
        {
#if __ANDROID__
            var filter = new IntentFilter();
            filter.AddAction(TileEvent.ActionTileOpened);
            filter.AddAction(TileEvent.ActionTileButtonPressed);
            filter.AddAction(TileEvent.ActionTileClosed);
            Application.Context.RegisterReceiver(tileReceiver, filter);
#elif __IOS__
            client.Native.TileDelegate = tileDelegate;
#elif WINDOWS_PHONE_APP
            await Native.StartReadingsAsync();
#endif
        }

        public async Task StopEventListenersAsync()
        {
#if __ANDROID__
            Application.Context.UnregisterReceiver(tileReceiver);
#elif __IOS__
            client.Native.TileDelegate = null;
#elif WINDOWS_PHONE_APP
            await Native.StopReadingsAsync();
#endif
        }
        
        protected virtual void OnTileButtonPressed(BandTileButtonPressedEventArgs e)
        {
            var handler = TileButtonPressed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnTileOpened(BandTileOpenedEventArgs e)
        {
            var handler = TileOpened;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnTileClosed(BandTileClosedEventArgs e)
        {
            var handler = TileClosed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<BandTileOpenedEventArgs> TileOpened;

        public event EventHandler<BandTileClosedEventArgs> TileClosed;

        public event EventHandler<BandTileButtonPressedEventArgs> TileButtonPressed;

#if __ANDROID__
        private class BandTileBroadcastReceiver : BroadcastReceiver
        {
            private readonly Action<TileEvent> opened;
            private readonly Action<TileEvent> closed;
            private readonly Action<TileButtonEvent> buttonPressed;

            public BandTileBroadcastReceiver(Action<TileEvent> opened, Action<TileEvent> closed, Action<TileButtonEvent> buttonPressed)
            {
                this.opened = opened;
                this.closed = closed;
                this.buttonPressed = buttonPressed;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action == TileEvent.ActionTileOpened)
                {
                    var tileEvent = (TileEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
                    opened(tileEvent);
                }
                else if (intent.Action == TileEvent.ActionTileButtonPressed)
                {
                    var tileEvent = (TileButtonEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
                    buttonPressed(tileEvent);
                }
                else if (intent.Action == TileEvent.ActionTileClosed)
                {
                    var tileEvent = (TileEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
                    closed(tileEvent);
                }
            }
        }
#elif __IOS__
        private class BandTileDelegate : BandClientTileDelegate
        {
            private readonly Action<BandTileEvent> opened;
            private readonly Action<BandTileEvent> closed;
            private readonly Action<BandTileButtonEvent> buttonPressed;

            public BandTileDelegate(Action<BandTileEvent> opened, Action<BandTileEvent> closed, Action<BandTileButtonEvent> buttonPressed)
            {
                this.opened = opened;
                this.closed = closed;
                this.buttonPressed = buttonPressed;
            }

            public override void ButtonPressed(NativeBandClient client, BandTileButtonEvent tileButtonEvent)
            {
                buttonPressed(tileButtonEvent);
            }
            public override void TileOpened(NativeBandClient client, BandTileEvent tileEvent)
            {
                opened(tileEvent);
            }
            public override void TileClosed(NativeBandClient client, BandTileEvent tileEvent)
            {
                closed(tileEvent);
            }
        }
#elif WINDOWS_PHONE_APP
        private void OnNativeTileButtonPressed(object sender, NativeTileButtonPressedEventArgs e)
        {
            OnTileButtonPressed(new BandTileButtonPressedEventArgs(e.TileEvent));
        }

        private void OnNativeTileOpened(object sender, NativeTileOpenedEventArgs e)
        {
            OnTileOpened(new BandTileOpenedEventArgs(e.TileEvent));
        }

        private void OnNativeTileClosed(object sender, NativeTileClosedEventArgs e)
        {
            OnTileClosed(new BandTileClosedEventArgs(e.TileEvent));
        }
#endif
    }
}
