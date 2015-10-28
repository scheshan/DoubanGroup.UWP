using DoubanGroup.Core.Api.Entity;
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
using Microsoft.Practices.Unity;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DoubanGroup.Client.Controls
{
    public sealed partial class ChannelDetail : UserControl
    {
        private bool _isFirstLoaded = true;

        public Channel Channel
        {
            get { return (Channel)GetValue(ChannelProperty); }
            set { SetValue(ChannelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Channel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChannelProperty =
            DependencyProperty.Register("Channel", typeof(Channel), typeof(ChannelDetail), new PropertyMetadata(null));

        public ChannelDetail()
        {
            this.InitializeComponent();

            this.Loaded += ChannelDetail_Loaded;
        }

        private void ChannelDetail_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isFirstLoaded)
            {
                _isFirstLoaded = false;

                var channel = this.Channel;
                var vm = App.Current.Container.Resolve<ViewModels.ChannelDetailViewModel>();
                this.DataContext = vm;
                vm.Init(channel);
            }
        }
    }
}
