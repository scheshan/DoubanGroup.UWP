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
    public class TopicDetailPageViewModel : NavigationViewModelBase
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

        private CommentListViewModel _currentComment;

        public CommentListViewModel CurrentComment
        {
            get { return _currentComment; }
            set { this.SetProperty(ref _currentComment, value); }
        }

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

        public override void OnNavigatedTo(NavigatedToEventArgs e)
        {
            var topicID = (long)e.Parameter;
            this.TopicID = topicID;

            base.OnNavigatedTo(e);

            if (e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.New)
            {
                this.LoadTopic();
                this.LoadComments();
            }
        }

        /// <summary>
        /// 加载评论
        /// </summary>
        /// <returns></returns>
        private async Task LoadComments()
        {
            CommentList commentList;

            if (this.ViewAuthor)
            {
                commentList = await this.RunTaskAsync(this.ApiClient.GetOpCommentList(this.TopicID, 0, PageSize));
            }
            else
            {
                commentList = await this.RunTaskAsync(this.ApiClient.GetCommentList(this.TopicID, 0, PageSize));
            }

            if (commentList == null)
            {
                return;
            }

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

            this.CurrentComment = this.CommentList.FirstOrDefault();

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
            var topic = await this.RunTaskAsync(this.ApiClient.GetTopic(this.TopicID));

            if (topic == null)
            {
                this.ShowToast("获取主题失败");
                this.NavigationService.GoBack();
                return;
            }

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

            if (await this.RunTaskAsync(this.ApiClient.LikeTopic(this.TopicID)))
            {
                this.Topic.Liked = true;
                this.OnPropertyChanged(() => this.Liked);
            }
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

            if (await this.RunTaskAsync(this.ApiClient.DislikeTopic(this.TopicID)))
            {
                this.Topic.Liked = false;
                this.OnPropertyChanged(() => this.Liked);
            }
        }

        private DelegateCommand<Comment> _addCommentCommand;

        public DelegateCommand<Comment> AddCommentCommand
        {
            get
            {
                if (_addCommentCommand == null)
                {
                    _addCommentCommand = new DelegateCommand<Comment>(this.AddComment);
                }
                return _addCommentCommand;
            }
        }

        private async void AddComment(Comment parameter)
        {
            if (this.Topic == null || !this.CurrentUser.IsGroupMember(this.Topic.Group.ID))
            {
                await this.Alert("只有小组成员才能发表评论");
                return;
            }

            var vm = new AddCommentPageViewModel(this.Topic, parameter);
            var comment = await vm.Show();

            if (comment != null)
            {
                this.CommentList.LastOrDefault()?.CommentList.Add(comment);
            }
        }

        private DelegateCommand<Comment> _voteCommentCommand;

        public DelegateCommand<Comment> VoteCommentCommand
        {
            get
            {
                if (_voteCommentCommand == null)
                {
                    _voteCommentCommand = new DelegateCommand<Comment>(this.VoteComment);
                }
                return _voteCommentCommand;
            }
        }

        private async void VoteComment(Comment parameter)
        {
            if (!parameter.CanVote)
            {
                return;
            }

            if (!await this.RequireLogin())
            {
                return;
            }

            var result = await this.RunTaskAsync(this.ApiClient.VoteComment(this.TopicID, parameter.ID));

            if (result != null)
            {
                parameter.CanVote = false;

                if (result.Result)
                {
                    parameter.VoteCount = result.VoteCount;
                }
                else
                {
                    this.ShowToast("您已经投过票了");
                }
            }
        }

        private DelegateCommand _chooseCommentPageCommand;

        public DelegateCommand ChooseCommentPageCommand
        {
            get
            {
                if (_chooseCommentPageCommand == null)
                {
                    _chooseCommentPageCommand = new DelegateCommand(ChooseCommentPage);
                }
                return _chooseCommentPageCommand;
            }
        }

        private async void ChooseCommentPage()
        {
            var vm = new SelectCommentPageViewModel(Convert.ToInt32(this.TotalPage), this.CurrentComment.Page);
            var page = await vm.Show();
            this.CurrentComment = this.CommentList[page - 1];
        }        
    }
}
