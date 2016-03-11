using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandPedometerSensor = Microsoft.Band.Sensors.PedometerSensor;
using NativeBandPedometerEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandPedometerEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandPedometerSensor = Microsoft.Band.Sensors.PedometerSensor;
using NativeBandPedometerEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorPedometerData>;
#elif WINDOWS_PHONE_APP
using NativeBandPedometerSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandPedometerReading>;
using NativeBandPedometerEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandPedometerReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandPedometerSensor : BandSensorBase<BandPedometerReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandPedometerSensor Native;
        internal readonly BandSensorManager manager;

        internal BandPedometerSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreatePedometerSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Pedometer;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandPedometerEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandPedometerReading(
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
                (long)reading.TotalSteps, (long)reading.StepsToday
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