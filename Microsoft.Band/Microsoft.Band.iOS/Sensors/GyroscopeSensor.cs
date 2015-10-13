using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class GyroscopeSensor : BandSensorBase<BandSensorGyroscopeData>
	{
		public GyroscopeSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartGyroscopeUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandGyroscopeDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopGyroscopeUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandGyroscopeDataEventArgs : BandSensorDataEventArgs<BandSensorGyroscopeData>
	{
		public BandGyroscopeDataEventArgs(BandSensorGyroscopeData data, NSError error)
			: base (data, error)
		{
		}

		public BandGyroscopeDataEventArgs(BandSensorGyroscopeData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
