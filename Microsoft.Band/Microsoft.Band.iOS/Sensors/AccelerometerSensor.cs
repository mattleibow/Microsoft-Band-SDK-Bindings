using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class AccelerometerSensor : BandSensorBase<BandSensorAccelerometerData>
	{
		public AccelerometerSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartAccelerometerUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged (new BandAccelerometerDataEventArgs (data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopAccelerometerUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandAccelerometerDataEventArgs : BandSensorDataEventArgs<BandSensorAccelerometerData>
	{
		public BandAccelerometerDataEventArgs (BandSensorAccelerometerData data, NSError error)
			: base (data, error)
		{
		}

		public BandAccelerometerDataEventArgs (BandSensorAccelerometerData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
