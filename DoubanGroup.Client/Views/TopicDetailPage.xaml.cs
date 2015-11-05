using DoubanGroup.Client.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DoubanGroup.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopicDetailPage : MtPage
    {
        public TopicDetailPageViewModel ViewModel { get; private set; }

        public TopicDetailPage()
        {
            this.InitializeComponent();

            this.ViewModel = (TopicDetailPageViewModel)this.DataContext;
            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.ViewModel.IsLoading))
            {
                this.BottomAppBar.IsEnabled = !this.ViewModel.IsLoading;
            }
            else if (e.PropertyName == nameof(this.ViewModel.Liked))
            {
                if (this.ViewModel.Liked)
                {
                    cmd_Like.Visibility = Visibility.Collapsed;
                    cmd_Dislike.Visibility = Visibility.Visible;
                }
                else
                {
                    cmd_Like.Visibility = Visibility.Visible;
                    cmd_Dislike.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void fvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.fvMain.Focus(FocusState.Pointer);
            var vm = this.fvMain.SelectedItem as ViewModels.CommentListViewModel;
            vm?.LoadData();
        }

        private void cmd_Like_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LikeTopicCommand.Execute();
        }

        private void cmd_Dislike_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DislikeTopicCommand.Execute();
        }

        private void cmd_Comment_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AddCommentCommand.Execute();
        }

        private void cmd_ViewAuthor_Checked(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ViewAuthor = true;
        }

        private void cmd_ViewAuthor_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ViewAuthor = false;
        }
    }
}
