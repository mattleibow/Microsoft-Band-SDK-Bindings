using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class DistanceSensor : BandSensorBase<BandSensorDistanceData>
	{
		public DistanceSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartDistanceUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandDistanceDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopDistanceUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandDistanceDataEventArgs : BandSensorDataEventArgs<BandSensorDistanceData>
	{
		public BandDistanceDataEventArgs(BandSensorDistanceData data, NSError error)
			: base (data, error)
		{
		}

		public BandDistanceDataEventArgs(BandSensorDistanceData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
