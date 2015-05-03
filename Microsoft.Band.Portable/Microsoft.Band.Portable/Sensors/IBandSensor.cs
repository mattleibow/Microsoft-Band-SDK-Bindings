using System;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Sensors
{
    public interface IBandSensor<T>
        where T : IBandSensorReading
    {
        event EventHandler<BandSensorReadingEventArgs<T>> ReadingChanged;

        Task StartReadingsAsync(BandSensorSampleRate sampleRate);

        Task StopReadingsAsync();
    }
}
