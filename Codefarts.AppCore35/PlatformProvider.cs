namespace Codefarts.AppCore
{
    /// <summary>
    /// Access the current <see cref="IPlatformProvider"/>.
    /// </summary>
    public static class PlatformProvider
    {
        /// <summary>
        /// Initializes static members of the <see cref="PlatformProvider"/> class.
        /// </summary>
        static PlatformProvider()
        {
            // assign the included default provider so current has a value.
            Current = new DefaultPlatformProvider();
        }

        /// <summary>
        /// Gets or sets the current <see cref="IPlatformProvider"/> implementation.
        /// </summary>
        public static IPlatformProvider Current { get; set; }
    }
}
