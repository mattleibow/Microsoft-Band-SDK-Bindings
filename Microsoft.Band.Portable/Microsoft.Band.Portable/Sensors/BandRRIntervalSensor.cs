using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandRRIntervalSensor = Microsoft.Band.Sensors.RRIntervalSensor;
using NativeBandRRIntervalEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandRRIntervalEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandRRIntervalSensor = Microsoft.Band.Sensors.RRIntervalSensor;
using NativeBandRRIntervalEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorRRIntervalData>;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Sensors;
using NativeBandRRIntervalSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandRRIntervalReading>;
using NativeBandRRIntervalEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandRRIntervalReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandRRIntervalSensor : BandSensorBase<BandRRIntervalReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandRRIntervalSensor Native;
        internal readonly BandSensorManager manager;

        internal BandRRIntervalSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateRRIntervalSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.RRInterval;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandRRIntervalEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandRRIntervalReading(
#if __ANDROID__
                reading.Interval
#elif __IOS__
				reading.Interval
#elif WINDOWS_PHONE_APP
                reading.Interval
#endif
                );
            OnReadingChanged(newReading);
        }
#endif

        public override async Task StartReadingsAsync(BandSensorSampleRate sampleRate)
        {
#if __ANDROID__
            Native.StartReadings();
#elif __IOS__
            Native.StartReadings();
#elif WINDOWS_PHONE_APP
            Native.ApplySampleRate(sampleRate);
            await Native.StartReadingsAsync();
#endif
        }

        public override async Task StopReadingsAsync()
        {
#if __ANDROID__
            Native.StopReadings();
#elif __IOS__
            Native.StopReadings();
#elif WINDOWS_PHONE_APP
            await Native.StopReadingsAsync();
#endif
        }
    }
}