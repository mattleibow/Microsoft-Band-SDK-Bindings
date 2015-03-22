using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class UVSensor : BandSensorBase<BandSensorUVData>
	{
		public UVSensor (IBandSensorManager sensorManager)
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
			SensorManager.StartUVUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandUVDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopUVUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandUVDataEventArgs : BandSensorDataEventArgs<BandSensorUVData>
	{
		public BandUVDataEventArgs(BandSensorUVData data, NSError error)
			: base (data, error)
		{
		}

		public BandUVDataEventArgs(BandSensorUVData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
