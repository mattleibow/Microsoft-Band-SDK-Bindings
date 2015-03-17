using System;
using System.CodeDom.Compiler;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

using Microsoft.Band;

namespace Microsoft.Band.iOS.Sample
{
	partial class MainViewController : UIViewController, IBandClientManagerDelegate
	{
		private BandClient client;

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view, typically from a nib.
			BandClientManager.SharedManager.Delegate = this;
			var clients = BandClientManager.SharedManager.AttachedClients;
			client = clients.FirstOrDefault ();
			if (client == null) {
				Output ("Failed! No Bands attached.");
			} else {
				BandClientManager.SharedManager.Connect (client);
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

		partial void OnRunClick (UIButton sender)
		{
			if (client != null && client.IsDeviceConnected) {
				Output ("Starting Accelerometer updates...");
				NSError queueError;
				client.SensorManager.StartAccelerometerUpdates (null, out queueError, delegate(BandSensorAccelData accelerometerData, NSError error) {
					AccelerometerDataText.Text = string.Format ("Accel Data: X={0:+0.00} Y={0:+0.00} Z={0:+0.00}", accelerometerData.X, accelerometerData.Y, accelerometerData.Z);
				});
				if (queueError != null) {
					Output ("Error: " + queueError.Description);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		public void Connected (BandClientManager clientManager, BandClient client)
		{
			Output ("Band connected.");
		}

		public void Disconnected (BandClientManager clientManager, BandClient client)
		{
			Output ("Band disconnected.");
		}

		public void FailedToConnect (BandClientManager clientManager, BandClient client, NSError error)
		{
			Output ("Failed to connect to Band.");
			Output (error.Description);
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
