namespace Microsoft.Band.Portable.Sensors
{
    public class BandGsrReading : IBandSensorReading
    {
        internal BandGsrReading(long resistance)
        {
            Resistance = resistance;
        }

        public long Resistance { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Resistance={0}",
                Resistance);
        }
    }
}