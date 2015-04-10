namespace Microsoft.Band.Portable.Sensors
{
    public class BandSkinTemperatureReading : IBandSensorReading
    {
        internal BandSkinTemperatureReading(double temp)
        {
            Temperature = temp;
        }

        public double Temperature { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Temperature={0}",
                Temperature);
        }
    }
}