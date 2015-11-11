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
    public class MyPageViewModel : ViewModelBase
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

        public IncrementalLoadingList<Topic> SuggestTopicList { get; private set; }

        public IncrementalLoadingList<Topic> LikeTopicList { get; private set; }

        public IncrementalLoadingList<Topic> PostTopicList { get; private set; }

        public IncrementalLoadingList<Topic> ReplyTopicList { get; private set; }

        public MyPageViewModel()
        {
            this.SuggestTopicList = new IncrementalLoadingList<Topic>(this.LoadSuggestTopics);
            this.LikeTopicList = new IncrementalLoadingList<Topic>(this.LoadLikeTopics);
            this.PostTopicList = new IncrementalLoadingList<Topic>(this.LoadPostTopics);
            this.ReplyTopicList = new IncrementalLoadingList<Topic>(this.LoadReplyTopics);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            this.JoinedGroupList = this.CurrentUser.JoinedGroupList;
            this.ManagedGroupList = this.CurrentUser.ManagedGroupList;
        }

        private async Task<IEnumerable<Topic>> LoadSuggestTopics(uint count)
        {
            var topicList = await this.RunTaskAsync(this.ApiClient.GetMySuggestTopics(this.SuggestTopicList.Count, 30));

            if (topicList == null || !topicList.HasMore)
            {
                this.SuggestTopicList.NoMore();
            }

            return topicList?.Topics;
        }

        private async Task<IEnumerable<Topic>> LoadLikeTopics(uint count)
        {
            int queryCount = 30;

            var topicList = await this.RunTaskAsync(this.ApiClient.GetMyLikedTopics(this.LikeTopicList.Count, queryCount));

            if (topicList == null || topicList.Items.Count < queryCount)
            {
                this.LikeTopicList.NoMore();
            }

            return topicList?.Items;
        }

        private async Task<IEnumerable<Topic>> LoadPostTopics(uint count)
        {
            int queryCount = 30;

            var topicList = await this.RunTaskAsync(this.ApiClient.GetMyCreatedTopics(this.PostTopicList.Count, queryCount));

            if (topicList == null || topicList.Items.Count < queryCount)
            {
                this.PostTopicList.NoMore();
            }

            return topicList?.Items;
        }

        private async Task<IEnumerable<Topic>> LoadReplyTopics(uint count)
        {
            int queryCount = 30;

            var topicList = await this.RunTaskAsync(this.ApiClient.GetMyRepliedTopics(this.ReplyTopicList.Count, queryCount));

            if (topicList == null || topicList.Items.Count < queryCount)
            {
                this.ReplyTopicList.NoMore();
            }

            return topicList?.Items;
        }
    }
}
