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

        public Task StartReadingsTaskAsync(SampleRate sampleRate)
        {
			return SensorManager.RegisterListener(listener, sampleRate).AsTask();
        }

        public Task StopReadingsTaskAsync()
        {
			return SensorManager.UnregisterListener(listener).AsTask();
        }
    }

    public partial class BandAccelerometerEventEventArgs : IBandSensorEventEventArgs<IBandAccelerometerEvent>
    {

    }

    public partial interface IBandAccelerometerEventListener : IBandSensorEventListener
    {

    }
}