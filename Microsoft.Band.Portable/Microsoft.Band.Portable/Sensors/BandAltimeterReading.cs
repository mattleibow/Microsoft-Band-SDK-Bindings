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
            long totalLoss,
            long flightsAscendedToday,
            long totalGainToday)
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
            FlightsAscendedToday = flightsAscendedToday;
            TotalGainToday = totalGainToday;
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
        public long FlightsAscendedToday { get; private set; }
        public long TotalGainToday { get; private set; }

        public override string ToString()
        {
            return $"FlightsAscended={FlightsAscended}, FlightsDescended={FlightsDescended}, Rate={Rate}, SteppingGain={SteppingGain}, SteppingLoss={SteppingLoss}, StepsAscended={StepsAscended}, StepsDescended={StepsDescended}, TotalGain={TotalGain}, TotalLoss={TotalLoss}, FlightsAscendedToday={FlightsAscendedToday}, TotalGainToday={TotalGainToday}";
        }
    }
}