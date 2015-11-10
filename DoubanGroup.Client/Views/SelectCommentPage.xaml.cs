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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DoubanGroup.Client.Views
{
    public sealed partial class SelectCommentPage : ContentDialog
    {
        public SelectCommentPage()
        {
            this.InitializeComponent();
            gvPageList.Loaded += GvPageList_Loaded;
        }

        private void GvPageList_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ViewModels.SelectCommentPageViewModel;
            this.gvPageList.SelectedItem = vm.CurrentPage;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
