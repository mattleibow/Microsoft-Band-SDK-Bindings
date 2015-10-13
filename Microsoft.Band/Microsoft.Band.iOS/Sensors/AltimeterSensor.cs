using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class AltimeterSensor : BandSensorBase<BandSensorAltimeterData>
	{
		public AltimeterSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartAltimeterUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged (new BandAltimeterDataEventArgs (data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopAltimeterUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandAltimeterDataEventArgs : BandSensorDataEventArgs<BandSensorAltimeterData>
	{
		public BandAltimeterDataEventArgs (BandSensorAltimeterData data, NSError error)
			: base (data, error)
		{
		}

		public BandAltimeterDataEventArgs (BandSensorAltimeterData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
