using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class AltimeterSensor : BandSensorBase<IBandAltimeterEvent>
    {
        private readonly IBandAltimeterEventListenerImplementor listener;

        public AltimeterSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandAltimeterEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandAltimeterEventListener Listener
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

    public partial class BandAltimeterEventEventArgs : IBandSensorEventEventArgs<IBandAltimeterEvent>
    {

    }

    public partial interface IBandAltimeterEventListener : IBandSensorEventListener
    {

    }
}