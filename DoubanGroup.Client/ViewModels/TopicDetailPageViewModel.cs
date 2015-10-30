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
    public class TopicDetailPageViewModel : ViewModelBase
    {
        private Topic _topic;

        public Topic Topic
        {
            get { return _topic; }
            set { this.SetProperty(ref _topic, value); }
        }

        private long TopicID { get; set; }

        public ObservableCollection<Comment> PopularCommentList { get; private set; }

        public IncrementalLoadingList<Comment> CommentList { get; private set; }

        public TopicDetailPageViewModel()
        {
            this.PopularCommentList = new ObservableCollection<Comment>();
            this.CommentList = new IncrementalLoadingList<Comment>(this.LoadComments);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            var topicID = (long)e.Parameter;
            this.TopicID = topicID;

            base.OnNavigatedTo(e, viewModelState);

            this.LoadTopic();
        }

        private async Task<IEnumerable<Comment>> LoadComments(uint count)
        {
            this.IsLoading = true;

            var commentList = await this.ApiClient.GetCommentList(this.TopicID, this.CommentList.Count, 30);

            this.IsLoading = false;

            if (this.PopularCommentList.Count == 0)
            {
                foreach (var comment in commentList.PopularComments)
                {
                    this.PopularCommentList.Add(comment);
                }
            }

            return commentList.Comments;
        }
        
        private async Task LoadTopic()
        {
            var topic = await this.ApiClient.GetTopic(this.TopicID);

            this.Topic = topic;
        }
    }
}
