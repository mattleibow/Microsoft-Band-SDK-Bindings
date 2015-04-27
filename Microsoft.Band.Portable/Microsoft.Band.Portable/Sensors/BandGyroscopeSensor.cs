using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandGyroscopeSensor = Microsoft.Band.Sensors.GyroscopeSensor;
using NativeBandGyroscopeEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandGyroscopeEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandGyroscopeSensor = Microsoft.Band.Sensors.GyroscopeSensor;
using NativeBandGyroscopeEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorGyroscopeData>;
#elif WINDOWS_PHONE_APP
using NativeBandGyroscopeSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandGyroscopeReading>;
using NativeBandGyroscopeEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandGyroscopeReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandGyroscopeSensor : BandSensorBase<BandGyroscopeReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandGyroscopeSensor Native;
        internal readonly BandSensorManager manager;

        internal BandGyroscopeSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateGyroscopeSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Gyroscope;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandGyroscopeEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandGyroscopeReading(
#if __ANDROID__
                reading.AngularVelocityX, reading.AngularVelocityY, reading.AngularVelocityZ
#elif __IOS__
				reading.AngularVelocityX, reading.AngularVelocityY, reading.AngularVelocityZ
#elif WINDOWS_PHONE_APP
                reading.AngularVelocityX, reading.AngularVelocityY, reading.AngularVelocityZ
#endif
                );
            OnReadingChanged(newReading);
        }
#endif

        public override async Task StartReadingsAsync(BandSensorSampleRate sampleRate)
        {
#if __ANDROID__
            Native.StartReadings(sampleRate.ToNative());
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