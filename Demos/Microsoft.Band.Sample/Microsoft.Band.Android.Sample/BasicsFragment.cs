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
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Genetics;
using Genetics.Attributes;
using Fragment = Android.Support.V4.App.Fragment;

using Microsoft.Band.Notifications;

namespace Microsoft.Band.Sample
{
    public class BasicsFragment : Fragment, FragmentListener
    {
        [Splice(Resource.Id.buttonConnect)] private Button mButtonConnect;
        [Splice(Resource.Id.buttonChooseBand)] private Button mButtonChooseBand;

        [Splice(Resource.Id.buttonGetHardwareVersion)] private Button mButtonGetHwVersion;
        [Splice(Resource.Id.buttonGetFirmwareVersion)] private Button mButtonGetFwVersion;

        [Splice(Resource.Id.textHardwareVersion)] private TextView mTextHwVersion;
        [Splice(Resource.Id.textFirmwareVersion)] private TextView mTextFwVersion;

        [Splice(Resource.Id.buttonVibrate)] private Button mButtonVibrate;
        [Splice(Resource.Id.buttonVibrationPattern)] private Button mButtonVibratePattern;

        private IBandConnectionCallback mCallback;
        private IBandInfo[] mPairedBands;
        private int mSelectedBandIndex = 0;

        private VibrationType mSelectedVibrationType = VibrationType.NotificationAlarm;

        public void OnFragmentSelected()
        {
            if (IsVisible)
            {
                RefreshControls();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_basics, container, false);

            Geneticist.Splice(this, rootView);

            mButtonVibratePattern.Text = mSelectedVibrationType.ToString();

            return rootView;
        }

        // Handle connect/disconnect requests.
        [SpliceClick(Resource.Id.buttonConnect)]
        private async void OnConnectClick(object sender, EventArgs args)
        {
            if (Model.Instance.Connected)
            {
                try
                {
                    await Model.Instance.Client.DisconnectTaskAsync();
                    RefreshControls();
                }
                catch (Exception ex)
                {
                    Util.ShowExceptionAlert(Activity, "Disconnect", ex);
                }
            }
            else
            {
                // Always recreate our BandClient since the selection might
                // have changed. This is safe since we aren't connected.
                IBandClient client = BandClientManager.Instance.Create(Activity, mPairedBands[mSelectedBandIndex]);
                Model.Instance.Client = client;

                mButtonConnect.Enabled = false;

                if (mCallback != null)
                {
                    Model.Instance.Client.UnregisterConnectionCallback();
                }
                mCallback = Model.Instance.Client.RegisterConnectionCallback(connectionState =>
                {
                    Toast.MakeText(Activity, "Connection state: " + connectionState, ToastLength.Short).Show();
                });
                
				try
				{
					// Connect must be called on a background thread.
					var result = await Model.Instance.Client.ConnectTaskAsync();

					// callback that must be handled on the UI thread
					if (result != ConnectionState.Connected)
					{
						Util.ShowExceptionAlert(Activity, "Connect", new Exception("Connection failed: result=" + result));
					}
					RefreshControls();
				}
				catch (Exception ex)
				{
					Util.ShowExceptionAlert(Activity, "Connect", ex);
				}
            }
        }

        public override void OnResume()
        {
            base.OnResume();

            mPairedBands = BandClientManager.Instance.GetPairedBands();

            // If one or more bands were removed, making our band selection invalid,
            // reset the selection to the first in the list.
            if (mSelectedBandIndex >= mPairedBands.Length)
            {
                mSelectedBandIndex = 0;
            }

            RefreshControls();
        }

        // If there are multiple bands, the "choose band" button is enabled and
        // launches a dialog where we can select the band to use.
        [SpliceClick(Resource.Id.buttonChooseBand)]
        private void OnChooseBandClick(object sender, EventArgs e)
        {
            using (var builder = new AlertDialog.Builder(Activity))
            {
                string[] names = new string[mPairedBands.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = mPairedBands[i].Name;
                }

                builder.SetItems(names, (dialog, args) =>
                {
                    mSelectedBandIndex = args.Which;
                    ((Dialog) dialog).Dismiss();
                    RefreshControls();
                });

                builder.SetTitle("Select band:");
                builder.Show();
            }
        }

        [SpliceClick(Resource.Id.buttonGetFirmwareVersion)]
        private async void OnGetFwVersionClick(object sender, EventArgs e)
        {
            try
            {
                mTextFwVersion.Text = "";
                var version = await Model.Instance.Client.GetFirmwareVersionTaskAsync();
                mTextFwVersion.Text = version;
            }
            catch (Java.Util.Concurrent.TimeoutException)
            {
                mTextFwVersion.Text = "timeout";
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Get firmware version", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonGetHardwareVersion)]
        private async void OnGetHwVersionClick(object sender, EventArgs e)
        {
            try
            {
                mTextHwVersion.Text = "";
                var version = await Model.Instance.Client.GetHardwareVersionTaskAsync();
                mTextHwVersion.Text = version;
            }
            catch (Java.Util.Concurrent.TimeoutException)
            {
                mTextHwVersion.Text = "timeout";
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Get hardware version", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonVibrate)]
        private async void OnVibrateClick(object sender, EventArgs e)
        {
            try
            {
                await Model.Instance.Client.NotificationManager.VibrateTaskAsync(mSelectedVibrationType);
            }
            catch (Exception ex)
            {
                Util.ShowExceptionAlert(Activity, "Vibrate band", ex);
            }
        }

        [SpliceClick(Resource.Id.buttonVibrationPattern)]
        private void OnVibratePatternClick(object sender, EventArgs e)
        {
            using (var builder = new AlertDialog.Builder(Activity))
            {

                VibrationType[] values = VibrationType.Values();
                string[] names = new string[values.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = values[i].ToString();
                }

                builder.SetItems(names, (dialog, args) =>
                {
                    mSelectedVibrationType = values[args.Which];
                    ((Dialog) dialog).Dismiss();
                    RefreshControls();
                });

                builder.SetTitle("Select vibration type:");
                builder.Show();
            }
        }

        private void RefreshControls()
        {
            switch (mPairedBands.Length)
            {
            case 0:
                mButtonChooseBand.Text = "No paired bands";
                mButtonChooseBand.Enabled = false;
                mButtonConnect.Enabled = false;
                break;

            case 1:
                mButtonChooseBand.Text = mPairedBands[mSelectedBandIndex].Name;
                mButtonChooseBand.Enabled = false;
                mButtonConnect.Enabled = true;
                break;

            default:
                mButtonChooseBand.Text = mPairedBands[mSelectedBandIndex].Name;
                mButtonChooseBand.Enabled = true;
                mButtonConnect.Enabled = true;
                break;
            }

            bool connected = Model.Instance.Connected;

            if (connected)
            {
                mButtonConnect.SetText(Resource.String.disconnect_label);

                // must disconnect before changing the band selection
                mButtonChooseBand.Enabled = false;
            }
            else
            {
                mButtonConnect.SetText(Resource.String.connect_label);
                mTextFwVersion.Text = "";
                mTextHwVersion.Text = "";
            }

            mButtonVibratePattern.Text = mSelectedVibrationType.ToString();
            mButtonGetFwVersion.Enabled = connected;
            mButtonGetHwVersion.Enabled = connected;
            mButtonVibrate.Enabled = connected;
            mButtonVibratePattern.Enabled = connected;
        }
    }

}