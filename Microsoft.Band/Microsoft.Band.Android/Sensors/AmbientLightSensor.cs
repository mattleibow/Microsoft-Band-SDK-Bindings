using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class AmbientLightSensor : BandSensorBase<IBandAmbientLightEvent>
    {
        private readonly IBandAmbientLightEventListenerImplementor listener;

        public AmbientLightSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandAmbientLightEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandAmbientLightEventListener Listener
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

    public partial class BandAmbientLightEventEventArgs : IBandSensorEventEventArgs<IBandAmbientLightEvent>
    {

    }

    public partial interface IBandAmbientLightEventListener : IBandSensorEventListener
    {

    }
}