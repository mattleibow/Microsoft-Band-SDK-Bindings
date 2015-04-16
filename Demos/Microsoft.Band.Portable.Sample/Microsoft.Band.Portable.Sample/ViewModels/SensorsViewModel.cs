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
            Sensors.Add(CreateSensorItem("Accelerometer", sensorManager.Accelerometer));
            Sensors.Add(CreateSensorItem("Contact", sensorManager.Contact));
            Sensors.Add(CreateSensorItem("Distance", sensorManager.Distance));
            Sensors.Add(CreateSensorItem("Gyroscope", sensorManager.Gyroscope));
            Sensors.Add(CreateSensorItem("Heart Rate", sensorManager.HeartRate));
            Sensors.Add(CreateSensorItem("Pedometer", sensorManager.Pedometer));
            Sensors.Add(CreateSensorItem("Skin Temperature", sensorManager.SkinTemperature));
            Sensors.Add(CreateSensorItem("Ultraviolet Light", sensorManager.UltravioletLight));
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

        private SensorViewModel<T> CreateSensorItem<T>(string type, BandSensorBase<T> sensor)
            where T : IBandSensorReading
        {
            return new SensorViewModel<T>(type, sensor);
        }
    }
}
