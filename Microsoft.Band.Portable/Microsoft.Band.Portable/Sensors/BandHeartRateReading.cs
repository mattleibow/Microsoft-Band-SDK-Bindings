namespace Microsoft.Band.Portable.Sensors
{
    public class BandHeartRateReading : IBandSensorReading
    {
        internal BandHeartRateReading(HeartRateQuality quality, int rate)
        {
            Quality = quality;
            HeartRate = rate;
        }

        public HeartRateQuality Quality { get; private set; }
        public int HeartRate { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Quality={0}, HeartRate={1}",
                Quality, HeartRate);
        }
    }
}