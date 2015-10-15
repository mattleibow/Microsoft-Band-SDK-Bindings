using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Band.Portable.Sensors;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class SensorViewModel<T> : BaseViewModel
        where T : IBandSensorReading
    {
        private IBandSensorReading reading;
        private string readingString;
        private BandSensorBase<T> sensor;
        private bool isSensorEnabled;
        private int lines;
        private ObservableCollection<BaseViewModel> sensors;

        public SensorViewModel(string type, BandSensorBase<T> sensor, ObservableCollection<BaseViewModel> sensors, int lines)
        {
            this.lines = lines;
            this.sensors = sensors;
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
                }
            }
        }

        public string ReadingString
        {
            get { return readingString; }
            set
            {
                if (readingString != value)
                {
                    readingString = value;
                    OnPropertyChanged("ReadingString");
                }
            }
        }

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
                var userConsenting = Sensor as IUserConsentingBandSensor<T>;
                if (userConsenting == null ||
                    userConsenting.UserConsented == UserConsent.Granted ||
                    await userConsenting.RequestUserConsent())
                {
                    try
                    {
                        await Sensor.StartReadingsAsync();

                        // refresh the list view with an empty result
                        ReadingString = string.Join("\n",Enumerable.Repeat("...", lines));
                        // quick hack to recalculate cell height
                        var idx = sensors.IndexOf(this);
                        sensors[idx] = sensors[idx];
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Problem starting sensor: " + ex);
                        IsSensorEnabled = false;
                    }
                }
                else
                {
                    IsSensorEnabled = false;
                }
            }
            else
            {
                await Sensor.StopReadingsAsync();
                // quick hack to recalculate cell height
                var idx = sensors.IndexOf(this);
                sensors[idx] = sensors[idx];
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
