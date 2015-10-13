using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class HeartRateSensor : BandSensorBase<BandSensorHeartRateData>
	{
		public HeartRateSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartHeartRateUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandHeartRateDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopHeartRateUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandHeartRateDataEventArgs : BandSensorDataEventArgs<BandSensorHeartRateData>
	{
		public BandHeartRateDataEventArgs(BandSensorHeartRateData data, NSError error)
			: base (data, error)
		{
		}

		public BandHeartRateDataEventArgs(BandSensorHeartRateData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
