using System;
using System.Linq;
using Microsoft.Band.Portable.Sensors;

#if __ANDROID__
using NativeSampleRate = Microsoft.Band.Sensors.SampleRate;
using NativePedometerMotionType = Microsoft.Band.Sensors.PedometerMode;
using NativeHeartRateQuality = Microsoft.Band.Sensors.HeartRateQuality;
using NativeUltravioletLightLevel = Microsoft.Band.Sensors.UVIndexLevel;
using NativeBandContactState = Microsoft.Band.Sensors.BandContactStatus;
#elif __IOS__
using NativePedometerMotionType = Microsoft.Band.Sensors.PedometerMode;
using NativeHeartRateQuality = Microsoft.Band.Sensors.HeartRateQuality;
using NativeUltravioletLightLevel = Microsoft.Band.Sensors.UVIndexLevel;
using NativeBandContactState = Microsoft.Band.Sensors.BandContactStatus;
#elif WINDOWS_PHONE_APP
using NativeSampleRate = System.TimeSpan;
using NativePedometerMotionType = Microsoft.Band.Sensors.MotionType;
using NativeHeartRateQuality = Microsoft.Band.Sensors.HeartRateQuality;
using NativeUltravioletLightLevel = Microsoft.Band.Sensors.UltravioletExposureLevel;
using NativeBandContactState = Microsoft.Band.Sensors.BandContactState;
#endif

namespace Microsoft.Band.Portable
{
    internal static class SensorExtensions
    {
#if WINDOWS_PHONE_APP
        public static void ApplySampleRate<T>(this Microsoft.Band.Sensors.IBandSensor<T> sensor, BandSensorSampleRate sampleRate)
            where T : Microsoft.Band.Sensors.IBandSensorReading
        {
            var nativeRate = ToNative(sampleRate);
            var intervals = sensor.SupportedReportingIntervals;
            if (intervals.Contains(nativeRate))
                sensor.ReportingInterval = nativeRate;
            else
                sensor.ReportingInterval = intervals.First();
        }
#endif
#if __ANDROID__ || WINDOWS_PHONE_APP
        public static NativeSampleRate ToNative(this BandSensorSampleRate sampleRate)
        {
            switch (sampleRate)
            {
                case BandSensorSampleRate.Ms128:
#if __ANDROID__
                    return NativeSampleRate.Ms128;
#elif WINDOWS_PHONE_APP
                    return NativeSampleRate.FromMilliseconds(128);
#endif
                case BandSensorSampleRate.Ms32:
#if __ANDROID__
                    return NativeSampleRate.Ms32;
#elif WINDOWS_PHONE_APP
                    return NativeSampleRate.FromMilliseconds(32);
#endif
                case BandSensorSampleRate.Ms16:
                default:
#if __ANDROID__
                    return NativeSampleRate.Ms16;
#elif WINDOWS_PHONE_APP
                    return NativeSampleRate.FromMilliseconds(16);
#endif
            }
        }
#endif

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        public static BandDistanceMotionType FromNative(this NativePedometerMotionType motion)
        {
            // can't use switch on Android as this is not an enum
            if (motion == NativePedometerMotionType.Running)
                return BandDistanceMotionType.Running;
            if (motion == NativePedometerMotionType.Jogging)
                return BandDistanceMotionType.Jogging;
            if (motion == NativePedometerMotionType.Walking)
                return BandDistanceMotionType.Walking;
            if (motion == NativePedometerMotionType.Idle)
                return BandDistanceMotionType.Idle;
            return BandDistanceMotionType.Unknown;
        }
        public static BandHeartRateQuality FromNative(this NativeHeartRateQuality motion)
        {
            // can't use switch on Android as this is not an enum
            if (motion == NativeHeartRateQuality.Locked)
                return BandHeartRateQuality.Locked;
#if WINDOWS_PHONE_APP || __IOS__
            if (motion == NativeHeartRateQuality.Acquiring)
#else
            if (motion == NativeHeartRateQuality.Aquiring) // spelling error
#endif
                return BandHeartRateQuality.Acquiring;
            return BandHeartRateQuality.Unknown;
        }
        public static BandUltravioletLightLevel FromNative(this NativeUltravioletLightLevel level)
        {
            // can't use switch on Android as this is not an enum
            if (level == NativeUltravioletLightLevel.VeryHigh)
                return BandUltravioletLightLevel.VeryHigh;
            if (level == NativeUltravioletLightLevel.High)
                return BandUltravioletLightLevel.High;
            if (level == NativeUltravioletLightLevel.Medium)
                return BandUltravioletLightLevel.Medium;
            if (level == NativeUltravioletLightLevel.Low)
                return BandUltravioletLightLevel.Low;
            if (level == NativeUltravioletLightLevel.None)
                return BandUltravioletLightLevel.None;
            return BandUltravioletLightLevel.Unknown;
        }
        public static BandContactState FromNative(this NativeBandContactState state)
        {
            // can't use switch on Android as this is not an enum
            if (state == NativeBandContactState.NotWorn)
                return BandContactState.NotWorn;
            if (state == NativeBandContactState.Worn)
                return BandContactState.Worn;
            return BandContactState.Unknown;
        }
#endif
    }
}
