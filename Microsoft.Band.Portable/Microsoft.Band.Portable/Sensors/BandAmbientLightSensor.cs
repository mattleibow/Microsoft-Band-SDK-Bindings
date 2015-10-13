using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandAmbientLightSensor = Microsoft.Band.Sensors.AmbientLightSensor;
using NativeBandAmbientLightEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandAmbientLightEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandAmbientLightSensor = Microsoft.Band.Sensors.AmbientLightSensor;
using NativeBandAmbientLightEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorAmbientLightData>;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Sensors;
using NativeBandAmbientLightSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandAmbientLightReading>;
using NativeBandAmbientLightEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandAmbientLightReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandAmbientLightSensor : BandSensorBase<BandAmbientLightReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandAmbientLightSensor Native;
        internal readonly BandSensorManager manager;

        internal BandAmbientLightSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateAmbientLightSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.AmbientLight;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandAmbientLightEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandAmbientLightReading(
#if __ANDROID__
                reading.Brightness
#elif __IOS__
				reading.Brightness
#elif WINDOWS_PHONE_APP
                reading.Brightness
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