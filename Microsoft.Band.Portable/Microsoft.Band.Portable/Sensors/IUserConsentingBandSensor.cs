using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Sensors
{
    public interface IUserConsentingBandSensor<T> : IBandSensor<T>
        where T : IBandSensorReading
    {
        UserConsent UserConsented { get; }

        Task<bool> RequestUserConsent();
    }
}
