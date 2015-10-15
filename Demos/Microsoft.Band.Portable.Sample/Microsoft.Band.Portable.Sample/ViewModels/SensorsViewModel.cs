using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Microsoft.Band.Portable.Sensors;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class SensorsViewModel : BaseClientViewModel
    {
        private BandSensorManager sensorManager;

        public SensorsViewModel(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            sensorManager = bandClient.SensorManager;
            Sensors = new ObservableCollection<BaseViewModel>();

            Init();
        }

        public void Init()
        {
            Sensors.Add(CreateSensorItem("Accelerometer", sensorManager.Accelerometer, 3));
            Sensors.Add(CreateSensorItem("Altimeter", sensorManager.Altimeter, 9));
            Sensors.Add(CreateSensorItem("Ambient Light", sensorManager.AmbientLight, 1));
            Sensors.Add(CreateSensorItem("Barometer", sensorManager.Barometer, 2));
            Sensors.Add(CreateSensorItem("Calories", sensorManager.Calories, 1));
            Sensors.Add(CreateSensorItem("Contact", sensorManager.Contact, 1));
            Sensors.Add(CreateSensorItem("Distance", sensorManager.Distance, 4));
            Sensors.Add(CreateSensorItem("Gyroscope", sensorManager.Gyroscope, 3));
            Sensors.Add(CreateSensorItem("Gsr", sensorManager.Gsr, 1));
            Sensors.Add(CreateSensorItem("Heart Rate", sensorManager.HeartRate, 2));
            Sensors.Add(CreateSensorItem("Pedometer", sensorManager.Pedometer, 1));
            Sensors.Add(CreateSensorItem("RR Interval", sensorManager.RRInterval, 1));
            Sensors.Add(CreateSensorItem("Skin Temperature", sensorManager.SkinTemperature, 1));
            Sensors.Add(CreateSensorItem("Ultraviolet Light", sensorManager.UltravioletLight, 1));
        }

        public override async Task CleanUp()
        {
            await base.CleanUp();

            foreach (var sensor in Sensors)
            {
                await sensor.CleanUp();
            }
        }

        public ObservableCollection<BaseViewModel> Sensors { get; private set; }

        private SensorViewModel<T> CreateSensorItem<T>(string type, BandSensorBase<T> sensor, int lines)
            where T : IBandSensorReading
        {
            return new SensorViewModel<T>(type, sensor, Sensors, lines);
        }
    }
}
