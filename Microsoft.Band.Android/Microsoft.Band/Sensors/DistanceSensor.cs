namespace Microsoft.Band.Sensors
{
    public sealed class DistanceSensor : BandSensorBase<IBandDistanceEvent>
    {
        private readonly IBandDistanceEventListenerImplementor listener;

        public DistanceSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandDistanceEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandDistanceEventListener Listener
        {
            get { return listener; }
        }

        public IBandPendingResult StartReadings()
        {
            return SensorManager.RegisterListener(listener);
        }

        public IBandPendingResult StopReadings()
        {
            return SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandDistanceEventEventArgs : IBandSensorEventEventArgs<IBandDistanceEvent>
    {

    }

    public partial interface IBandDistanceEventListener : IBandSensorEventListener
    {

    }
}