using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandCaloriesSensor = Microsoft.Band.Sensors.CaloriesSensor;
using NativeBandCaloriesEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandCaloriesEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandCaloriesSensor = Microsoft.Band.Sensors.CaloriesSensor;
using NativeBandCaloriesEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorCaloriesData>;
#elif WINDOWS_PHONE_APP
using NativeBandCaloriesSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandCaloriesReading>;
using NativeBandCaloriesEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandCaloriesReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandCaloriesSensor : BandSensorBase<BandCaloriesReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandCaloriesSensor Native;
        internal readonly BandSensorManager manager;

        internal BandCaloriesSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateCaloriesSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Calories;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandCaloriesEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandCaloriesReading(
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
                (long)reading.Calories, (long)reading.CaloriesToday
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