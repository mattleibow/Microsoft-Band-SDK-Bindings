namespace Microsoft.Band.Portable.Sensors
{
    public class BandCaloriesReading : IBandSensorReading
    {
        internal BandCaloriesReading(long calories, long caloriesToday)
        {
            Calories = calories;
            CaloriesToday = caloriesToday;
        }
        
        public long Calories { get; private set; }
        public long CaloriesToday { get; private set; }

        public override string ToString()
        {
            return $"Calories={Calories}, CaloriesToday={CaloriesToday}";
        }
    }
}