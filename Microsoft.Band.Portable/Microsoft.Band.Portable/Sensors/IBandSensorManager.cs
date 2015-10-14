using System;

#if __ANDROID__
using NativeBandSensorManager = Microsoft.Band.Sensors.IBandSensorManager;
#elif __IOS__
using NativeBandSensorManager = Microsoft.Band.Sensors.IBandSensorManager;
#elif WINDOWS_PHONE_APP
using NativeBandSensorManager = Microsoft.Band.Sensors.IBandSensorManager;
#endif

namespace Microsoft.Band.Portable.Sensors
{
    public class BandSensorManager
    {
        private readonly Lazy<BandAccelerometerSensor> accelerometer;
        private readonly Lazy<BandAltimeterSensor> altimeter;
        private readonly Lazy<BandAmbientLightSensor> ambientLight;
        private readonly Lazy<BandBarometerSensor> barometer;
        private readonly Lazy<BandCaloriesSensor> calories;
        private readonly Lazy<BandContactSensor> contact;
        private readonly Lazy<BandDistanceSensor> distance;
        private readonly Lazy<BandGyroscopeSensor> gyroscope;
        private readonly Lazy<BandGsrSensor> gsr;
        private readonly Lazy<BandHeartRateSensor> heartRate;
        private readonly Lazy<BandPedometerSensor> pedometer;
        private readonly Lazy<BandRRIntervalSensor> rrInterval;
        private readonly Lazy<BandSkinTemperatureSensor> skinTemperature;
        private readonly Lazy<BandUltravioletLightSensor> ultravioletLight;

        private readonly BandClient client;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandSensorManager Native;

        internal BandSensorManager(BandClient client, NativeBandSensorManager sensorManager)
        {
            this.Native = sensorManager;

            this.client = client;

            this.accelerometer = new Lazy<BandAccelerometerSensor>(() => new BandAccelerometerSensor(this));
            this.altimeter = new Lazy<BandAltimeterSensor>(() => new BandAltimeterSensor(this));
            this.ambientLight = new Lazy<BandAmbientLightSensor>(() => new BandAmbientLightSensor(this));
            this.barometer = new Lazy<BandBarometerSensor>(() => new BandBarometerSensor(this));
            this.calories = new Lazy<BandCaloriesSensor>(() => new BandCaloriesSensor(this));
            this.contact = new Lazy<BandContactSensor>(() => new BandContactSensor(this));
            this.distance = new Lazy<BandDistanceSensor>(() => new BandDistanceSensor(this));
            this.gsr = new Lazy<BandGsrSensor>(() => new BandGsrSensor(this));
            this.gyroscope = new Lazy<BandGyroscopeSensor>(() => new BandGyroscopeSensor(this));
            this.heartRate = new Lazy<BandHeartRateSensor>(() => new BandHeartRateSensor(this));
            this.pedometer = new Lazy<BandPedometerSensor>(() => new BandPedometerSensor(this));
            this.rrInterval = new Lazy<BandRRIntervalSensor>(() => new BandRRIntervalSensor(this));
            this.skinTemperature = new Lazy<BandSkinTemperatureSensor>(() => new BandSkinTemperatureSensor(this));
            this.ultravioletLight = new Lazy<BandUltravioletLightSensor>(() => new BandUltravioletLightSensor(this));
        }
#endif

        public BandAccelerometerSensor Accelerometer
        {
            get { return accelerometer.Value; }
        }
        public BandAltimeterSensor Altimeter
        {
            get { return altimeter.Value; }
        }
        public BandAmbientLightSensor AmbientLight
        {
            get { return ambientLight.Value; }
        }
        public BandBarometerSensor Barometer
        {
            get { return barometer.Value; }
        }
        public BandCaloriesSensor Calories
        {
            get { return calories.Value; }
        }
        public BandContactSensor Contact
        {
            get { return contact.Value; }
        }
        public BandDistanceSensor Distance
        {
            get { return distance.Value; }
        }
        public BandGsrSensor Gsr
        {
            get { return gsr.Value; }
        }
        public BandGyroscopeSensor Gyroscope
        {
            get { return gyroscope.Value; }
        }
        public BandHeartRateSensor HeartRate
        {
            get { return heartRate.Value; }
        }
        public BandPedometerSensor Pedometer
        {
            get { return pedometer.Value; }
        }
        public BandRRIntervalSensor RRInterval
        {
            get { return rrInterval.Value; }
        }
        public BandSkinTemperatureSensor SkinTemperature
        {
            get { return skinTemperature.Value; }
        }
        public BandUltravioletLightSensor UltravioletLight
        {
            get { return ultravioletLight.Value; }
        }
    }
}
