using System;

namespace Microsoft.Band.Portable.Sensors
{
    public class BandSensorReadingEventArgs<T> : EventArgs
        where T : IBandSensorReading
    {
        public BandSensorReadingEventArgs(T reading)
        {
            this.SensorReading = reading;
        }

        public T SensorReading { get; private set; }
    }
}