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

        public override void StartReadings()
        {
			SensorManager.RegisterListener(listener);
        }

        public override void StopReadings()
        {
			SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandPedometerEventEventArgs : IBandSensorEventEventArgs<IBandPedometerEvent>
    {

    }

    public partial interface IBandPedometerEventListener : IBandSensorEventListener
    {

    }
}