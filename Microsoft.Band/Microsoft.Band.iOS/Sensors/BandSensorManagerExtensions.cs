using System.Collections.Generic;
using System.Deployment.Internal;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
	public static class BandSensorManagerExtensions
	{
		public static AccelerometerSensor CreateAccelerometerSensor(this IBandSensorManager sensorManager)
		{
			return new AccelerometerSensor(sensorManager);
		}

		public static ContactSensor CreateContactSensor(this IBandSensorManager sensorManager)
		{
			return new ContactSensor(sensorManager);
		}

		public static DistanceSensor CreateDistanceSensor(this IBandSensorManager sensorManager)
		{
			return new DistanceSensor(sensorManager);
		}

		public static GyroscopeSensor CreateGyroscopeSensor(this IBandSensorManager sensorManager)
		{
			return new GyroscopeSensor(sensorManager);
		}

		public static HeartRateSensor CreateHeartRateSensor(this IBandSensorManager sensorManager)
		{
			return new HeartRateSensor(sensorManager);
		}

		public static PedometerSensor CreatePedometerSensor(this IBandSensorManager sensorManager)
		{
			return new PedometerSensor(sensorManager);
		}

		public static SkinTemperatureSensor CreateSkinTemperatureSensor(this IBandSensorManager sensorManager)
		{
			return new SkinTemperatureSensor(sensorManager);
		}

		public static UVSensor CreateUVSensor(this IBandSensorManager sensorManager)
		{
			return new UVSensor(sensorManager);
		}

		public static CaloriesSensor CreateCaloriesSensor(this IBandSensorManager sensorManager)
		{
			return new CaloriesSensor(sensorManager);
		}

		public static AltimeterSensor CreateAltimeterSensor(this IBandSensorManager sensorManager)
		{
			return new AltimeterSensor(sensorManager);
		}

		public static AmbientLightSensor CreateAmbientLightSensor(this IBandSensorManager sensorManager)
		{
			return new AmbientLightSensor(sensorManager);
		}

		public static BarometerSensor CreateBarometerSensor(this IBandSensorManager sensorManager)
		{
			return new BarometerSensor(sensorManager);
		}

		public static GsrSensor CreateGsrSensor(this IBandSensorManager sensorManager)
		{
			return new GsrSensor(sensorManager);
		}

		public static RRIntervalSensor CreateRRIntervalSensor(this IBandSensorManager sensorManager)
		{
			return new RRIntervalSensor(sensorManager);
		}

		public static Task<bool> RequestHeartRateUserConsentTaskAsync(this IBandSensorManager sensorManager)
		{
			var tcs = new TaskCompletionSource<bool> ();
			if (sensorManager.HeartRateUserConsent == UserConsent.Granted)
			{
				tcs.SetResult(true);
			}
			else
			{
				sensorManager.RequestHeartRateUserConsentAsync (tcs.AttachCompletionHandler ());
			}
			return tcs.Task;
		}

	}
}
