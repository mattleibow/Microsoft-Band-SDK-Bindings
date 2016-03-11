namespace Microsoft.Band.Portable.Sensors
{
    public class BandUltravioletLightReading : IBandSensorReading
    {
        internal BandUltravioletLightReading(UVIndexLevel level, long exposureToday)
        {
            Level = level;
            ExposureToday = exposureToday;
        }

        public UVIndexLevel Level { get; private set; }
        public long ExposureToday { get; private set; }

        public override string ToString()
        {
            return $"Level={Level}, ExposureToday={ExposureToday}";
        }
    }
}