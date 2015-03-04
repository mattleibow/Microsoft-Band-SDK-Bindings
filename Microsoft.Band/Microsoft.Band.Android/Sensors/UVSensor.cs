using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class UVSensor : BandSensorBase<IBandUVEvent>
    {
        private readonly IBandUVEventListenerImplementor listener;

        public UVSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandUVEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandUVEventListener Listener
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

    public partial class BandUVEventEventArgs : IBandSensorEventEventArgs<IBandUVEvent>
    {

    }

    public partial interface IBandUVEventListener : IBandSensorEventListener
    {

    }
}