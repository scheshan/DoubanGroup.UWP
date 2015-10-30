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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DoubanGroup.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopicDetailPage : Page
    {
        public TopicDetailPage()
        {
            this.InitializeComponent();
            this.Loaded += TopicDetailPage_Loaded;
        }

        private void TopicDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            fvMain.Focus(FocusState.Pointer);
        }

        private void fvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.fvMain.SelectedItem as ViewModels.CommentListViewModel;
            vm?.LoadData();
        }
    }
}
