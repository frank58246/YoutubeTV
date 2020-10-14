using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YoutubeTV.Controller;
using YoutubeTV.Providers.Implement;
using YoutubeTV.Providers.Interface;
using YoutubeTV.ViewModel.Implement;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // ViewModel
            services.AddSingleton<IChannelViewModel, ChannelViewModel>();
            services.AddSingleton<IVolumnViewModel, VolumnViewModel>();

            // Provider
            services.AddSingleton<IChannelProvider, ChannelProvider>();
            services.AddSingleton<IUserConfigProvider, UserConfigProvider>();

            // Controller
            services.AddSingleton<MainViewController>();

            services.AddSingleton<MainWindow>();
        }
    }
}