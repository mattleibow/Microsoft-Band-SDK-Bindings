namespace Microsoft.Band.Portable.Sensors
{
    public class BandContactReading : IBandSensorReading
    {
        internal BandContactReading(BandContactState state)
        {
            State = state;
        }

        public BandContactState State { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "State={0}",
                State);
        }
    }
}