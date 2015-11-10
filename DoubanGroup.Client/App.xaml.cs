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
using DoubanGroup.Core.Api;
using Prism.Mvvm;
using DoubanGroup.Client.Controls;
using DoubanGroup.Client.CacheItem;
using AutoMapper;
using MyToolkit.Paging;
using Prism.Windows.Navigation;
using Prism.Events;
using Windows.UI.Core;
using MyToolkit.Controls;

namespace DoubanGroup.Client
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : MtApplication
    {
        private static IUnityContainer _container = new UnityContainer();

        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        private Shell Shell { get; set; }

        private INavigationService NavigationService { get; set; }

        private MtFrame RootFrame { get; set; }

        public override Type StartPageType
        {
            get
            {
                return typeof(Views.TestPage);
            }
        }

        public override UIElement CreateWindowContentElement()
        {
            this.InitializeFrame();

            this.Shell = new Shell(this.RootFrame);
            return this.Shell;
        }

        public override MtFrame GetFrame(UIElement windowContentElement)
        {
            return this.Shell.RootFrame;
        }

        private void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(type =>
           {
               return Container.Resolve(type);
           });

            ViewModelLocationProvider.Register(typeof(ChannelDetail).FullName, () =>
            {
                return Container.Resolve<ViewModels.ChannelDetailViewModel>();
            });
            ViewModelLocationProvider.Register(typeof(Shell).FullName, () =>
            {
                return Container.Resolve<ViewModels.ShellViewModel>();
            });
        }

        private void ConfigureContainer()
        {
            Container.RegisterInstance(Container);
            Container.RegisterInstance(this.NavigationService);
            Container.RegisterInstance<IEventAggregator>(new EventAggregator());

            var currentUserViewModel = Container.Resolve<ViewModels.CurrentUserViewModel>();

            Container.RegisterInstance<IAccessTokenProvider>(currentUserViewModel);
            Container.RegisterInstance(currentUserViewModel);
        }

        public App()
        {
            this.UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.Write(e.Exception);

            if (e.Exception is Core.Api.ApiException)
            {
                e.Handled = true;
            }
        }

        private void InitializeFrame()
        {
            var frame = new MtFrame();
            frame.DisableForwardStack = false;
            frame.Navigated += Frame_Navigated;
            this.RootFrame = frame;
            this.NavigationService = new MtNavigationService(frame);

            this.ConfigureContainer();
            this.ConfigureViewModelLocator();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            if (!string.IsNullOrWhiteSpace(args.Arguments))
            {
                var arguments = args.Arguments;
                var arr = arguments.Split('-');
                var pageToken = arr[0];
                var parameter = arr[1];

                this.NavigationService.Navigate(pageToken, Convert.ToInt64(parameter));
            }
        }

        private void Frame_Navigated(object sender, MtNavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
