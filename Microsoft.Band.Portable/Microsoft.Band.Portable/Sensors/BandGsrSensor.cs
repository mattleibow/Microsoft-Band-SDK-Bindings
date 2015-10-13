using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandGsrSensor = Microsoft.Band.Sensors.GsrSensor;
using NativeBandGsrEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandGsrEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandGsrSensor = Microsoft.Band.Sensors.GsrSensor;
using NativeBandGsrEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorGsrData>;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Sensors;
using NativeBandGsrSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandGsrReading>;
using NativeBandGsrEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandGsrReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandGsrSensor : BandSensorBase<BandGsrReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandGsrSensor Native;
        internal readonly BandSensorManager manager;

        internal BandGsrSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateGsrSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Gsr;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandGsrEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandGsrReading(
#if __ANDROID__
                reading.Resistance
#elif __IOS__
				(long)reading.Resistance
#elif WINDOWS_PHONE_APP
                reading.Resistance
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