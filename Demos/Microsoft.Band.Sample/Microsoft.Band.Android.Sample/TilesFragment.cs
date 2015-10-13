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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Graphics;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Genetics;
using Genetics.Attributes;
using Fragment = Android.Support.V4.App.Fragment;

using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;

namespace Microsoft.Band.Sample
{
    public class TilesFragment : Fragment, FragmentListener
    {
        private int mRemainingCapacity;

        [Splice(Resource.Id.textAvailableCapacity, Optional = true)] private TextView mTextRemainingCapacity;
        [Splice(Resource.Id.buttonAddTile, Optional = true)] private Button mButtonAddTile;
        [Splice(Resource.Id.buttonRemoveTile, Optional = true)] private Button mButtonRemoveTile;
        [Splice(Resource.Id.buttonAddBarcodeTile, Optional = true)] private Button mButtonAddBarcodeTile;
        [Splice(Resource.Id.buttonAddButtonTile, Optional = true)] private Button mButtonAddButtonTile;
        [Splice(Resource.Id.cbBadging, Optional = true)] private CheckBox mCheckboxBadging;
        [Splice(Resource.Id.cbCustomTheme, Optional = true)] private CheckBox mCheckboxCustomTheme;
        [Splice(Resource.Id.viewCustomTheme, Optional = true)] private BandThemeView mThemeView;

        [Splice(Resource.Id.editTileName, Optional = true)] private EditText mEditTileName;
        [Splice(Resource.Id.editTitle, Optional = true)] private EditText mEditTitle;
        [Splice(Resource.Id.editBody, Optional = true)] private EditText mEditBody;

        [Splice(Resource.Id.buttonSendMessage, Optional = true)] private Button mButtonSendMessage;
        [Splice(Resource.Id.buttonSendDialog, Optional = true)] private Button mButtonSendDialog;

        [Splice(Resource.Id.cbWithDialog, Optional = true)] private CheckBox mCheckboxWithDialog;

        [Splice(Resource.Id.listTiles, Optional = true)] private ListView mListTiles;

        private TileListAdapter mTileListAdapter;
        private BandTile mSelectedTile;

		private ButtonTileBroadcastReceiver tileReceiver;

        public TilesFragment()
        {
            mRemainingCapacity = -1;
			tileReceiver = new ButtonTileBroadcastReceiver();
        }

		public override void OnResume()
		{
			base.OnResume();

			var filter = new IntentFilter();
			filter.AddAction(TileEvent.ActionTileOpened);
			filter.AddAction(TileEvent.ActionTileButtonPressed);
			filter.AddAction(TileEvent.ActionTileClosed);

			Application.Context.RegisterReceiver(tileReceiver, filter);
		}

		public override void OnPause()
		{
			base.OnPause();

			Application.Context.UnregisterReceiver(tileReceiver);
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // all the splicing is optional as we are going to populate 
            // the members/events from three different views (fragment/header/footer)

            var rootView = inflater.Inflate(Resource.Layout.fragment_tiles, container, false);
            Geneticist.Splice(this, rootView);

            var header = inflater.Inflate(Resource.Layout.fragment_tiles_header, null);
            Geneticist.Splice(this, header);

            var footer = inflater.Inflate(Resource.Layout.fragment_tiles_footer, null);
            Geneticist.Splice(this, footer);

            mListTiles.AddHeaderView(header);
            mListTiles.AddFooterView(footer);

            mTileListAdapter = new TileListAdapter(this);
            mListTiles.Adapter = mTileListAdapter;
            mListTiles.ItemClick += (sender, e) =>
            {
                var position = e.Position - 1; // ignore the header
                if (position >= 0 && position < mTileListAdapter.Count)
                {
                    mSelectedTile = (BandTile)mTileListAdapter.GetItem(position);
                    RefreshControls();
                }
            };

            return rootView;
        }

        [SpliceClick(Resource.Id.buttonAddButtonTile, Optional = true)]
        private async void OnAddButtonTileClick(object sender, EventArgs e)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InScaled = false;
                BandIcon tileIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.tile, options));

                BandIcon badgeIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.badge, options));

                FilledButton button = new FilledButton(0, 5, 210, 45);
                button.BackgroundColor = Color.Red;
                button.Margins = new Margins(0, 5, 0, 0);
                button.ElementId = 12;

                TextButton button2 = new TextButton(0, 0, 210, 45);
                button2.PressedColor = Color.Blue;
                button2.Margins = new Margins(0, 5, 0, 0);
                button2.ElementId = 21;

                FlowPanel flowPanel = new FlowPanel(15, 0, 260, 105, FlowPanelOrientation.Vertical);
                flowPanel.AddElements(button);
                flowPanel.AddElements(button2);

                PageLayout pageLayout = new PageLayout(flowPanel);

                BandTile.Builder builder = new BandTile.Builder(Java.Util.UUID.RandomUUID(), mEditTileName.Text, tileIcon);
                if (mCheckboxBadging.Checked)
                {
                    builder.SetTileSmallIcon(badgeIcon);
                }
                if (mCheckboxCustomTheme.Checked)
                {
                    builder.SetTheme(mThemeView.Theme);
                }
                builder.SetPageLayouts(pageLayout);
                BandTile tile = builder.Build();

                try
                {
                    var result = await Model.Instance.Client.TileManager.AddTileTaskAsync(Activity, tile);
                    if (result)
                    {
                        Toast.MakeText(Activity, "Tile added", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(Activity, "Unable to add tile", ToastLength.Short).Show();
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowExceptionAlert(Activity, "Add tile", ex);
                }

                PageData pageData = new PageData(Java.Util.UUID.RandomUUID(), 0);
                pageData.Update(new FilledButtonData(12, Color.Yellow));
                pageData.Update(new TextButtonData(21, "Text Button"));
                await Model.Instance.Client.TileManager.SetPagesTaskAsync(tile.TileId, pageData);

                Toast.MakeText(Activity, "Page updated", ToastLength.Short).Show();

                // Refresh our tile list and count
                await RefreshData();
                RefreshControls();
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Add tile", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonAddBarcodeTile, Optional = true)]
        private async void OnAddBarcodeButtonClick(object sender, EventArgs e)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InScaled = false;
                BandIcon tileIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.tile, options));

                BandIcon badgeIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.badge, options));

                // create layout 1

                Barcode barcode1 = new Barcode(new PageRect(0, 0, 221, 70), BarcodeType.Code39);
                barcode1.Margins = new Margins(3, 0, 0, 0);
                barcode1.ElementId = 11;

                TextBlock textBlock1 = new TextBlock(new PageRect(0, 0, 230, 30), TextBlockFont.Small, 0);
                textBlock1.Color = Color.Red;
                textBlock1.ElementId = 21;

                FlowPanel flowPanel1 = new FlowPanel(new PageRect(15, 0, 245, 105), FlowPanelOrientation.Vertical);
                flowPanel1.AddElements(barcode1);
                flowPanel1.AddElements(textBlock1);

                PageLayout pageLayout1 = new PageLayout(flowPanel1);

                // create layout 2

                Barcode barcode2 = new Barcode(0, 0, 221, 70, BarcodeType.Pdf417);
                barcode2.Margins = new Margins(3, 0, 0, 0);
                barcode2.ElementId = 11;

                TextBlock textBlock2 = new TextBlock(0, 0, 230, 30, TextBlockFont.Small, 0);
                textBlock2.Color = Color.Red;
                textBlock2.ElementId = 21;

                FlowPanel flowPanel2 = new FlowPanel(15, 0, 245, 105, FlowPanelOrientation.Vertical);
                flowPanel2.AddElements(barcode2);
                flowPanel2.AddElements(textBlock2);

                PageLayout pageLayout2 = new PageLayout(flowPanel2);

                // create the tile

                BandTile.Builder builder = new BandTile.Builder(Java.Util.UUID.RandomUUID(), mEditTileName.Text, tileIcon);
                if (mCheckboxBadging.Checked)
                {
                    builder.SetTileSmallIcon(badgeIcon);
                }
                if (mCheckboxCustomTheme.Checked)
                {
                    builder.SetTheme(mThemeView.Theme);
                }
                builder.SetPageLayouts(pageLayout1, pageLayout2);
                BandTile tile = builder.Build();

                // add tile

                try
                {
                    var result = await Model.Instance.Client.TileManager.AddTileTaskAsync(Activity, tile);
                    if (result)
                    {
                        Toast.MakeText(Activity, "Tile added", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(Activity, "Unable to add tile", ToastLength.Short).Show();
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowExceptionAlert(Activity, "Add tile", ex);
                }

                PageData pageData1 = new PageData(Java.Util.UUID.RandomUUID(), 0);
                pageData1.Update(new BarcodeData(11, "MK12345509", BarcodeType.Code39));
                pageData1.Update(new TextButtonData(21, "MK12345509"));

                PageData pageData2 = new PageData(Java.Util.UUID.RandomUUID(), 1);
                pageData2.Update(new BarcodeData(11, "901234567890123456", BarcodeType.Pdf417));
                pageData2.Update(new TextButtonData(21, "901234567890123456"));

                await Model.Instance.Client.TileManager.SetPagesTaskAsync(tile.TileId, pageData1, pageData2);

                Toast.MakeText(Activity, "Page updated", ToastLength.Short).Show();

                // Refresh our tile list and count
                await RefreshData();
                RefreshControls();
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Add tile", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonAddTile, Optional = true)]
        private async void OnAddTileClick(object sender, EventArgs e)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InScaled = false;
                BandIcon tileIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.tile, options));
                BandIcon badgeIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.badge, options));

                BandTile.Builder builder = new BandTile.Builder(Java.Util.UUID.RandomUUID(), mEditTileName.Text, tileIcon);
                if (mCheckboxBadging.Checked)
                {
                    builder.SetTileSmallIcon(badgeIcon);
                }
                if (mCheckboxCustomTheme.Checked)
                {
                    builder.SetTheme(mThemeView.Theme);
                }
                BandTile tile = builder.Build();

                try
                {
                    var result = await Model.Instance.Client.TileManager.AddTileTaskAsync(Activity, tile);
                    if (result)
                    {
                        Toast.MakeText(Activity, "Tile added", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(Activity, "Unable to add tile", ToastLength.Short).Show();
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowExceptionAlert(Activity, "Add tile", ex);
                }

                // Refresh our tile list and count
                await RefreshData();
                RefreshControls();
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Add tile", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonRemoveTile, Optional = true)]
        private async void OnRemoveTileClick(object sender, EventArgs e)
        {
            try
            {
                await Model.Instance.Client.TileManager.RemoveTileTaskAsync(mSelectedTile.TileId);
                mSelectedTile = null;
                Toast.MakeText(Activity, "Tile removed", ToastLength.Short).Show();
                await RefreshData();
                RefreshControls();
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Remove tile", ex);
            }
        }

        [SpliceCheckedChange(Resource.Id.cbCustomTheme, Optional = true)]
        private void OnCustomThemeCheckChanged(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            mThemeView.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
        }

        [SpliceTextChanged(Resource.Id.editTileName, Optional = true)]
        private void OnEditTileNameTextChanged(object sender, EventArgs e)
        {
            RefreshControls();
        }

        [SpliceClick(Resource.Id.buttonSendMessage, Optional = true)]
        private async void OnSendMessageClick(object sender, EventArgs e)
        {
            try
            {
                await Model.Instance.Client.NotificationManager.SendMessageTaskAsync(mSelectedTile.TileId, mEditTitle.Text, mEditBody.Text, DateTime.Now, mCheckboxWithDialog.Checked);
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Send message", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonSendDialog, Optional = true)]
        private async void OnSendDialogClick(object sender, EventArgs e)
        {
            try
            {
                await Model.Instance.Client.NotificationManager.ShowDialogTaskAsync(mSelectedTile.TileId, mEditTitle.Text, mEditBody.Text);
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Show dialog", ex);
            }
        }

        public async void OnFragmentSelected()
        {
            if (!IsVisible)
            {
                return;
            }

            await RefreshData();
            RefreshControls();
        }

        //
        // Helper methods
        //

        private async Task RefreshData()
        {
            if (Model.Instance.Connected)
            {
                try
                {
                    var capacity = await Model.Instance.Client.TileManager.GetRemainingTileCapacityTaskAsync();
                    mRemainingCapacity = capacity;
                }
                catch (Exception e)
                {
                    mRemainingCapacity = -1;
                    Util.ShowExceptionAlert(Activity, "Check capacity", e);
                }

                try
                {
                    var tiles = await Model.Instance.Client.TileManager.GetTilesTaskAsync();
                    mTileListAdapter.TileList = tiles.ToList();
                }
                catch (Exception e)
                {
                    Util.ShowExceptionAlert(Activity, "Get tiles", e);
                }
            }
            else
            {
                mRemainingCapacity = -1;
            }
        }

        private void RefreshControls()
        {
            bool connected = Model.Instance.Connected;

            mTextRemainingCapacity.Text = mRemainingCapacity < 0 ? "?" : Convert.ToString(mRemainingCapacity);

            mButtonRemoveTile.Enabled = connected && mSelectedTile != null;

            mButtonAddTile.Enabled = connected && mRemainingCapacity > 0 && mEditTileName.Text.Length > 0;

            mButtonAddBarcodeTile.Enabled = connected && mRemainingCapacity > 0 && mEditTileName.Text.Length > 0;

            mButtonAddButtonTile.Enabled = connected && mRemainingCapacity > 0 && mEditTileName.Text.Length > 0;

            mButtonSendDialog.Enabled = connected && mSelectedTile != null && (mEditTitle.Text.Length > 0 || mEditBody.Text.Length > 0);

            mButtonSendMessage.Enabled = connected && mSelectedTile != null && (mEditTitle.Text.Length > 0 || mEditBody.Text.Length > 0);
        }

        private class TileListAdapter : BaseAdapter
        {
            private readonly TilesFragment fragment;

            public TileListAdapter(TilesFragment fragment)
            {
                this.fragment = fragment;
            }

            private List<BandTile> mList;

            public virtual ICollection<BandTile> TileList
            {
                set
                {
                    if (mList == null)
                    {
                        mList = new List<BandTile>();
                    }

                    mList.Clear();
                    mList.AddRange(value);
                    NotifyDataSetChanged();
                }
            }

            public override int Count
            {
                get
                {
                    return (mList != null) ? mList.Count : 0;
                }
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return (mList != null) ? mList[position] : null;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                if (view == null)
                {
                    view = fragment.Activity.LayoutInflater.Inflate(Resource.Layout.item_tilelist, null);
                }

                BandTile tile = mList[position];

                ImageView tileImage = view.FindViewById<ImageView>(Resource.Id.imageTileListImage);
                TextView tileTitle = view.FindViewById<TextView>(Resource.Id.textTileListTitle);

                if (tile.TileIcon != null)
                {
                    tileImage.SetImageBitmap(tile.TileIcon.Icon);
                }
                else
                {
                    BandIcon tileIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(fragment.Resources, Resource.Raw.badge));
                    tileImage.SetImageBitmap(tileIcon.Icon);
                }

                tileImage.SetBackgroundColor(Color.Blue);
                tileTitle.Text = tile.TileName;

                return view;
            }
        }

        private class ButtonTileBroadcastReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action == TileEvent.ActionTileOpened)
                {
                    var tileOpenData = (TileEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
                    Toast.MakeText(context, string.Format("Tile opened: {0}", tileOpenData.TileName), ToastLength.Short).Show();
                }
                else if (intent.Action == TileEvent.ActionTileButtonPressed)
                {
                    var buttonData = (TileButtonEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
                    Toast.MakeText(context, string.Format("Button {0} Pressed: {1}", buttonData.ElementId, buttonData.TileName), ToastLength.Short).Show();
                }
                else if (intent.Action == TileEvent.ActionTileClosed)
                {
                    var tileCloseData = (TileEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
                    Toast.MakeText(context, string.Format("Tile closed: {0}", tileCloseData.TileName), ToastLength.Short).Show();
                }
            }
        }
    }

}
