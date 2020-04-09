// <copyright file="PlatformData.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

namespace Codefarts.AppCore
{
    /// <summary>
    /// Contains properties that provide platform and environment data.
    /// </summary>
    public class PlatformData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformData"/> class.
        /// </summary>
        /// <param name="operatingSystem">The operating system name.</param>
        /// <param name="processorCount">The processor count.</param>
        /// <param name="commandLine">The command line (if any) that was used to start the process.</param>
        public PlatformData(string operatingSystem, int processorCount, string commandLine)
        {
            this.OperatingSystem = operatingSystem;
            this.ProcessorCount = processorCount;
            this.CommandLine = commandLine;
        }

        /// <summary>
        /// Gets the operating system name.
        /// </summary>
        /// <remarks>
        /// This string could also contain version and service pack info.
        /// </remarks>
        public string OperatingSystem { get; private set; }

        /// <summary>
        /// Gets the number of processors on the platform.
        /// </summary>
        public int ProcessorCount { get; private set; }

        /// <summary>
        /// Gets the command line (if any) that was used to start the process.
        /// </summary>
        public string CommandLine { get; private set; }
    }
}