namespace Microsoft.Band.Portable.Sensors
{
    public class BandDistanceReading : IBandSensorReading
    {
        internal BandDistanceReading(MotionType motion, double pace, double speed, long total, long distanceToday)
        {
            CurrentMotion = motion;
            Pace = pace;
            Speed = speed;
            TotalDistance = total;
            DistanceToday = distanceToday;
        }

        public MotionType CurrentMotion { get; private set; }
        public double Pace { get; private set; }
        public double Speed { get; private set; }
        public long TotalDistance { get; private set; }
        public long DistanceToday { get; private set; }

        public override string ToString()
        {
            return $"CurrentMotion={CurrentMotion}, Pace={Pace}, Speed={Speed}, TotalDistance={TotalDistance}, DistanceToday={DistanceToday}";
        }
    }
}