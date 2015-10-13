using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandBarometerSensor = Microsoft.Band.Sensors.BarometerSensor;
using NativeBandBarometerEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandBarometerEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandBarometerSensor = Microsoft.Band.Sensors.BarometerSensor;
using NativeBandBarometerEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorBarometerData>;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Sensors;
using NativeBandBarometerSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandBarometerReading>;
using NativeBandBarometerEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandBarometerReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandBarometerSensor : BandSensorBase<BandBarometerReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandBarometerSensor Native;
        internal readonly BandSensorManager manager;

        internal BandBarometerSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateBarometerSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Barometer;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandBarometerEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandBarometerReading(
#if __ANDROID__
                reading.AirPressure, reading.Temperature
#elif __IOS__
				reading.AirPressure, reading.Temperature
#elif WINDOWS_PHONE_APP
                reading.AirPressure, reading.Temperature
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