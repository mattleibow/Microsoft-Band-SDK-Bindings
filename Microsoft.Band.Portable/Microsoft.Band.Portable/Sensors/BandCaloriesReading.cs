namespace Microsoft.Band.Portable.Sensors
{
    public class BandCaloriesReading : IBandSensorReading
    {
        internal BandCaloriesReading(long calories)
        {
            Calories = calories;
        }
        
        public long Calories { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Calories={0}",
                Calories);
        }
    }
}