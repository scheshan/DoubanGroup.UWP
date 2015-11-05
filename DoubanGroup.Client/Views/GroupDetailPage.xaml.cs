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
using DoubanGroup.Client.Extensions;
using MyToolkit.Paging;
using DoubanGroup.Client.ViewModels;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace DoubanGroup.Client.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GroupDetailPage : MtPage
    {
        public GroupDetailPageViewModel ViewModel { get; private set; }

        public GroupDetailPage()
        {
            this.InitializeComponent();

            this.ViewModel = (GroupDetailPageViewModel)this.DataContext;
            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.ViewModel.IsLoading))
            {
                //this.BottomAppBar.IsEnabled = !this.ViewModel.IsLoading;
            }
            else if (e.PropertyName == nameof(this.ViewModel.IsGroupMember))
            {
                if (this.ViewModel.IsGroupMember)
                {
                    //cmd_QuitGroup.Visibility = Visibility.Visible;
                    //cmd_JoinGroup.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //cmd_QuitGroup.Visibility = Visibility.Collapsed;
                    //cmd_JoinGroup.Visibility = Visibility.Visible;
                }
            }
        }

        private void cmd_ViewMember_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ViewMembersCommand.Execute();
        }

        private void cmd_JoinGroup_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.JoinGroupCommand.Execute();
        }

        private void cmd_QuitGroup_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.QuitGroupCommand.Execute();
        }
    }
}
