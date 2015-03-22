using System;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Sensors;

namespace Microsoft.Band.iOS.Sample
{
	partial class MainViewController : UIViewController
	{
		private BandClientManager manager;
		private BandClient client;
		private AccelerometerSensor accelerometer;
		private bool sensorStarted;

		private static NSUuid tileId = new NSUuid ("DCBABA9F-12FD-47A5-83A9-E7270A4399BB");

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view, typically from a nib.

			manager = BandClientManager.Instance;
		}

		async partial void ConnectToBandClick (UIButton sender)
		{
			if (client == null) {
				// get the client
				client = manager.AttachedClients.FirstOrDefault ();
				if (client == null) {
					Output ("Failed! No Bands attached.");
				} else {
					try {
						Output ("Please wait. Connecting to Band...");
						await manager.ConnectTaskAsync (client);
						Output ("Band connected.");
					} catch (BandException ex) {
						Output ("Failed to connect to Band:");
						Output (ex.Message);
					}
				}
			} else {
				Output ("Please wait. Disconnecting from Band...");
				await manager.DisconnectTaskAsync (client);
				Output ("Band disconnected.");
			}
		}

		partial void StartAccelerometerClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				// create the sensor once
				if (accelerometer == null) {
					accelerometer = client.SensorManager.CreateAccelerometerSensor ();
					accelerometer.ReadingChanged += (_, e) => {
						var data = e.SensorReading;
						AccelerometerDataText.Text = string.Format ("Accel Data: X={0:+0.00} Y={0:+0.00} Z={0:+0.00}", data.X, data.Y, data.Z);
					};
				}
				if (sensorStarted) {
					Output ("Stopping Accelerometer updates...");
					try {
						accelerometer.StopReadings ();
						sensorStarted = false;
					} catch (BandException ex) {
						Output ("Error: " + ex.Message);
					}
				} else {
					Output ("Starting Accelerometer updates...");
					try {
						accelerometer.StartReadings ();
						sensorStarted = true;
					} catch (BandException ex) {
						Output ("Error: " + ex.Message);
					}
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		async partial void ToggleAppTileClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				Output ("Creating tile...");

				// the number of tile spaces left
				var capacity = await client.TileManager.RemainingTileCapacityTaskAsync ();
				Output ("Remaning tile space: " + capacity);

				// create the tile
				NSError operationError;
				var tileName = "B tile";
				var tileIcon = BandIcon.FromUIImage (UIImage.FromBundle ("B.png"), out operationError);
				var smallIcon = BandIcon.FromUIImage (UIImage.FromBundle ("Bb.png"), out operationError);
				var tile = BandTile.Create (tileId, tileName, tileIcon, smallIcon, out operationError);

				// get the tiles
				try {
					var tiles = await client.TileManager.GetTilesTaskAsync ();
					if (tiles.Any (x => x.TileId.AsString () == tileId.AsString ())) {
						// a tile exists, so remove it
						await client.TileManager.RemoveTileTaskAsync (tileId);
						Output ("Removed tile!");
					} else {
						// the tile does not exist, so add it
						await client.TileManager.AddTileTaskAsync (tile);
						Output ("Added tile!");
					}
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		async partial void SendMessageClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				Output ("Sending notification...");

				// send a message with a dialog
				try {
					await client.NotificationManager.SendMessageTaskAsync (tileId, "Hello", "Hello World!", DateTime.Now, true);
					Output ("Sent the message!!");
				} catch (BandException ex) {
					Output ("Failed to send the message:");
					Output (ex.Message);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		private void Output (string message)
		{
			OutputText.Text += Environment.NewLine + message;
			CGPoint p = OutputText.ContentOffset;
			OutputText.SetContentOffset (p, false);
			OutputText.ScrollRangeToVisible (new NSRange (OutputText.Text.Length, 0));
		}
	}
}
