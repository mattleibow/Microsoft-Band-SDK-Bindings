using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class RRIntervalSensor : BandSensorBase<IBandRRIntervalEvent>
    {
        private readonly IBandRRIntervalEventListenerImplementor listener;

        public RRIntervalSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandRRIntervalEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandRRIntervalEventListener Listener
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

    public partial class BandRRIntervalEventEventArgs : IBandSensorEventEventArgs<IBandRRIntervalEvent>
    {

    }

    public partial interface IBandRRIntervalEventListener : IBandSensorEventListener
    {

    }
}