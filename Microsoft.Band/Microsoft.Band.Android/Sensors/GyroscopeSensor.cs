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

        public override void StartReadings()
        {
            StartReadings(SampleRate.Ms16);
        }

		public void StartReadings(SampleRate sampleRate)
        {
			SensorManager.RegisterListener(listener, sampleRate);
        }

		public override void StopReadings()
        {
			SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandGyroscopeEventEventArgs : IBandSensorEventEventArgs<IBandGyroscopeEvent>
    {

    }

    public partial interface IBandGyroscopeEventListener : IBandSensorEventListener
    {

    }
}