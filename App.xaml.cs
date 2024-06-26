﻿using Microsoft.Extensions.DependencyInjection;
using SocketTool.ViewModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;

namespace SocketTool
{
    public sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(typeof(MainViewModel),new MainViewModel());
            return services.BuildServiceProvider();
        }
    }

}
