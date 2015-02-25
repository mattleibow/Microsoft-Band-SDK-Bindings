using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;

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
    }
}
