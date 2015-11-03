using DoubanGroup.Client.Models;
using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using Prism.Commands;

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

        private int PageSize { get; set; } = 30;

        private long _totalPage;

        public long TotalPage
        {
            get { return _totalPage; }
            set { this.SetProperty(ref _totalPage, value); }
        }

        private long TopicID { get; set; }

        private bool _liked;

        public bool Liked
        {
            get { return _liked; }
            set { this.SetProperty(ref _liked, value); }
        }

        public ObservableCollection<Comment> PopularCommentList { get; private set; }

        public ObservableCollection<CommentListViewModel> CommentList { get; private set; }

        public bool HasPopularComments
        {
            get
            {
                return this.PopularCommentList.Count > 0;
            }
        }

        public TopicDetailPageViewModel()
        {
            this.PopularCommentList = new ObservableCollection<Comment>();
            this.CommentList = new ObservableCollection<CommentListViewModel>();
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            var topicID = (long)e.Parameter;
            this.TopicID = topicID;

            base.OnNavigatedTo(e, viewModelState);

            this.LoadTopic();
            this.LoadComments();
        }

        private async Task LoadComments()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var commentList = await this.ApiClient.GetCommentList(this.TopicID, 0, PageSize);

            this.IsLoading = false;

            var totalPage = commentList.Total / PageSize;
            if (commentList.Total % PageSize > 0)
            {
                totalPage++;
            }

            if (totalPage == 0)
            {
                totalPage = 1;
            }

            this.TotalPage = totalPage;

            if (this.TotalPage > this.CommentList.Count)
            {
                for (var i = this.CommentList.Count; i < this.TotalPage; i++)
                {
                    var item = new CommentListViewModel(this.TopicID, i + 1, this.PageSize);

                    if (i == 0)
                    {
                        item.Topic = this;
                    }

                    this.CommentList.Add(item);
                }
            }

            if (this.PopularCommentList.Count == 0)
            {
                foreach (var comment in commentList.PopularComments)
                {
                    this.PopularCommentList.Add(comment);
                }
            }

            this.OnPropertyChanged(() => this.HasPopularComments);
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

            this.Liked = this.Topic.Liked;
        }

        private DelegateCommand _likeTopicCommand;

        public DelegateCommand LikeTopicCommand
        {
            get
            {
                if (_likeTopicCommand == null)
                {
                    _likeTopicCommand = new DelegateCommand(LikeTopic);
                }
                return _likeTopicCommand;
            }
        }

        private void LikeTopic()
        {
        }

        private DelegateCommand _dislikeTopicCommand;

        public DelegateCommand DislikeTopicCommand
        {
            get
            {
                if (_dislikeTopicCommand == null)
                {
                    _dislikeTopicCommand = new DelegateCommand(DislikeTopic);
                }
                return _dislikeTopicCommand;
            }
        }

        private void DislikeTopic()
        {
        }
    }
}
