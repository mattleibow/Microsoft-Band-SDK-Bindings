namespace Microsoft.Band.Portable.Sensors
{
    public class BandUltravioletLightReading : IBandSensorReading
    {
        internal BandUltravioletLightReading(BandUltravioletLightLevel level)
        {
            Level = level;
        }

        public BandUltravioletLightLevel Level { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Level={0}",
                Level);
        }
    }
}