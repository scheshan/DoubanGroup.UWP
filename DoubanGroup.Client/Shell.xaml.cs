using MyToolkit.Controls;
using MyToolkit.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DoubanGroup.Client
{
    public sealed partial class Shell : UserControl
    {
        public MtFrame RootFrame
        {
            get; private set;
        }

        private DispatcherTimer HideToastTimer { get; set; }

        public Shell(MtFrame rootFrame)
        {
            this.RootFrame = rootFrame;

            this.InitializeComponent();

            this.main_content.Content = this.RootFrame;

            this.Loaded += Shell_Loaded;

            this.HideToastTimer = new DispatcherTimer();
            this.HideToastTimer.Interval = TimeSpan.FromSeconds(3);
            this.HideToastTimer.Tick += HideToastTimer_Tick;
        }

        private void HideToastTimer_Tick(object sender, object e)
        {
            this.HideToast();
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Navigate(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var pageToken = (string)btn.Tag;

            Type pageType = null;

            switch (pageToken)
            {
                case "Home":
                    pageType = typeof(Views.HomePage);
                    break;
                case "My":
                    pageType = typeof(Views.MyPage);
                    break;
            }

            if (pageType != null)
            {
                this.ViewPage(pageType);
            }
        }

        private void ViewPage(Type pageType)
        {
            var page = this.RootFrame.GetNearestPageOfTypeInBackStack(pageType);

            if (page != null)
            {
                this.RootFrame.MoveToTopAndNavigateAsync(page);
            }
            else
            {
                this.RootFrame.Navigate(pageType);
            }
        }

        public void ShowToast(string message)
        {
            toast_content.Text = message;
            var sb = this.Resources["ShowToastStoryboard"] as Storyboard;
            sb.Begin();
            this.HideToastTimer.Start();
        }

        public void HideToast()
        {
            toast_content.Text = string.Empty;
            toast_container.Visibility = Visibility.Collapsed;
            toast_container.Opacity = 0;
            this.HideToastTimer.Stop();
        }
    }
}
