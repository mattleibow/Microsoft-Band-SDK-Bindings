using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class PedometerSensor : BandSensorBase<BandSensorPedometerData>
	{
		public PedometerSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartPedometerUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandPedometerDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopPedometerUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandPedometerDataEventArgs : BandSensorDataEventArgs<BandSensorPedometerData>
	{
		public BandPedometerDataEventArgs(BandSensorPedometerData data, NSError error)
			: base (data, error)
		{
		}

		public BandPedometerDataEventArgs(BandSensorPedometerData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
