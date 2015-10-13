namespace Microsoft.Band.Portable.Sensors
{
    public class BandRRIntervalReading : IBandSensorReading
    {
        internal BandRRIntervalReading(double interval)
        {
            Interval = interval;
        }

        public double Interval { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Interval={0}",
                Interval);
        }
    }
}