namespace Microsoft.Band.Portable.Sensors
{
    public class BandAltimeterReading : IBandSensorReading
    {
        internal BandAltimeterReading(
            long flightsAscended,
            long flightsDescended,
            double rate,
            long steppingGain, 
            long steppingLoss,
            long stepsAscended, 
            long stepsDescended,
            long totalGain, 
            long totalLoss)
        {
            FlightsAscended = flightsAscended;
            FlightsDescended = flightsDescended;
            Rate = rate;
            SteppingGain = steppingGain;
            SteppingLoss = steppingLoss;
            StepsAscended = stepsAscended;
            StepsDescended = stepsDescended;
            TotalGain = totalGain;
            TotalLoss = totalLoss;
        }

        public long FlightsAscended { get; private set; }
        public long FlightsDescended { get; private set; }
        public double Rate { get; private set; }
        public long SteppingGain { get; private set; }
        public long SteppingLoss { get; private set; }
        public long StepsAscended { get; private set; }
        public long StepsDescended { get; private set; }
        public long TotalGain { get; private set; }
        public long TotalLoss { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "FlightsAscended={0}, FlightsDescended={1}, Rate={2} SteppingGain={3}, SteppingLoss={4} StepsAscended={5}, StepsDescended={6} TotalGain={7}, TotalLoss={8}",
                FlightsAscended, FlightsDescended, Rate, SteppingGain, SteppingLoss, StepsAscended, StepsDescended, TotalGain, TotalLoss);
        }
    }
}