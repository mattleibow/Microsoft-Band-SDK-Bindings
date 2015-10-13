namespace Microsoft.Band.Portable.Sensors
{
    public class BandBarometerReading : IBandSensorReading
    {
        internal BandBarometerReading(double airPressure, double temperature)
        {
            AirPressure = airPressure;
            Temperature = temperature;
        }

        public double AirPressure { get; private set; }
        public double Temperature { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "AirPressure={0}, Temperature={1}",
                AirPressure, Temperature);
        }
    }
}