using Foundation;

namespace Microsoft.Band.Sensors
{
	public sealed class AmbientLightSensor : BandSensorBase<BandSensorAmbientLightData>
	{
		public AmbientLightSensor (IBandSensorManager sensorManager)
			: base (sensorManager)
		{
		}

		public override void StartReadings (NSOperationQueue queue)
		{
			NSError operationError;
			SensorManager.StartAmbientLightUpdates (queue, out operationError, (data, callbackError) => {
				OnReadingChanged (new BandAmbientLightDataEventArgs (data, callbackError));
			});
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}

		public override void StopReadings ()
		{
			NSError operationError;
			SensorManager.StopAmbientLightUpdates (out operationError);
			if (operationError != null) {
				throw new BandException (operationError);
			}
		}
	}

	public class BandAmbientLightDataEventArgs : BandSensorDataEventArgs<BandSensorAmbientLightData>
	{
		public BandAmbientLightDataEventArgs (BandSensorAmbientLightData data, NSError error)
			: base (data, error)
		{
		}

		public BandAmbientLightDataEventArgs (BandSensorAmbientLightData data, BandException exception)
			: base (data, exception)
		{
		}
	}
}
