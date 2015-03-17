using System;
using System.CodeDom.Compiler;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

using Microsoft.Band;

namespace Microsoft.Band.iOS.Sample
{
	partial class MainViewController : UIViewController, IClientManagerDelegate
	{
		private Client client;

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view, typically from a nib.
			ClientManager.SharedManager.Delegate = this;
			var clients = ClientManager.SharedManager.AttachedClients;
			client = clients.FirstOrDefault ();
			if (client == null) {
				Output ("Failed! No Bands attached.");
			} else {
				ClientManager.SharedManager.ConnectClient (client);
				Output ("Please wait. Connecting to Band...");
			}
		}

		public override void ViewWillUnload ()
		{
			Output ("Stopping Accelerometer updates...");
			NSError error;
			client.SensorManager.StopAccelerometerUpdatesErrorRef (out error);
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
				client.SensorManager.StartAccelerometerUpdatesToQueue (null, out queueError, delegate(SensorAccelData accelerometerData, NSError error) {
					AccelerometerDataText.Text = string.Format ("Accel Data: X={0:+0.00} Y={0:+0.00} Z={0:+0.00}", accelerometerData.X, accelerometerData.Y, accelerometerData.Z);
				});
				if (queueError != null) {
					Output ("Error: " + queueError.Description);
				}
			} else {
				Output ("Band is not connected. Please wait....");
			}
		}

		public void Connected (ClientManager clientManager, Client client)
		{
			Output ("Band connected.");
		}

		public void Disconnected (ClientManager clientManager, Client client)
		{
			Output ("Band disconnected.");
		}

		public void FailedToConnect (ClientManager clientManager, Client client, NSError error)
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
