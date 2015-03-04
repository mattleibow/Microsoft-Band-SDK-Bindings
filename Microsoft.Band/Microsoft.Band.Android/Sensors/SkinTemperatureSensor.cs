using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class SkinTemperatureSensor : BandSensorBase<IBandSkinTemperatureEvent>
    {
        private readonly IBandSkinTemperatureEventListenerImplementor listener;

        public SkinTemperatureSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandSkinTemperatureEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandSkinTemperatureEventListener Listener
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

    public partial class BandSkinTemperatureEventEventArgs : IBandSensorEventEventArgs<IBandSkinTemperatureEvent>
    {

    }

    public partial interface IBandSkinTemperatureEventListener : IBandSensorEventListener
    {

    }
}