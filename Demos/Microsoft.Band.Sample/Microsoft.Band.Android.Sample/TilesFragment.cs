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
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Android.Graphics;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Sql;
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using Fragment = Android.Support.V4.App.Fragment;

namespace Microsoft.Band.Sample
{
    public class TilesFragment : Fragment, FragmentListener
    {
        private int mRemainingCapacity;
        private ICollection<BandTile> mTiles;

        private TextView mTextRemainingCapacity;
        private Button mButtonAddTile;
        private Button mButtonRemoveTile;
		private Button mButtonAddBarcodeTile;
		private Button mButtonAddButtonTile;
        private CheckBox mCheckboxBadging;
        private CheckBox mCheckboxCustomTheme;
        private BandThemeView mThemeView;

        private EditText mEditTileName;
        private EditText mEditTitle;
        private EditText mEditBody;

        private Button mButtonSendMessage;
        private Button mButtonSendDialog;

        private CheckBox mCheckboxWithDialog;

        private ListView mListTiles;
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
            View rootView = inflater.Inflate(Resource.Layout.fragment_tiles, container, false);
            mListTiles = rootView.FindViewById<ListView>(Resource.Id.listTiles);

            RelativeLayout header = (RelativeLayout)inflater.Inflate(Resource.Layout.fragment_tiles_header, null);

			mTextRemainingCapacity = header.FindViewById<TextView>(Resource.Id.textAvailableCapacity);
			mButtonAddButtonTile = header.FindViewById<Button>(Resource.Id.buttonAddButtonTile);
			mButtonAddButtonTile.Click += async delegate
			{
				try
				{
					//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
					//ORIGINAL LINE: final android.graphics.BitmapFactory.Options options = new android.graphics.BitmapFactory.Options();
					BitmapFactory.Options options = new BitmapFactory.Options();
					options.InScaled = false;
					BandIcon tileIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.tile, options));

					BandIcon badgeIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.badge, options));

					FilledButton button = new FilledButton(0, 5, 210, 45);
					button.SetBackgroundColor(Color.Red);
					button.SetMargins(0, 5, 0 ,0);
					button.SetId(12);

					TextButton button2 = new TextButton(0, 0, 210, 45);
					button2.SetPressedColor(Color.Blue);
					button2.SetMargins(0, 5, 0 ,0);
					button2.SetId(21);

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
				catch (Exception e)
				{
					Util.ShowExceptionAlert(Activity, "Add tile", e);
				}
			};
			mButtonAddBarcodeTile = header.FindViewById<Button>(Resource.Id.buttonAddBarcodeTile);
			mButtonAddBarcodeTile.Click += async delegate
			{
				try
				{
					//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
					//ORIGINAL LINE: final android.graphics.BitmapFactory.Options options = new android.graphics.BitmapFactory.Options();
					BitmapFactory.Options options = new BitmapFactory.Options();
					options.InScaled = false;
					BandIcon tileIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.tile, options));

					BandIcon badgeIcon = BandIcon.ToBandIcon(BitmapFactory.DecodeResource(Resources, Resource.Raw.badge, options));

					// create layout 1

					Barcode barcode1 = new Barcode(0, 0, 221, 70, BarcodeType.Code39);
					barcode1.SetMargins(3, 0, 0, 0);
					barcode1.SetId(11);

					TextBlock textBlock1 = new TextBlock(0, 0, 230, 30, TextBlockFont.Small, 0);
					textBlock1.SetColor(Color.Red);
					textBlock1.SetId(21);

					FlowPanel flowPanel1 = new FlowPanel(15, 0, 245, 105, FlowPanelOrientation.Vertical);
					flowPanel1.AddElements(barcode1);
					flowPanel1.AddElements(textBlock1);

					PageLayout pageLayout1 = new PageLayout(flowPanel1);

					// create layout 2

					Barcode barcode2 = new Barcode(0, 0, 221, 70, BarcodeType.Pdf417);
					barcode2.SetMargins(3, 0, 0, 0);
					barcode2.SetId(11);

					TextBlock textBlock2 = new TextBlock(0, 0, 230, 30, TextBlockFont.Small, 0);
					textBlock2.SetColor(Color.Red);
					textBlock2.SetId(21);

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
				catch (Exception e)
				{
					Util.ShowExceptionAlert(Activity, "Add tile", e);
				}
			};
            mButtonAddTile = header.FindViewById<Button>(Resource.Id.buttonAddTile);
            mButtonAddTile.Click += async delegate
            {
                try
                {
                    //JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
                    //ORIGINAL LINE: final android.graphics.BitmapFactory.Options options = new android.graphics.BitmapFactory.Options();
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
                catch (Exception e)
                {
                    Util.ShowExceptionAlert(Activity, "Add tile", e);
                }
            };
            mButtonRemoveTile = header.FindViewById<Button>(Resource.Id.buttonRemoveTile);
            mButtonRemoveTile.Click += async delegate
            {
                try
                {
                    await Model.Instance.Client.TileManager.RemoveTileTaskAsync(mSelectedTile.TileId);
                    mSelectedTile = null;
                    Toast.MakeText(Activity, "Tile removed", ToastLength.Short).Show();
                    await RefreshData();
                    RefreshControls();
                }
                catch (Exception e)
                {
                    Util.ShowExceptionAlert(Activity, "Remove tile", e);
                }
            };
            mCheckboxBadging = header.FindViewById<CheckBox>(Resource.Id.cbBadging);

            mThemeView = header.FindViewById<BandThemeView>(Resource.Id.viewCustomTheme);
            mCheckboxCustomTheme = header.FindViewById<CheckBox>(Resource.Id.cbCustomTheme);
            mCheckboxCustomTheme.CheckedChange += (sender, e) =>
            {
                    mThemeView.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            };

            mEditTileName = header.FindViewById<EditText>(Resource.Id.editTileName);
            mEditTileName.TextChanged += (sender, e) => RefreshControls();

            RelativeLayout footer = (RelativeLayout)inflater.Inflate(Resource.Layout.fragment_tiles_footer, null);

            mEditTitle = footer.FindViewById<EditText>(Resource.Id.editTitle);
            mEditBody = footer.FindViewById<EditText>(Resource.Id.editBody);
            mCheckboxWithDialog = footer.FindViewById<CheckBox>(Resource.Id.cbWithDialog);

            mButtonSendMessage = footer.FindViewById<Button>(Resource.Id.buttonSendMessage);
            mButtonSendMessage.Click += async delegate
            {
                try
                {
                    await Model.Instance.Client.NotificationManager.SendMessageTaskAsync(
                        mSelectedTile.TileId,
                        mEditTitle.Text,
                        mEditBody.Text,
                        DateTime.Now,
                        mCheckboxWithDialog.Checked);
                }
                catch (Exception e)
                {
                    Util.ShowExceptionAlert(Activity, "Send message", e);
                }
            };

            mButtonSendDialog = footer.FindViewById<Button>(Resource.Id.buttonSendDialog);
            mButtonSendDialog.Click += async delegate
            {
                try
                {
                    await Model.Instance.Client.NotificationManager.ShowDialogTaskAsync(mSelectedTile.TileId, mEditTitle.Text, mEditBody.Text);
                }
                catch (Exception e)
                {
                    Util.ShowExceptionAlert(Activity, "Show dialog", e);
                }
            };

            mListTiles.AddHeaderView(header);
            mListTiles.AddFooterView(footer);

            mTileListAdapter = new TileListAdapter(this);
            mListTiles.Adapter = mTileListAdapter;

            mListTiles.ItemClick += (sender, e) =>
            {
                var position = e.Position - 1; // ignore the header
                if (position >= 0 && position < mTileListAdapter.Count)
                {
                    mSelectedTile = (BandTile) mTileListAdapter.GetItem(position);
                    RefreshControls();
                }
            };

            return rootView;
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
                    mTiles = null;
                    mSelectedTile = null;
                    Util.ShowExceptionAlert(Activity, "Get tiles", e);
                }
            }
            else
            {
                mRemainingCapacity = -1;
                mTiles = null;
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
					Toast.MakeText(context, string.Format("Button {0} Pressed: {1}", buttonData.ElementID, buttonData.TileName), ToastLength.Short).Show();
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