using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
    public sealed class AccelerometerSensor : BandSensorBase<IBandAccelerometerEvent>
    {
        private readonly IBandAccelerometerEventListenerImplementor listener;

        public AccelerometerSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandAccelerometerEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandAccelerometerEventListener Listener
        {
            get { return listener; }
        }

        public void StartReadings(SampleRate sampleRate)
        {
			SensorManager.RegisterListener(listener, sampleRate);
        }

        public override void StartReadings()
        {
            StartReadings(SampleRate.Ms16);
        }

        public override void StopReadings()
        {
            SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandAccelerometerEventEventArgs : IBandSensorEventEventArgs<IBandAccelerometerEvent>
    {

    }

    public partial interface IBandAccelerometerEventListener : IBandSensorEventListener
    {

    }
}