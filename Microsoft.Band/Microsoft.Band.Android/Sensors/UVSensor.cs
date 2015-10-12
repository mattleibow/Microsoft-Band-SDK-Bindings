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

        public override void StartReadings()
        {
			SensorManager.RegisterListener(listener);
        }

        public override void StopReadings()
        {
			SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandUVEventEventArgs : IBandSensorEventEventArgs<IBandUVEvent>
    {

    }

    public partial interface IBandUVEventListener : IBandSensorEventListener
    {

    }
}