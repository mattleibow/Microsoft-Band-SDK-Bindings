using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class ContactSensor : BandSensorBase<BandSensorContactData>
	{
		public ContactSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartBandContactUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged(new BandContactDataEventArgs(data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopBandContactUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandContactDataEventArgs : BandSensorDataEventArgs<BandSensorContactData>
	{
		public BandContactDataEventArgs(BandSensorContactData data, NSError error)
			: base (data, error)
		{
		}

		public BandContactDataEventArgs(BandSensorContactData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
