using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class CaloriesSensor : BandSensorBase<BandSensorCaloriesData>
	{
		public CaloriesSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public void StartReadings ()
		{
			StartReadings (null);
		}

		public void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartCaloriesUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandCaloriesDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopCaloriesUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandCaloriesDataEventArgs : BandSensorDataEventArgs<BandSensorCaloriesData>
	{
		public BandCaloriesDataEventArgs(BandSensorCaloriesData data, NSError error)
			: base (data, error)
		{
		}

		public BandCaloriesDataEventArgs(BandSensorCaloriesData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
