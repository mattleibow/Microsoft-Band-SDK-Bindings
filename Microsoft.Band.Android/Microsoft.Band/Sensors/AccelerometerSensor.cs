namespace Microsoft.Band.Sensors
{
    public sealed class AccelerometerSensor : BandSensorBase<IBandAccelerometerEvent>
    {
        private readonly IBandAccelerometerEventListenerImplementor listener;

        public AccelerometerSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandAccelerometerEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandAccelerometerEventListener Listener
        {
            get { return listener; }
        }

        public IBandPendingResult StartReadings(SampleRate sampleRate)
        {
            return SensorManager.RegisterListener(listener, sampleRate);
        }

        public IBandPendingResult StopReadings()
        {
            return SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandAccelerometerEventEventArgs : IBandSensorEventEventArgs<IBandAccelerometerEvent>
    {

    }

    public partial interface IBandAccelerometerEventListener : IBandSensorEventListener
    {

    }
}