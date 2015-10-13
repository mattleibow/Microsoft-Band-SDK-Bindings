using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class BarometerSensor : BandSensorBase<BandSensorBarometerData>
	{
		public BarometerSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartBarometerUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged (new BandBarometerDataEventArgs (data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopBarometerUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandBarometerDataEventArgs : BandSensorDataEventArgs<BandSensorBarometerData>
	{
		public BandBarometerDataEventArgs (BandSensorBarometerData data, NSError error)
			: base (data, error)
		{
		}

		public BandBarometerDataEventArgs (BandSensorBarometerData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
