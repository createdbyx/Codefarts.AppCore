// <copyright file="PlatformData.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

using System.Collections.Generic;

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
        /// <param name="commandLineArguments">The command line (if any) that was used to start the process.</param>
        public PlatformData(string operatingSystem, int processorCount, IEnumerable<string> commandLineArguments, string applicationPath)
        {
            this.OperatingSystem = operatingSystem;
            this.ProcessorCount = processorCount;
            this.CommandLineArguments = commandLineArguments;
            this.ApplicationPath = applicationPath;
        }

        /// <summary>
        /// Gets the operating system name.
        /// </summary>
        /// <remarks>
        /// This string could also contain version and service pack info.
        /// </remarks>
        public string OperatingSystem
        {
            get; private set;
        }

        /// <summary>
        /// Gets the number of processors on the platform.
        /// </summary>
        public int ProcessorCount
        {
            get; private set;
        }

        /// <summary>
        /// Gets the command line (if any) that was used to start the process.
        /// </summary>
        public IEnumerable<string> CommandLineArguments
        {
            get; private set;
        }

        public string ApplicationPath
        {
            get; private set;
        }
    }
}