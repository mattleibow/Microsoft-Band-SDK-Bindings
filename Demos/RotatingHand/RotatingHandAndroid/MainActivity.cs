using System;

using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

using Microsoft.Band;
using Microsoft.Band.Sensors;
using System.Diagnostics;
using Android.OS;
using Android.App;

[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Microsoft.Band.BandClientManager.BindBandService)]

namespace RotatingHandAndroid
{
	[Activity (Label = "Rotating Hand", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
	public class MainActivity : ActionBarActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			// get the image
			var hand = FindViewById<ImageView> (Resource.Id.hand);

			// start the process when the user taps
			hand.Click += async delegate {
				// get the bands
				var pairedBands = BandClientManager.Instance.GetPairedBands ();

				try {
					// connect to one
					var bandClient = BandClientManager.Instance.Create (this, pairedBands [0]);
					await bandClient.ConnectTaskAsync();

					// get hold of the accelerometer
					var accelerometer = bandClient.SensorManager.CreateAccelerometerSensor();

					// handle incoming updates
					accelerometer.ReadingChanged += (o, args) => {
						// get the rotation in degrees
						var yReading = args.SensorReading.AccelerationY;
						var rotation = yReading*90;

						// update the image
						RunOnUiThread(() => {
							hand.RotationY = rotation;
						});
					};

					// start listening for updates
					await accelerometer.StartReadingsTaskAsync(SampleRate.Ms16);
				} catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex.Message);
				}
			};
		}
	}
}
