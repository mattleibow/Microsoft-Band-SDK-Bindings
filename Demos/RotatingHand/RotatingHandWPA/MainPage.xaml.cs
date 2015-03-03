using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Band;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace RotatingHandWPA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hand.Tapped += async delegate
            {
                // get the bands
                var pairedBands = await BandClientManager.Instance.GetBandsAsync();

                try
                {
                    // connect to one
                    var bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);

                    // get hold of the accelerometer
                    var accelerometer = bandClient.SensorManager.Accelerometer;

                    // set the reading frequency
                    accelerometer.ReportingInterval = TimeSpan.FromMilliseconds(16);

                    // handle incoming updates
                    accelerometer.ReadingChanged += (o, args) =>
                    {
                        // get the rotation in degrees
                        var yReading = args.SensorReading.AccelerationY;
                        var rotation = yReading * 90;

                        // update the image
                        Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            hand.Projection.SetValue(PlaneProjection.RotationYProperty, rotation);
                        });
                    };

                    // start listening for updates
                    await accelerometer.StartReadingsAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            };
        }
    }
}
