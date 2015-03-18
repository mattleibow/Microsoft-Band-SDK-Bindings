using System;
using System.CodeDom.Compiler;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

using Microsoft.Band;

namespace Microsoft.Band.iOS.Sample
{
	partial class MainViewController : UIViewController
	{
		private BandClientManager manager;
		private BandClient client;

		private static NSUuid tileId = new NSUuid ("DCBABA9F-12FD-47A5-83A9-E7270A4399BB");

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view, typically from a nib.

			// create the delgate events
			manager = BandClientManager.SharedManager;
			manager.Connected += (sender, e) => {
				Output ("Band connected.");
			};
			manager.Disconnected += (sender, e) => {
				Output ("Band disconnected.");
			};
			manager.FailedToConnect += (sender, e) => {
				Output ("Failed to connect to Band.");
				Output (e.Error.Description);
			};

			// get the client
			client = manager.AttachedClients.FirstOrDefault ();
			if (client == null) {
				Output ("Failed! No Bands attached.");
			} else {
				manager.Connect (client);
				Output ("Please wait. Connecting to Band...");
			}
		}

		public override void ViewWillUnload ()
		{
			Output ("Stopping Accelerometer updates...");
			NSError error;
			client.SensorManager.StopAccelerometerUpdates (out error);
			if (error != null) {
				Output ("Error: " + error.Description);
			}

			base.ViewWillUnload ();
		}

		partial void StartAccelerometerClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				Output ("Starting Accelerometer updates...");
				NSError queueError;
				client.SensorManager.StartAccelerometerUpdates (null, out queueError, (data, error) => {
					AccelerometerDataText.Text = string.Format ("Accel Data: X={0:+0.00} Y={0:+0.00} Z={0:+0.00}", data.X, data.Y, data.Z);
				});
				if (queueError != null) {
					Output ("Error: " + queueError.Description);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		partial void ToggleAppTileClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				Output ("Creating tile...");

				// the number of tile spaces left
				client.TileManager.RemainingTileCapacity ((capacity, error) => {
					Output ("Remaning tile space: " + capacity);
				});

				// create the tile
				NSError operationError;
				var tileName = "B tile";
				var tileIcon = BandIcon.FromUIImage (UIImage.FromBundle ("B.png"), out operationError);
				var smallIcon = BandIcon.FromUIImage (UIImage.FromBundle ("Bb.png"), out operationError);
				var tile = BandTile.Create (tileId, tileName, tileIcon, smallIcon, out operationError);

				// get the tiles
				client.TileManager.GetTiles ((tiles, tileError) => {
					if (tiles.Any (x => x.TileId.AsString () == tileId.AsString ())) {
						// a tile exists, so remove it
						client.TileManager.RemoveTile (tileId, removeError => {
							if (removeError == null) {
								Output ("Removed tile!");
							} else {
								Output ("Error: " + removeError.Description);
							}
						});
					} else {
						// the tile does not exist, so add it
						client.TileManager.AddTile (tile, addError => {
							if (addError == null) {
								Output ("Added tile!");
							} else {
								Output ("Error: " + addError.Description);
							}
						});
					}
				});
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		partial void SendMessageClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				Output ("Sending notification...");

				// send a message with a dialog
				client.NotificationManager.SendMessage (tileId, "Hello", "Hello World!", NSDate.Now, BandNotificationMessageFlags.ShowDialog, error => {
					if (error == null) {
						Output ("Sent the message!!");
					} else {
						Output ("Error: " + error.Description);
					}
				});
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
