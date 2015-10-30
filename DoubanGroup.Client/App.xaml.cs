using Prism.Unity.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DoubanGroup.Core.Api.Entity;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DoubanGroup.Client
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate("Home", null);

            return Task.FromResult<object>(null);
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = new Shell(rootFrame);
            return shell;
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            Prism.Mvvm.ViewModelLocationProvider.Register(typeof(Controls.ChannelDetail).FullName, () =>
            {
                return this.Container.Resolve<ViewModels.ChannelDetailViewModel>();
            });
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            base.OnRegisterKnownTypesForSerialization();
        }

        public App()
        {
            this.UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.Write(e.Exception);
        }
    }
}
