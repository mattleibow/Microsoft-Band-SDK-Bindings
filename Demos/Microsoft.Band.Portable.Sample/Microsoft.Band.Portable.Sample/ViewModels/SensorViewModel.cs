using System;
using System.Threading.Tasks;

using Microsoft.Band.Portable.Sensors;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class SensorViewModel<T> : BaseViewModel
        where T : IBandSensorReading
    {
        private IBandSensorReading reading;
        private BandSensorBase<T> sensor;
        private bool isSensorEnabled;

        public SensorViewModel(string type, BandSensorBase<T> sensor)
        {
            this.sensor = sensor;
            this.reading = null;
            this.isSensorEnabled = false;

            Type = type;
            Sensor.ReadingChanged += (sender, e) => Reading = e.SensorReading;
        }

        public override async Task Prepare()
        {
            await base.Prepare();

            // restore the sensor state
            await ApplySensorState();
        }

        public override async Task CleanUp()
        {
            await base.CleanUp();

            // stop the actual sensor, but keep the state
            await Sensor.StopReadingsAsync();
        }

        public BandSensorBase<T> Sensor { get { return sensor; } }

        public string Type { get; private set; }

        public IBandSensorReading Reading
        {
            get { return reading; }
            set
            {
                if (reading != value)
                {
                    reading = value;
                    ReadingString = GetStringValue(value);

                    OnPropertyChanged("Reading");
                    OnPropertyChanged("ReadingString");
                }
            }
        }

        public string ReadingString { get; private set; }

        public bool IsSensorEnabled
        {
            get { return isSensorEnabled; }
            set
            {
                if (isSensorEnabled != value)
                {
                    isSensorEnabled = value;
                    OnPropertyChanged("IsSensorEnabled");

                    ApplySensorState();
                }
            }
        }

        private async Task ApplySensorState()
        {
            if (isSensorEnabled)
            {
                // specify the minimum available
                await Sensor.StartReadingsAsync(BandSensorSampleRate.Ms16);
            }
            else
            {
                await Sensor.StopReadingsAsync();
            }
        }

        private static string GetStringValue(IBandSensorReading value)
        {
            return value
                .ToString()
                .Replace(", ", Environment.NewLine)
                .Replace("=", " = ");
        }
    }
}
