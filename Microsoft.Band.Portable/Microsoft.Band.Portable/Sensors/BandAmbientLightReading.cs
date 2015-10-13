namespace Microsoft.Band.Portable.Sensors
{
    public class BandAmbientLightReading : IBandSensorReading
    {
        internal BandAmbientLightReading(int brightness)
        {
            Brightness = brightness;
        }

        public int Brightness{ get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Brightness={0}",
                Brightness);
        }
    }
}