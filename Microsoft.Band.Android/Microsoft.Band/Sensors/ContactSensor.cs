using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class ContactSensor : BandSensorBase<IBandContactEvent>
    {
        private readonly IBandContactEventListenerImplementor listener;

        public ContactSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandContactEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandContactEventListener Listener
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

    public partial class BandContactEventEventArgs : IBandSensorEventEventArgs<IBandContactEvent>
    {

    }

    public partial interface IBandContactEventListener : IBandSensorEventListener
    {

    }
}