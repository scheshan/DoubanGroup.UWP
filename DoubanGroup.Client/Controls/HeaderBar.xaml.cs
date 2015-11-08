using MyToolkit.Paging;
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

namespace DoubanGroup.Client.Controls
{
    public sealed partial class HeaderBar : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(HeaderBar), new PropertyMetadata(null));

        private MtFrame RootFrame { get; set; }

        public CommandBar CommandBar
        {
            get { return (CommandBar)GetValue(CommandBarProperty); }
            set { SetValue(CommandBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandBarProperty =
            DependencyProperty.Register("CommandBar", typeof(CommandBar), typeof(HeaderBar), new PropertyMetadata(null, OnCommandBarChanged));

        private static void OnCommandBarChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (HeaderBar)sender;
            control.commandbar_container.Content = e.NewValue;
        }

        public bool EnableSearch
        {
            get { return (bool)GetValue(EnableSearchProperty); }
            set { SetValue(EnableSearchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableSearch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableSearchProperty =
            DependencyProperty.Register("EnableSearch", typeof(bool), typeof(HeaderBar), new PropertyMetadata(false));

        public HeaderBar()
        {
            this.InitializeComponent();

            this.RootFrame = (Window.Current.Content as Shell)?.RootFrame;

            this.Loaded += HeaderBar_Loaded;

            searchbox.QuerySubmitted += Searchbox_QuerySubmitted;
        }

        private void Searchbox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.QueryText))
            {
                this.RootFrame.NavigateAsync(typeof(Views.SearchPage), args.QueryText);
            }
        }

        private void HeaderBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.RootFrame == null)
            {
                return;
            }

            btnBack.Visibility = this.RootFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.RootFrame.GoBackAsync();
        }
    }
}
