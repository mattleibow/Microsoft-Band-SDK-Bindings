using System.Threading.Tasks;

#if __ANDROID__
using Microsoft.Band.Sensors;
using NativeBandContactSensor = Microsoft.Band.Sensors.ContactSensor;
using NativeBandContactEventArgs = Microsoft.Band.Sensors.IBandSensorEventEventArgs<Microsoft.Band.Sensors.IBandContactEvent>;
#elif __IOS__
using Microsoft.Band.Sensors;
using NativeBandContactSensor = Microsoft.Band.Sensors.ContactSensor;
using NativeBandContactEventArgs = Microsoft.Band.Sensors.BandSensorDataEventArgs<Microsoft.Band.Sensors.BandSensorContactData>;
#elif WINDOWS_PHONE_APP
using NativeBandContactSensor = Microsoft.Band.Sensors.IBandSensor<Microsoft.Band.Sensors.IBandContactReading>;
using NativeBandContactEventArgs = Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandContactReading>;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandContactSensor : BandSensorBase<BandContactReading>
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandContactSensor Native;
        internal readonly BandSensorManager manager;

        internal BandContactSensor(BandSensorManager manager)
        {
            this.manager = manager;
#if __ANDROID__ || __IOS__
            this.Native = manager.Native.CreateContactSensor();
#elif WINDOWS_PHONE_APP
            this.Native = manager.Native.Contact;
#endif

            Native.ReadingChanged += OnReadingChanged;
        }

        private void OnReadingChanged(object sender, NativeBandContactEventArgs e)
        {
            var reading = e.SensorReading;
            var newReading = new BandContactReading(
#if __ANDROID__
                reading.ContactState.FromNative()
#elif __IOS__
                reading.WornState.FromNative()
#elif WINDOWS_PHONE_APP
                reading.State.FromNative()
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