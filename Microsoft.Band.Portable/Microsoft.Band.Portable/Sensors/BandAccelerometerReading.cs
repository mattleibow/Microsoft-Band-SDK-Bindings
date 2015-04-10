namespace Microsoft.Band.Portable.Sensors
{
    public class BandAccelerometerReading : IBandSensorReading
    {
        internal BandAccelerometerReading(double x, double y, double z)
        {
            AccelerationX = x;
            AccelerationY = y;
            AccelerationZ = z;
        }

        public double AccelerationX { get; private set; }
        public double AccelerationY { get; private set; }
        public double AccelerationZ { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "AccelerationX={0}, AccelerationY={1}, AccelerationZ={2}",
                AccelerationX, AccelerationY, AccelerationZ);
        }
    }
}