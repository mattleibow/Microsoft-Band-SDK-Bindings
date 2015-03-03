using System.Threading.Tasks;

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

		public Task StartReadingsTaskAsync()
        {
			return SensorManager.RegisterListener(listener).AsTask();
        }

		public Task StopReadingsTaskAsync()
        {
			return SensorManager.UnregisterListener(listener).AsTask();
        }
    }

    public partial class BandDistanceEventEventArgs : IBandSensorEventEventArgs<IBandDistanceEvent>
    {

    }

    public partial interface IBandDistanceEventListener : IBandSensorEventListener
    {

    }
}