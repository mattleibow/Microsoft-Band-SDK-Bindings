using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class HeartRateSensor : BandSensorBase<IBandHeartRateEvent>
    {
        private readonly IBandHeartRateEventListenerImplementor listener;

        public HeartRateSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandHeartRateEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandHeartRateEventListener Listener
        {
            get { return listener; }
        }

        public override void StartReadings()
        {
			SensorManager.RegisterListener(listener);
        }

        public override void StopReadings()
        {
			SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandHeartRateEventEventArgs : IBandSensorEventEventArgs<IBandHeartRateEvent>
    {

    }

    public partial interface IBandHeartRateEventListener : IBandSensorEventListener
    {

    }
}