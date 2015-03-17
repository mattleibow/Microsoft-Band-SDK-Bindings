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

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton RunButton { get; set; }

		[Action ("OnRunClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnRunClick (UIButton sender);

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
			if (RunButton != null) {
				RunButton.Dispose ();
				RunButton = null;
			}
		}
	}
}
