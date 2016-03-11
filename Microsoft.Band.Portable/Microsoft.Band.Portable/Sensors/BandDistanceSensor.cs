using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandDistanceSensor = Microsoft.Band.Sensors.DistanceSensor;
using NativeBandDistanceEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandDistanceEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandDistanceSensor = Microsoft.Band.Sensors.DistanceSensor;
using NativeBandDistanceEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorDistanceData>;
#elif WINDOWS_PHONE_APP
using NativeBandDistanceSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandDistanceReading>;
using NativeBandDistanceEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandDistanceReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandDistanceSensor : BandSensorBase<BandDistanceReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandDistanceSensor Native;
        internal readonly BandSensorManager manager;

        internal BandDistanceSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateDistanceSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Distance;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandDistanceEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandDistanceReading(
#if __ANDROID__ || __IOS__
                reading.MotionType.FromNative(), reading.Pace, reading.Speed, (long)reading.TotalDistance, (long)reading.DistanceToday
#elif WINDOWS_PHONE_APP
                reading.CurrentMotion.FromNative(), reading.Pace, reading.Speed, reading.TotalDistance, reading.DistanceToday
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