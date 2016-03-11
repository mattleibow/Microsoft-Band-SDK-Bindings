using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandUltravioletLightSensor = Microsoft.Band.Sensors.UVSensor;
using NativeBandUltravioletLightEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandUVEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandUltravioletLightSensor = Microsoft.Band.Sensors.UVSensor;
using NativeBandUltravioletLightEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorUVData>;
#elif WINDOWS_PHONE_APP
using NativeBandUltravioletLightSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandUVReading>;
using NativeBandUltravioletLightEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandUVReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandUltravioletLightSensor : BandSensorBase<BandUltravioletLightReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandUltravioletLightSensor Native;
        internal readonly BandSensorManager manager;

        internal BandUltravioletLightSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateUVSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.UV;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandUltravioletLightEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandUltravioletLightReading(
#if __ANDROID__
                reading.UVIndexLevel.FromNative(), reading.UVExposureToday
#elif __IOS__
                reading.UVIndexLevel.FromNative(), (long)reading.ExposureToday
#elif WINDOWS_PHONE_APP
                reading.IndexLevel.FromNative(), reading.ExposureToday
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