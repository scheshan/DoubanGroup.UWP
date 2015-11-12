using DoubanGroup.Client.Models;
using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;

namespace DoubanGroup.Client.ViewModels
{
    public class MyPageViewModel : NavigationViewModelBase
    {
        public ObservableCollection<Group> JoinedGroupList
        {
            get;
            private set;
        }

        public ObservableCollection<Group> ManagedGroupList
        {
            get;
            private set;
        }

        public RefreshableViewModel<Topic> SuggestTopicViewModel { get; private set; }

        public RefreshableViewModel<Topic> LikeTopicViewModel { get; private set; }

        public RefreshableViewModel<Topic> PostTopicViewModel { get; private set; }

        public RefreshableViewModel<Topic> ReplyTopicViewModel { get; private set; }

        public MyPageViewModel()
        {
            this.SuggestTopicViewModel = new RefreshableViewModel<Topic>(this.LoadSuggestTopics, 30);
            this.LikeTopicViewModel = new RefreshableViewModel<Topic>(this.LoadLikeTopics, 30);
            this.PostTopicViewModel = new RefreshableViewModel<Topic>(this.LoadPostTopics, 30);
            this.ReplyTopicViewModel = new RefreshableViewModel<Topic>(this.LoadReplyTopics, 30);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.JoinedGroupList = this.CurrentUser.JoinedGroupList;
            this.ManagedGroupList = this.CurrentUser.ManagedGroupList;
        }

        private async Task<IEnumerable<Topic>> LoadSuggestTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetMySuggestTopics(start, count);

            return topicList.Topics;
        }

        private async Task<IEnumerable<Topic>> LoadLikeTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetMyLikedTopics(start, count);

            return topicList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadPostTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetMyCreatedTopics(start, count);

            return topicList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadReplyTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetMyRepliedTopics(start, count);

            return topicList.Items;
        }
    }
}
