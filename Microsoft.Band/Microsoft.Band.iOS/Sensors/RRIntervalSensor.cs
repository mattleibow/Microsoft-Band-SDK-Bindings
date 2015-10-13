using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class RRIntervalSensor : BandSensorBase<BandSensorRRIntervalData>
	{
		public RRIntervalSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartRRIntervalUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged (new BandRRIntervalDataEventArgs (data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopRRIntervalUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandRRIntervalDataEventArgs : BandSensorDataEventArgs<BandSensorRRIntervalData>
	{
		public BandRRIntervalDataEventArgs (BandSensorRRIntervalData data, NSError error)
			: base (data, error)
		{
		}

		public BandRRIntervalDataEventArgs (BandSensorRRIntervalData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
