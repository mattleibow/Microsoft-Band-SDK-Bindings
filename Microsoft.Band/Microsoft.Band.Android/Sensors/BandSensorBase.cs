using System;

namespace Microsoft.Band.Sensors
{
    // base type for each sensor

    public abstract class BandSensorBase<T>
        where T : IBandSensorEvent
    {
        protected IBandSensorManager SensorManager;

        protected BandSensorBase(IBandSensorManager sensorManager)
        {
            SensorManager = sensorManager;
        }

        public event EventHandler<IBandSensorEventEventArgs<T>> ReadingChanged;

        protected virtual void OnReadingChanged(IBandSensorEventEventArgs<T> e)
        {
            var handler = ReadingChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public abstract void StartReadings();

        public abstract void StopReadings();
    }

    // base type for sensor listeners event args

    public interface IBandSensorEventListener
    {
    }

    public interface IBandSensorEventEventArgs<T>
        where T : IBandSensorEvent
    {
        T SensorReading { get; }
    }
}