using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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

        public static AltimeterSensor CreateAltimeterSensor(this IBandSensorManager sensorManager)
        {
            return new AltimeterSensor(sensorManager);
        }

        public static AmbientLightSensor CreateAmbientLightSensor(this IBandSensorManager sensorManager)
        {
            return new AmbientLightSensor(sensorManager);
        }

		public static CaloriesSensor CreateCaloriesSensor(this IBandSensorManager sensorManager)
		{
			return new CaloriesSensor(sensorManager);
		}

        public static BarometerSensor CreateBarometerSensor(this IBandSensorManager sensorManager)
        {
            return new BarometerSensor(sensorManager);
        }

        public static DistanceSensor CreateDistanceSensor(this IBandSensorManager sensorManager)
        {
            return new DistanceSensor(sensorManager);
        }

        public static GsrSensor CreateGsrSensor(this IBandSensorManager sensorManager)
        {
            return new GsrSensor(sensorManager);
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

        public static RRIntervalSensor CreateRRIntervalSensor(this IBandSensorManager sensorManager)
        {
            return new RRIntervalSensor(sensorManager);
        }

        public static SkinTemperatureSensor CreateSkinTemperatureSensor(this IBandSensorManager sensorManager)
        {
            return new SkinTemperatureSensor(sensorManager);
        }

        public static UVSensor CreateUVSensor(this IBandSensorManager sensorManager)
        {
            return new UVSensor(sensorManager);
        }

		public static void RequestHeartRateConsentAsync(this IBandSensorManager sensorManager, Activity activity, Action<bool> callback)
		{
			sensorManager.RequestHeartRateConsentAsync(activity, new HeartRateConsentListener(callback));
		}

		public static Task<bool> RequestHeartRateConsentTaskAsync(this IBandSensorManager sensorManager, Activity activity)
		{
			var t = new TaskCompletionSource<bool>();
			if (sensorManager.CurrentHeartRateConsent == UserConsent.Granted)
			{
				t.SetResult(true);
			}
			else
			{
				sensorManager.RequestHeartRateConsentAsync(activity, result =>
				{
					t.SetResult(result);
				});
			}
			return t.Task;
		}
	}

	internal class HeartRateConsentListener : Java.Lang.Object, IHeartRateConsentListener
	{
		private readonly Action<bool> callback;

		public HeartRateConsentListener(Action<bool> callback)
		{
			this.callback = callback;
		}

		public void UserAccepted(bool p0)
		{
			callback(p0);
		}
	}
}
