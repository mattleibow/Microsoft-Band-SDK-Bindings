using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class GyroscopeSensor : BandSensorBase<IBandGyroscopeEvent>
    {
        private readonly IBandGyroscopeEventListenerImplementor listener;

        public GyroscopeSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandGyroscopeEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandGyroscopeEventListener Listener
        {
            get { return listener; }
        }

		public Task StartReadingsTaskAsync(SampleRate sampleRate)
        {
			return SensorManager.RegisterListener(listener, sampleRate).AsTask();
        }

		public Task StopReadingsTaskAsync()
        {
			return SensorManager.UnregisterListener(listener).AsTask();
        }
    }

    public partial class BandGyroscopeEventEventArgs : IBandSensorEventEventArgs<IBandGyroscopeEvent>
    {

    }

    public partial interface IBandGyroscopeEventListener : IBandSensorEventListener
    {

    }
}