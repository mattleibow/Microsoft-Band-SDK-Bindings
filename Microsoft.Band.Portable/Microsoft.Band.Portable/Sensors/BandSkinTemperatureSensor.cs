using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandSkinTemperatureSensor = Microsoft.Band.Sensors.SkinTemperatureSensor;
using NativeBandSkinTemperatureEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandSkinTemperatureEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandSkinTemperatureSensor = Microsoft.Band.Sensors.SkinTemperatureSensor;
using NativeBandSkinTemperatureEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorSkinTemperatureData>;
#elif WINDOWS_PHONE_APP
using NativeBandSkinTemperatureSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandSkinTemperatureReading>;
using NativeBandSkinTemperatureEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandSkinTemperatureReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandSkinTemperatureSensor : BandSensorBase<BandSkinTemperatureReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandSkinTemperatureSensor Native;
        internal readonly BandSensorManager manager;

        internal BandSkinTemperatureSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateSkinTemperatureSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.SkinTemperature;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandSkinTemperatureEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandSkinTemperatureReading(
#if __ANDROID__
                reading.Temperature
#elif __IOS__
                reading.Temperature
#elif WINDOWS_PHONE_APP
                reading.Temperature
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