namespace Microsoft.Band.Portable.Notifications
{
    /// <summary>
    /// Represents a type of vibration that can be sent to a Band device.
    /// </summary>
    public enum VibrationType
    {
        /// <summary>
        /// The ramp down vibration type.
        /// </summary>
        RampDown,
        /// <summary>
        /// The ramp up vibration type.
        /// </summary>
        RampUp,
        /// <summary>
        /// The notification one-tone vibration type.
        /// </summary>
        NotificationOneTone,
        /// <summary>
        /// The notification two-tone vibration type.
        /// </summary>
        NotificationTwoTone,
        /// <summary>
        /// The notification alarm vibration type.
        /// </summary>
        NotificationAlarm,
        /// <summary>
        /// The notification timer vibration type.
        /// </summary>
        NotificationTimer,
        /// <summary>
        /// The one-tone high vibration type.
        /// </summary>
        OneToneHigh,
        /// <summary>
        /// The three-tone high vibration type.
        /// </summary>
        ThreeToneHigh,
        /// <summary>
        /// The two-tone high vibration type.
        /// </summary>
        TwoToneHigh
    }
}
