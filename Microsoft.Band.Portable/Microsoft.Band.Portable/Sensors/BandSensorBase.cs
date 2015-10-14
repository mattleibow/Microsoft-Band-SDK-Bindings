using System;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Sensors
{
    public abstract class BandSensorBase<T> : IBandSensor<T>
        where T : IBandSensorReading
    {
        public event EventHandler<BandSensorReadingEventArgs<T>> ReadingChanged;

        protected void OnReadingChanged(T reading)
        {
            var handler = ReadingChanged;
            if (handler != null)
            {
                handler(this, new BandSensorReadingEventArgs<T>(reading));
            }
        }

        public virtual Task StartReadingsAsync()
        {
            return StartReadingsAsync(BandSensorSampleRate.Ms16);
        }

        public abstract Task StartReadingsAsync(BandSensorSampleRate sampleRate);

        public abstract Task StopReadingsAsync();
    }
}
