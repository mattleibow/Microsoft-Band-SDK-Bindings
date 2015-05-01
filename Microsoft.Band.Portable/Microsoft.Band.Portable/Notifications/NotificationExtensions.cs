using System;
using Microsoft.Band.Portable.Notifications;

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
using NativeMessageFlags = Microsoft.Band.Notifications.MessageFlags;
using NativeVibrationType = Microsoft.Band.Notifications.VibrationType;
#endif

#if __ANDROID__
using NativeGuid = Java.Util.UUID;
#elif __IOS__
using NativeGuid = Foundation.NSUuid;
#elif WINDOWS_PHONE_APP
using NativeGuid = System.Guid;
#endif

namespace Microsoft.Band.Portable
{
    internal static class NotificationExtensions
    {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal static NativeGuid ToNative(this Guid guid)
        {
#if __ANDROID__
            return NativeGuid.FromString(guid.ToString("D"));
#elif __IOS__
            return new NativeGuid(guid.ToString("D"));
#elif WINDOWS_PHONE_APP
            return guid;
#endif
        }

        internal static Guid FromNative(this NativeGuid guid)
        {
#if __ANDROID__
            return Guid.Parse(guid.ToString());
#elif __IOS__
            return Guid.Parse(guid.AsString());
#elif WINDOWS_PHONE_APP
            return guid;
#endif
        }

        internal static NativeMessageFlags ToNative(this MessageFlags messageFlags)
        {
            // can't use switch on Android as this is not an enum
            if (messageFlags == MessageFlags.ShowDialog)
                return NativeMessageFlags.ShowDialog;
            return NativeMessageFlags.None;
        }

        internal static NativeVibrationType ToNative(this VibrationType vibrationType)
        {
            // can't use switch on Android as this is not an enum
            if (vibrationType == VibrationType.RampDown)
                return NativeVibrationType.RampDown;
            if (vibrationType == VibrationType.RampUp)
                return NativeVibrationType.RampUp;
            if (vibrationType == VibrationType.NotificationOneTone)
                return NativeVibrationType.NotificationOneTone;
            if (vibrationType == VibrationType.NotificationTwoTone)
                return NativeVibrationType.NotificationTwoTone;
            if (vibrationType == VibrationType.NotificationAlarm)
                return NativeVibrationType.NotificationAlarm;
            if (vibrationType == VibrationType.NotificationTimer)
                return NativeVibrationType.NotificationTimer;
            if (vibrationType == VibrationType.OneToneHigh)
                return NativeVibrationType.OneToneHigh;
            if (vibrationType == VibrationType.ThreeToneHigh)
                return NativeVibrationType.ThreeToneHigh;
            if (vibrationType == VibrationType.TwoToneHigh)
                return NativeVibrationType.TwoToneHigh;

            throw new ArgumentOutOfRangeException("vibrationType", "Invalid VibrationType specified.");
        }
#endif
    }
}
