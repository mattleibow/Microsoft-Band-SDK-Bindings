using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandHeartRateSensor = Microsoft.Band.Sensors.HeartRateSensor;
using NativeBandHeartRateEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandHeartRateEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandHeartRateSensor = Microsoft.Band.Sensors.HeartRateSensor;
using NativeBandHeartRateEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorHeartRateData>;
#elif WINDOWS_PHONE_APP
using NativeBandHeartRateSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandHeartRateReading>;
using NativeBandHeartRateEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandHeartRateReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandHeartRateSensor : BandSensorBase<BandHeartRateReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandHeartRateSensor Native;
        internal readonly BandSensorManager manager;

        internal BandHeartRateSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateHeartRateSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.HeartRate;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandHeartRateEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandHeartRateReading(
#if __ANDROID__
                reading.Quality.FromNative(), reading.HeartRate
#elif __IOS__
                reading.Quality.FromNative(), (int)reading.HeartRate
#elif WINDOWS_PHONE_APP
                reading.Quality.FromNative(), reading.HeartRate
#endif
                );
            OnReadingChanged(newReading);
        }
#endif

        public override async Task StartReadingsAsync(BandSensorSampleRate sampleRate)
        {
#if __ANDROID__
            await Native.StartReadingsTaskAsync();
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
            await Native.StopReadingsTaskAsync();
#elif __IOS__
            Native.StopReadings();
#elif WINDOWS_PHONE_APP
            await Native.StopReadingsAsync();
#endif
        }
    }
}