using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandAccelerometerSensor = Microsoft.Band.Sensors.AccelerometerSensor;
using NativeBandAccelerometerEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandAccelerometerEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandAccelerometerSensor = Microsoft.Band.Sensors.AccelerometerSensor;
using NativeBandAccelerometerEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorAccelerometerData>;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Sensors;
using NativeBandAccelerometerSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandAccelerometerReading>;
using NativeBandAccelerometerEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandAccelerometerReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandAccelerometerSensor : BandSensorBase<BandAccelerometerReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandAccelerometerSensor Native;
        internal readonly BandSensorManager manager;

        internal BandAccelerometerSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateAccelerometerSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Accelerometer;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandAccelerometerEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandAccelerometerReading(
#if __ANDROID__
                (double)reading.AccelerationX, (double)reading.AccelerationY, (double)reading.AccelerationZ
#elif __IOS__
				reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ
#elif WINDOWS_PHONE_APP
                (double)reading.AccelerationX, (double)reading.AccelerationY, (double)reading.AccelerationZ
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