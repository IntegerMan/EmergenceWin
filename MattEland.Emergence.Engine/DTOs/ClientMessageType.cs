namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// An enum containing various types of messages that need to be displayed on the client
    /// </summary>
    public enum ClientMessageType
    {
        /// <summary>
        /// Indicates a generic message without disambiguating characteristics.
        /// </summary>
        Generic = 0,
        /// <summary>
        /// Indicates that the player tried to do something but failed.
        /// </summary>
        Failure = 1,
        /// <summary>
        /// Indicates that the player tried to do something and succeeded
        /// </summary>
        Success = 2,
        /// <summary>
        /// Indicates a logging message around verbose mathematics, typically around combat
        /// </summary>
        Math = 3,
        /// <summary>
        /// Indicates a system assertion. If this appears, something bad has happened.
        /// This should be handled differently on prod builds.
        /// </summary>
        Assertion = 4,
        Help = 5
    }
}