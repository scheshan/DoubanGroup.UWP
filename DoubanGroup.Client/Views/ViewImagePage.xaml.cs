using DoubanGroup.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace DoubanGroup.Client.Views
{
    public sealed partial class ViewImagePage : UserControl
    {
        private Shell Shell { get; set; }

        private Popup Popup { get; set; }

        private List<ImageItem> ImageList { get; set; }

        public ViewImagePage(List<ImageItem> imageList)
        {
            this.ImageList = imageList;
            this.InitializeComponent();
            this.Loaded += ViewImagePage_Loaded;
            this.Unloaded += ViewImagePage_Unloaded;
            this.Popup = new Popup();
            this.Popup.Child = this;
            container.ItemsSource = imageList;
        }

        private void ViewImagePage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Shell.SizeChanged -= Shell_SizeChanged;
        }

        private void ViewImagePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Shell = Window.Current.Content as Shell;

            this.Width = this.Shell.ActualWidth;
            this.Height = this.Shell.ActualHeight;

            this.Shell.SizeChanged += Shell_SizeChanged;
        }

        private void Shell_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = this.Shell.ActualWidth;
            this.Height = this.Shell.ActualHeight;
        }

        public void Show()
        {
            this.Popup.IsOpen = true;
        }

        public void Hide()
        {
            this.Popup.IsOpen = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void cmd_ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            this.Zoom(-1);
        }

        private void cmd_ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            this.Zoom(1);
        }

        private void Zoom(float factor)
        {
            var flipViewItem = (FlipViewItem)container.ContainerFromIndex(container.SelectedIndex);
            var scrollViewer = (ScrollViewer)flipViewItem.ContentTemplateRoot;

            factor = scrollViewer.ZoomFactor + factor;

            if (factor < scrollViewer.MinZoomFactor)
            {
                factor = scrollViewer.MinZoomFactor;
            }
            else if (factor > scrollViewer.MaxZoomFactor)
            {
                factor = scrollViewer.MaxZoomFactor;
            }

            scrollViewer.ChangeView(null, null, factor);
        }
    }
}
