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

        public IBandPendingResult StartReadings()
        {
            return SensorManager.RegisterListener(listener);
        }

        public IBandPendingResult StopReadings()
        {
            return SensorManager.UnregisterListener(listener);
        }
    }

    public partial class BandSkinTemperatureEventEventArgs : IBandSensorEventEventArgs<IBandSkinTemperatureEvent>
    {

    }

    public partial interface IBandSkinTemperatureEventListener : IBandSensorEventListener
    {

    }
}