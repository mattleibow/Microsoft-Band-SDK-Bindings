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

        public IBandPendingResult StartReadings(SampleRate sampleRate)
        {
            return SensorManager.RegisterListener(listener, sampleRate);
        }

        public IBandPendingResult StopReadings()
        {
            return SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandGyroscopeEventEventArgs : IBandSensorEventEventArgs<IBandGyroscopeEvent>
    {

    }

    public partial interface IBandGyroscopeEventListener : IBandSensorEventListener
    {

    }
}