namespace Microsoft.Band.Portable.Sensors
{
    public class BandDistanceReading : IBandSensorReading
    {
        internal BandDistanceReading(BandDistanceMotionType motion, double pace, double speed, long total)
        {
            CurrentMotion = motion;
            Pace = pace;
            Speed = speed;
            TotalDistance = total;
        }

        public BandDistanceMotionType CurrentMotion { get; private set; }
        public double Pace { get; private set; }
        public double Speed { get; private set; }
        public long TotalDistance { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "CurrentMotion={0}, Pace={1}, Speed={2}, TotalDistance={3}",
                CurrentMotion, Pace, Speed, TotalDistance);
        }
    }
}