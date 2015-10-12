using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class BarometerSensor : BandSensorBase<IBandBarometerEvent>
    {
        private readonly IBandBarometerEventListenerImplementor listener;

        public BarometerSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandBarometerEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandBarometerEventListener Listener
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

    public partial class BandBarometerEventEventArgs : IBandSensorEventEventArgs<IBandBarometerEvent>
    {

    }

    public partial interface IBandBarometerEventListener : IBandSensorEventListener
    {

    }
}