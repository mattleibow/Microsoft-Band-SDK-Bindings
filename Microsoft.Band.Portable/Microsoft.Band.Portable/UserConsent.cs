namespace Microsoft.Band.Portable
{
    /// <summary>
    /// Represents the result of a request for user consent.
    /// </summary>
    public enum UserConsent
    {
        /// <summary>
        /// The user declined consent.
        /// </summary>
        Declined,
        /// <summary>
        /// The user granted consent.
        /// </summary>
        Granted,
        /// <summary>
        /// The user has not yet reponded to a request.
        /// </summary>
        Unspecified
    }
}
