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
            Current = new DefaultPlatformProvider();
        }

        /// <summary>
        /// Gets or sets the current <see cref="IPlatformProvider"/>.
        /// </summary>
        public static IPlatformProvider Current { get; set; }
    }
}
