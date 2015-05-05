namespace Microsoft.Band.Portable.Notifications
{
    /// <summary>
    /// Represents flags that control how a message is sent to a Band device.
    /// </summary>
    public enum MessageFlags
    {
        /// <summary>
        /// Use the default message style.
        /// </summary>
        None,
        /// <summary>
        /// Show dialog when sending a message.
        /// </summary>
        ShowDialog
    }
}
