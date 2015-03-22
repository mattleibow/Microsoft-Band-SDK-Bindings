using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class SkinTemperatureSensor : BandSensorBase<BandSensorSkinTemperatureData>
	{
		public SkinTemperatureSensor (IBandSensorManager sensorManager)
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
			SensorManager.StartSkinTemperatureUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandSkinTemperatureDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopSkinTemperatureUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandSkinTemperatureDataEventArgs : BandSensorDataEventArgs<BandSensorSkinTemperatureData>
	{
		public BandSkinTemperatureDataEventArgs(BandSensorSkinTemperatureData data, NSError error)
			: base (data, error)
		{
		}

		public BandSkinTemperatureDataEventArgs(BandSensorSkinTemperatureData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
