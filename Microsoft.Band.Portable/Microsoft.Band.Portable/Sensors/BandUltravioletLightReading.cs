namespace Microsoft.Band.Portable.Sensors
{
    public class BandUltravioletLightReading : IBandSensorReading
    {
        internal BandUltravioletLightReading(UVIndexLevel level)
        {
            Level = level;
        }

        public UVIndexLevel Level { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Level={0}",
                Level);
        }
    }
}