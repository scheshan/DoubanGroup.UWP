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

        public bool Liked
        {
            get
            {
                return this.Topic != null ? this.Topic.Liked : false;
            }
        }

        private bool _viewAuthor;

        public bool ViewAuthor
        {
            get { return _viewAuthor; }
            set
            {
                if (this.SetProperty(ref _viewAuthor, value))
                {
                    this.CommentList.Clear();
                    this.LoadComments();
                }
            }
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

        /// <summary>
        /// 加载评论
        /// </summary>
        /// <returns></returns>
        private async Task LoadComments()
        {
            this.IsLoading = true;

            CommentList commentList;

            if (this.ViewAuthor)
            {
                commentList = await this.ApiClient.GetOpCommentList(this.TopicID, 0, PageSize);
            }
            else
            {
                commentList = await this.ApiClient.GetCommentList(this.TopicID, 0, PageSize);
            }

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
                    var item = new CommentListViewModel(this.TopicID, i + 1, this.PageSize, this.ViewAuthor);

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

        /// <summary>
        /// 加载主题
        /// </summary>
        /// <returns></returns>
        private async Task LoadTopic()
        {
            this.IsLoading = true;

            var topic = await this.ApiClient.GetTopic(this.TopicID);

            this.Topic = topic;

            this.OnPropertyChanged(() => this.Liked);

            this.IsLoading = false;
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

        private async void LikeTopic()
        {
            if (!await this.RequireLogin())
            {
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            await this.ApiClient.LikeTopic(this.TopicID);
            this.Topic.Liked = true;
            this.OnPropertyChanged(() => this.Liked);

            this.IsLoading = false;
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

        private async void DislikeTopic()
        {
            if (!await this.RequireLogin())
            {
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            await this.ApiClient.DislikeTopic(this.TopicID);
            this.Topic.Liked = false;
            this.OnPropertyChanged(() => this.Liked);

            this.IsLoading = false;
        }

        private DelegateCommand _addCommentCommand;

        public DelegateCommand AddCommentCommand
        {
            get
            {
                if (_addCommentCommand == null)
                {
                    _addCommentCommand = new DelegateCommand(this.AddComment);
                }
                return _addCommentCommand;
            }
        }

        private async void AddComment()
        {
            if (this.Topic == null || !this.CurrentUser.IsGroupMember(this.Topic.Group.ID))
            {
                await this.Alert("只有小组成员才能发表评论");
                return;
            }

            var vm = new AddCommentPageViewModel(this.Topic, null);
            await vm.Show();
        }
    }
}
