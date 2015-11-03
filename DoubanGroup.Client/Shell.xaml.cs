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

namespace DoubanGroup.Client
{
    public sealed partial class Shell : UserControl
    {
        private Frame RootFrame { get; set; }

        private ViewModels.ShellViewModel ViewModel { get; set; }

        public Shell(Frame rootFrame)
        {
            this.RootFrame = rootFrame;

            this.InitializeComponent();

            this.Loaded += Shell_Loaded;
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            this.main_content.Content = this.RootFrame;
            this.ViewModel = (ViewModels.ShellViewModel)this.DataContext;

            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            this.SetConfigButtonPosition();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.ViewModel.IsPaneOpen))
            {
                this.SetConfigButtonPosition();
            }
        }

        private void SetConfigButtonPosition()
        {
            if (sv_container.IsPaneOpen)
            {
                Grid.SetRow(btnConfig, 0);
                Grid.SetColumn(btnConfig, 1);
            }
            else
            {
                Grid.SetRow(btnConfig, 1);
                Grid.SetColumn(btnConfig, 0);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var vm = new ViewModels.LoginPageViewModel();
            vm.Show();
        }
    }
}
