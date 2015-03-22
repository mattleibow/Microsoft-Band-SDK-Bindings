// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Microsoft.Band.iOS.Sample
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel AccelerometerDataText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView OutputText { get; set; }

		[Action ("ConnectToBandClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ConnectToBandClick (UIButton sender);

		[Action ("SendMessageClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void SendMessageClick (UIButton sender);

		[Action ("StartAccelerometerClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void StartAccelerometerClick (UIButton sender);

		[Action ("ToggleAppTileClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ToggleAppTileClick (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (AccelerometerDataText != null) {
				AccelerometerDataText.Dispose ();
				AccelerometerDataText = null;
			}
			if (OutputText != null) {
				OutputText.Dispose ();
				OutputText = null;
			}
		}
	}
}
