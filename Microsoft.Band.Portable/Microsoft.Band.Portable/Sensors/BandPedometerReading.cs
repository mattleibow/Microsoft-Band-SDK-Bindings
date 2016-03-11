namespace Microsoft.Band.Portable.Sensors
{
    public class BandPedometerReading : IBandSensorReading
    {
        internal BandPedometerReading(long total, long stepsToday)
        {
            TotalSteps = total;
            StepsToday = stepsToday;
        }

        public long TotalSteps { get; private set; }
        public long StepsToday { get; private set; }

        public override string ToString()
        {
            return $"TotalSteps={TotalSteps}, StepsToday={StepsToday}";
        }
    }
}