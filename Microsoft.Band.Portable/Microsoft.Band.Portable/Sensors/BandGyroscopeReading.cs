namespace Microsoft.Band.Portable.Sensors
{
    public class BandGyroscopeReading : IBandSensorReading
    {
        internal BandGyroscopeReading(double x, double y, double z)
        {
            AngularVelocityX = x;
            AngularVelocityY = y;
            AngularVelocityZ = z;
        }

        public double AngularVelocityX { get; private set; }
        public double AngularVelocityY { get; private set; }
        public double AngularVelocityZ { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "AngularVelocityX={0}, AngularVelocityY={1}, AngularVelocityZ={2}",
                AngularVelocityX, AngularVelocityY, AngularVelocityZ);
        }
    }
}