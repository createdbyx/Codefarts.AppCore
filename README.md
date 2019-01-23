# Codefarts.AppCore
Provides basic application platform code.

# Simple WPF app Example
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            PlatformProvider.Current = new WpfPlatformProvider();

            var ioc = Container.Default;
            ioc.Register<IPlatformProvider>(() => PlatformProvider.Current);
        }
