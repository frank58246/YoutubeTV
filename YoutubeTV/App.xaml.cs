using CefSharp;
using CefSharp.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using YoutubeTV.Controller;
using YoutubeTV.Misc;
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
            ZipManager.Uncompress(@"CefPack/CEF.zip", Directory.GetCurrentDirectory());

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            InitSetting();
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // ViewModel
            services.AddSingleton<IChannelViewModel, ChannelViewModel>();
            services.AddSingleton<IVolumeViewModel, SystemVolumeViewModel>();

            // Provider
            services.AddSingleton<IChannelProvider, ChannelProvider>();
            services.AddSingleton<IUserConfigProvider, UserConfigProvider>();

            // Controller
            services.AddSingleton<MainViewController>();

            services.AddSingleton<MainWindow>();
        }

        private void InitSetting()
        {
            // auto play video
            var settings = new CefSettings();
            settings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";
            Cef.Initialize(settings, true, browserProcessHandler: null);
        }
    }
}