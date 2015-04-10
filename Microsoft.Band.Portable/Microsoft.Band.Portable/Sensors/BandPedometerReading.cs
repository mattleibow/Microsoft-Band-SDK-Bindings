namespace Microsoft.Band.Portable.Sensors
{
    public class BandPedometerReading : IBandSensorReading
    {
        internal BandPedometerReading(long total)
        {
            TotalSteps = total;
        }

        public long TotalSteps { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "TotalSteps={0}",
                TotalSteps);
        }
    }
}