using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Band.Portable.Sensors;

#if __ANDROID__
using System.Collections.Concurrent;
using Android.App;
using Android.Content;
using Android.OS;
using NativeBandSensorManager = Microsoft.Band.Sensors.IBandSensorManager;
using NativeSampleRate = Microsoft.Band.Sensors.SampleRate;
using NativePedometerMotionType = Microsoft.Band.Sensors.MotionType;
using NativeHeartRateQuality = Microsoft.Band.Sensors.HeartRateQuality;
using NativeUltravioletLightLevel = Microsoft.Band.Sensors.UVIndexLevel;
using NativeBandContactState = Microsoft.Band.Sensors.BandContactState;
#elif __IOS__
using NativePedometerMotionType = Microsoft.Band.Sensors.MotionType;
using NativeHeartRateQuality = Microsoft.Band.Sensors.HeartRateQuality;
using NativeUltravioletLightLevel = Microsoft.Band.Sensors.UVIndexLevel;
using NativeBandContactState = Microsoft.Band.Sensors.BandContactStatus;
#elif WINDOWS_PHONE_APP
using NativeSampleRate = System.TimeSpan;
using NativePedometerMotionType = Microsoft.Band.Sensors.MotionType;
using NativeHeartRateQuality = Microsoft.Band.Sensors.HeartRateQuality;
using NativeUltravioletLightLevel = Microsoft.Band.Sensors.UVIndexLevel;
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
        public static MotionType FromNative(this NativePedometerMotionType motion)
        {
            // can't use switch on Android as this is not an enum
            if (motion == NativePedometerMotionType.Running)
                return MotionType.Running;
            if (motion == NativePedometerMotionType.Jogging)
                return MotionType.Jogging;
            if (motion == NativePedometerMotionType.Walking)
                return MotionType.Walking;
            if (motion == NativePedometerMotionType.Idle)
                return MotionType.Idle;
            return MotionType.Unknown;
        }
        public static HeartRateQuality FromNative(this NativeHeartRateQuality motion)
        {
            // can't use switch on Android as this is not an enum
            if (motion == NativeHeartRateQuality.Locked)
                return HeartRateQuality.Locked;
            if (motion == NativeHeartRateQuality.Acquiring)
                return HeartRateQuality.Acquiring;
            return HeartRateQuality.Unknown;
        }
        public static UVIndexLevel FromNative(this NativeUltravioletLightLevel level)
        {
            // can't use switch on Android as this is not an enum
            if (level == NativeUltravioletLightLevel.VeryHigh)
                return UVIndexLevel.VeryHigh;
            if (level == NativeUltravioletLightLevel.High)
                return UVIndexLevel.High;
            if (level == NativeUltravioletLightLevel.Medium)
                return UVIndexLevel.Medium;
            if (level == NativeUltravioletLightLevel.Low)
                return UVIndexLevel.Low;
            if (level == NativeUltravioletLightLevel.None)
                return UVIndexLevel.None;
            return UVIndexLevel.Unknown;
        }
        public static ContactState FromNative(this NativeBandContactState state)
        {
            // can't use switch on Android as this is not an enum
            if (state == NativeBandContactState.NotWorn)
                return ContactState.NotWorn;
            if (state == NativeBandContactState.Worn)
                return ContactState.Worn;
            return ContactState.Unknown;
        }
#endif
    }
}
