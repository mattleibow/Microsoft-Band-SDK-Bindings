using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class GsrSensor : BandSensorBase<IBandGsrEvent>
    {
        private readonly IBandGsrEventListenerImplementor listener;

        public GsrSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandGsrEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandGsrEventListener Listener
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

    public partial class BandGsrEventEventArgs : IBandSensorEventEventArgs<IBandGsrEvent>
    {

    }

    public partial interface IBandGsrEventListener : IBandSensorEventListener
    {

    }
}