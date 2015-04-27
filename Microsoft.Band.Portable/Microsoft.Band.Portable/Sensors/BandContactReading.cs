namespace Microsoft.Band.Portable.Sensors
{
    public class BandContactReading : IBandSensorReading
    {
        internal BandContactReading(ContactState state)
        {
            State = state;
        }

        public ContactState State { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "State={0}",
                State);
        }
    }
}