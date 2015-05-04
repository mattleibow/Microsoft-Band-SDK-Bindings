using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
	public sealed class CaloriesSensor : BandSensorBase<IBandCaloriesEvent>
    {
        private readonly IBandCaloriesEventListenerImplementor listener;

        public CaloriesSensor(IBandSensorManager sensorManager)
            : base(sensorManager)
        {
            listener = new IBandCaloriesEventListenerImplementor(this);
            listener.Handler += (sender, e) => OnReadingChanged(e);
        }

        public IBandCaloriesEventListener Listener
        {
            get { return listener; }
        }

		public void StartReadings()
        {
			SensorManager.RegisterListener(listener);
        }

		public void StopReadings()
        {
			SensorManager.UnregisterListener(listener);
        }
    }

	public partial class BandCaloriesEventEventArgs : IBandSensorEventEventArgs<IBandCaloriesEvent>
    {

    }

    public partial interface IBandCaloriesEventListener : IBandSensorEventListener
    {

    }
}