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
    public class UserDetailPageViewModel : ViewModelBase
    {
        private long _userID;

        public long UserID
        {
            get { return _userID; }
            set { this.SetProperty(ref _userID, value); }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { this.SetProperty(ref _user, value); }
        }

        public ObservableCollection<Group> TopGroupList { get; private set; }

        public ObservableCollection<Photo> TopPhotoList { get; private set; }

        public Models.IncrementalLoadingList<Group> JoinedGroupList { get; private set; }

        public Models.IncrementalLoadingList<Topic> RecommandTopicList { get; private set; }

        public Models.IncrementalLoadingList<Topic> LikeTopicList { get; private set; }

        public UserDetailPageViewModel()
        {
            this.TopGroupList = new ObservableCollection<Group>();
            this.TopPhotoList = new ObservableCollection<Photo>();

            this.JoinedGroupList = new Models.IncrementalLoadingList<Group>(this.LoadJoinedGroups);
            this.RecommandTopicList = new Models.IncrementalLoadingList<Topic>(this.LoadRecommandTopicList);
            this.LikeTopicList = new Models.IncrementalLoadingList<Topic>(this.LoadLikeTopicList);
        }

        private async Task InitData()
        {
            var userDetail = await this.RunTaskAsync(this.ApiClient.GetUserDetail(this.UserID, 20));
            var photoList = await this.RunTaskAsync(this.ApiClient.GetUserCreatedPhotos(this.UserID, 0, 20));

            if (userDetail == null)
            {
                this.ShowToast("获取用户失败");
                this.NavigationService.GoBack();
                return;
            }

            this.User = userDetail.User;

            if (userDetail.JoinedGroups?.Count > 0)
            {
                foreach (var group in userDetail.JoinedGroups.Items)
                {
                    this.TopGroupList.Add(group);
                    this.JoinedGroupList.Add(group);
                }
            }

            foreach (var photo in photoList?.Photos)
            {
                this.TopPhotoList.Add(photo);
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            this.UserID = (long)e.Parameter;

            if (e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.New)
            {
                this.InitData();
            }
        }

        private async Task<IEnumerable<Group>> LoadJoinedGroups(uint count)
        {
            int queryCount = 100;

            var groupList = await this.RunTaskAsync(this.ApiClient.GetUserJoinedGroups(this.UserID, this.JoinedGroupList.Count, queryCount));

            if (groupList == null || groupList.Items.Count < queryCount)
            {
                this.JoinedGroupList.NoMore();
            }

            return groupList?.Items;
        }

        private async Task<IEnumerable<Topic>> LoadRecommandTopicList(uint count)
        {
            int queryCount = 100;

            var topicList = await this.RunTaskAsync(this.ApiClient.GetUserRecommandTopics(this.UserID, this.RecommandTopicList.Count, queryCount));

            if (topicList == null || topicList.Items.Count < queryCount)
            {
                this.RecommandTopicList.NoMore();
            }

            return topicList?.Items;
        }

        private async Task<IEnumerable<Topic>> LoadLikeTopicList(uint count)
        {
            int queryCount = 100;

            var topicList = await this.RunTaskAsync(this.ApiClient.GetUserLikeTopics(this.UserID, this.LikeTopicList.Count, queryCount));            

            if (topicList == null || topicList.Items.Count < queryCount)
            {
                this.LikeTopicList.NoMore();
            }

            return topicList?.Items;
        }

        private DelegateCommand _viewImageCommand;

        public DelegateCommand ViewImageCommand
        {
            get
            {
                if (_viewImageCommand == null)
                {
                    _viewImageCommand = new DelegateCommand(ViewImage);
                }
                return _viewImageCommand;
            }
        }

        private void ViewImage()
        {
            var imageList = this.TopPhotoList.Select(t => new Models.ImageItem
            {
                Description = t.Description,
                Height = t.Sizes.Image[1],
                Width = t.Sizes.Image[0],
                Source = t.Large,
                Title = t.AlbumTitle
            }).ToList();

            new Views.ViewImagePage(imageList).Show();
        }
    }
}
