using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandAltimeterSensor = Microsoft.Band.Sensors.AltimeterSensor;
using NativeBandAltimeterEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandAltimeterEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandAltimeterSensor = Microsoft.Band.Sensors.AltimeterSensor;
using NativeBandAltimeterEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorAltimeterData>;
#elif WINDOWS_PHONE_APP
using Microsoft.Band.Sensors;
using NativeBandAltimeterSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandAltimeterReading>;
using NativeBandAltimeterEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandAltimeterReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandAltimeterSensor : BandSensorBase<BandAltimeterReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandAltimeterSensor Native;
        internal readonly BandSensorManager manager;

        internal BandAltimeterSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateAltimeterSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Altimeter;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandAltimeterEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandAltimeterReading(
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
                (long)reading.FlightsAscended, 
                (long)reading.FlightsDescended, 
                (double)reading.Rate, 
                (long)reading.SteppingGain, 
                (long)reading.SteppingLoss, 
                (long)reading.StepsAscended, 
                (long)reading.StepsDescended, 
                (long)reading.TotalGain, 
                (long)reading.TotalLoss, 
                (long)reading.FlightsAscendedToday,
                (long)reading.TotalGainToday
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