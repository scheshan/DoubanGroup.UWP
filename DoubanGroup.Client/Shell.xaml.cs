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

        public Shell(MtFrame rootFrame)
        {
            this.RootFrame = rootFrame;

            this.InitializeComponent();

            this.main_content.Content = this.RootFrame;
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
    }
}
