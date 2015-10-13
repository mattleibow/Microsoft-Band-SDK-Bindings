using System;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using Microsoft.Band.Personalization;
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
		private static NSUuid barcodePageId = new NSUuid ("1234BA9F-12FD-47A5-83A9-E7270A43BB99");

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view, typically from a nib.

			manager = BandClientManager.Instance;

			manager.Connected += (sender, e) => {
				Output ("Band connected.");
			};
			manager.ConnectionFailed += (sender, e) => {
				Output ("Band connection failed.");
			};
			manager.Disconnected += (sender, e) => {
				Output ("Band disconnected.");
			};
		}

		async partial void ConnectToBandClick (UIButton sender)
		{
			if (client == null) {
				// get the client
				client = manager.AttachedClients.FirstOrDefault ();

				if (client == null) {
					Output ("Failed! No Bands attached.");
				} else {

					// attach event handlers
					client.ButtonPressed += (_, e) => {
						Output (string.Format ("Button {0} Pressed: {1}", e.TileButtonEvent.ElementId, e.TileButtonEvent.TileName));
					};
					client.TileOpened += (_, e) => {
						Output (string.Format ("Tile Opened: {0}", e.TileEvent.TileName));
					};
					client.TileClosed += (_, e) => {
						Output (string.Format ("Tile Closed: {0}", e.TileEvent.TileName));
					};

					try {
						Output ("Please wait. Connecting to Band...");
						await manager.ConnectTaskAsync (client);
					} catch (BandException ex) {
						Output ("Failed to connect to Band:");
						Output (ex.Message);
					}
				}
			} else {
				Output ("Please wait. Disconnecting from Band...");
				await manager.DisconnectTaskAsync (client);
				client = null;
			}
		}

		partial void StartAccelerometerClick (UIButton sender)
		{
			if (client != null && client.IsConnected) {
				// create the sensor once
				if (accelerometer == null) {
					accelerometer = client.SensorManager.CreateAccelerometerSensor ();
					accelerometer.ReadingChanged += (_, e) => {
						var data = e.SensorReading;
						AccelerometerDataText.Text = string.Format (
							"Accel Data: X={0:0.00} Y={1:0.00} Z={2:0.00}", 
							data.AccelerationX, data.AccelerationY, data.AccelerationZ);
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
			if (client != null && client.IsConnected) {
				Output ("Creating tile...");

				// the number of tile spaces left
				var capacity = await client.TileManager.GetRemainingTileCapacityTaskAsync ();
				Output ("Remaning tile space: " + capacity);

				// create the tile
				NSError operationError;
				var tileName = "iOS Sample";
				var tileIcon = BandIcon.FromUIImage (UIImage.FromBundle ("tile.png"), out operationError);
				var smallIcon = BandIcon.FromUIImage (UIImage.FromBundle ("badge.png"), out operationError);
				var tile = BandTile.Create (tileId, tileName, tileIcon, smallIcon, out operationError);
				tile.BadgingEnabled = true;

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
			if (client != null && client.IsConnected) {
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

		async partial void AddBarcodePageClick (UIButton sender)
		{
			if (client != null && client.IsConnected) {
				Output ("Creating tile...");
		        
				// remove an old tile
				try {
					var tiles = await client.TileManager.GetTilesTaskAsync ();
					if (tiles.Any (x => x.TileId.AsString () == tileId.AsString ())) {
						// a tile exists, so remove it
						await client.TileManager.RemoveTileTaskAsync (tileId);
						Output ("Removed tile!");
					}
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}

				// create the tile
				NSError operationError;
				var tileName = "iOS Sample";
				var tileIcon = BandIcon.FromUIImage (UIImage.FromBundle ("tile.png"), out operationError);
				var smallIcon = BandIcon.FromUIImage (UIImage.FromBundle ("badge.png"), out operationError);
				var tile = BandTile.Create (tileId, tileName, tileIcon, smallIcon, out operationError);
				tile.BadgingEnabled = true;

				// create the barcode page
				var textBlock = new TextBlock (PageRect.Create (0, 0, 230, 40), TextBlockFont.Small);
				textBlock.ElementId = 10;
				textBlock.Baseline = 25;
				textBlock.HorizontalAlignment = HorizontalAlignment.Center;
				textBlock.BaselineAlignment = TextBlockBaselineAlignment.Relative;
				textBlock.AutoWidth = false;
		        
				var barcode = new Barcode (PageRect.Create (0, 5, 230, 95), BarcodeType.Code39);
				barcode.ElementId = 11;
		        
				var flowPanel = new FlowPanel (PageRect.Create (15, 0, 260, 105));
				flowPanel.AddElement (textBlock);
				flowPanel.AddElement (barcode);
		        
				var pageLayout = new PageLayout ();
				pageLayout.Root = flowPanel;
				tile.PageLayouts.Add (pageLayout);

				// add the tile to the band
				try {
					Output ("Adding tile...");
					await client.TileManager.AddTileTaskAsync (tile);
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}

				// set the page data
				try {
					Output ("Creating page data...");
					var pageValues = new PageElementData [] {
						TextBlockData.Create (textBlock.ElementId, "Barcode value: A1 B", out operationError),
						BarcodeData.Create (barcode.ElementId, BarcodeType.Code39, "A1 B", out operationError)
					};
					var page = PageData.Create (barcodePageId, 0, pageValues);
	                
					await client.TileManager.SetPagesTaskAsync (new[]{ page }, tileId);
					Output ("Completed custom page!");
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		async partial void RegisterNotificationsClick (UIButton sender)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var types = UIUserNotificationType.Badge | UIUserNotificationType.Sound | UIUserNotificationType.Alert;
				var mySettings = UIUserNotificationSettings.GetSettingsForTypes (types, null);
				UIApplication.SharedApplication.RegisterUserNotificationSettings (mySettings);
			}

			if (client != null && client.IsConnected) {
				Output ("Creating tile...");

				// remove an old tile
				try {
					var tiles = await client.TileManager.GetTilesTaskAsync ();
					if (tiles.Any (x => x.TileId.AsString () == tileId.AsString ())) {
						// a tile exists, so remove it
						await client.TileManager.RemoveTileTaskAsync (tileId);
						Output ("Removed tile!");
					}
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}

				// create the tile
				NSError operationError;
				var tileName = "iOS Sample";
				var tileIcon = BandIcon.FromUIImage (UIImage.FromBundle ("tile.png"), out operationError);
				var smallIcon = BandIcon.FromUIImage (UIImage.FromBundle ("badge.png"), out operationError);
				var tile = BandTile.Create (tileId, tileName, tileIcon, smallIcon, out operationError);
				tile.BadgingEnabled = true;

				// add the tile to the band
				try {
					Output ("Adding tile...");
					await client.TileManager.AddTileTaskAsync (tile);
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}

				try {
					Output ("Registering notification...");
					await client.NotificationManager.RegisterNotificationTaskAsync ();
					Output ("Completed registration!");

					Output ("Sending notification...");

					var localNotification = new UILocalNotification ();
					localNotification.FireDate = NSDate.Now.AddSeconds (20);
					localNotification.TimeZone = NSTimeZone.DefaultTimeZone;
					localNotification.AlertBody = "Local notification";
					localNotification.AlertAction = "View Details";
					localNotification.SoundName = UILocalNotification.DefaultSoundName;

					UIApplication.SharedApplication.PresentLocalNotificationNow (localNotification);

					Output ("Notification sent!");
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		async partial void AddButtonPageClick (UIButton sender)
		{
			if (client != null && client.IsConnected) {
				Output ("Creating tile...");

				// remove an old tile
				try {
					var tiles = await client.TileManager.GetTilesTaskAsync ();
					if (tiles.Any (x => x.TileId.AsString () == tileId.AsString ())) {
						// a tile exists, so remove it
						await client.TileManager.RemoveTileTaskAsync (tileId);
						Output ("Removed tile!");
					}
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}

				// create the tile
				NSError operationError;
				var tileName = "iOS Sample";
				var tileIcon = BandIcon.FromUIImage (UIImage.FromBundle ("tile.png"), out operationError);
				var smallIcon = BandIcon.FromUIImage (UIImage.FromBundle ("badge.png"), out operationError);
				var tile = BandTile.Create (tileId, tileName, tileIcon, smallIcon, out operationError);
				tile.BadgingEnabled = true;

				// create the button page
				var textBlock = new TextBlock (PageRect.Create (0, 0, 200, 40), TextBlockFont.Small);
				textBlock.ElementId = 10;
				textBlock.Baseline = 25;
				textBlock.HorizontalAlignment = HorizontalAlignment.Center;
				textBlock.BaselineAlignment = TextBlockBaselineAlignment.Relative;
				textBlock.AutoWidth = false;
				textBlock.Color = BandColor.FromUIColor (UIColor.Red, out operationError);
				textBlock.Margins = Margins.Create (5, 2, 5, 2);

				var button = new TextButton (PageRect.Create (0, 0, 200, 40));
				button.ElementId = 11;
				button.HorizontalAlignment = HorizontalAlignment.Center;
				button.PressedColor = BandColor.FromUIColor (UIColor.Purple, out operationError);
				button.Margins = Margins.Create (5, 2, 5, 2);

				var flowPanel = new FlowPanel (PageRect.Create (15, 0, 260, 105));
				flowPanel.AddElement (textBlock);
				flowPanel.AddElement (button);

				var pageLayout = new PageLayout ();
				pageLayout.Root = flowPanel;
				tile.PageLayouts.Add (pageLayout);

				// add the tile to the band
				try {
					Output ("Adding tile...");
					await client.TileManager.AddTileTaskAsync (tile);
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
				}

				// set the page data
				try {
					Output ("Creating page data...");
					var pageValues = new PageElementData [] {
						TextBlockData.Create (textBlock.ElementId, "TextButton sample", out operationError),
						TextButtonData.Create (button.ElementId, "Press Me", out operationError)
					};
					var page = PageData.Create (barcodePageId, 0, pageValues);

					await client.TileManager.SetPagesTaskAsync (new[]{ page }, tileId);
					Output ("Completed custom page!");
				} catch (BandException ex) {
					Output ("Error: " + ex.Message);
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
