using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class PedometerSensor : BandSensorBase<IBandPedometerEvent>
    {
        private readonly IBandPedometerEventListenerImplementor listener;

        public PedometerSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandPedometerEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandPedometerEventListener Listener
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

    public partial class BandPedometerEventEventArgs : IBandSensorEventEventArgs<IBandPedometerEvent>
    {

    }

    public partial interface IBandPedometerEventListener : IBandSensorEventListener
    {

    }
}