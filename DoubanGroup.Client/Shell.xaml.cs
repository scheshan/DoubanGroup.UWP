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

        public Shell(Frame rootFrame)
        {
            this.RootFrame = rootFrame;

            this.InitializeComponent();

            this.main_content.Content = this.RootFrame;

            sv_container.PaneClosed += Sv_container_PaneClosed;

            this.SetConfigButtonPosition();
        }

        private void Sv_container_PaneClosed(SplitView sender, object args)
        {
            this.SetConfigButtonPosition();
        }

        private void btnTogglePan_Click(object sender, RoutedEventArgs e)
        {
            sv_container.IsPaneOpen = !sv_container.IsPaneOpen;

            if (sv_container.IsPaneOpen)
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
    }
}
