using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class GsrSensor : BandSensorBase<BandSensorGsrData>
	{
		public GsrSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartGsrUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged (new BandGsrDataEventArgs (data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopGsrUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandGsrDataEventArgs : BandSensorDataEventArgs<BandSensorGsrData>
	{
		public BandGsrDataEventArgs (BandSensorGsrData data, NSError error)
			: base (data, error)
		{
		}

		public BandGsrDataEventArgs (BandSensorGsrData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
