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
    public class UserDetailPageViewModel : NavigationViewModelBase
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

        public RefreshableViewModel<Group> JoinedGroupViewModel { get; private set; }

        public RefreshableViewModel<Topic> RecommandTopicViewModel { get; private set; }

        public RefreshableViewModel<Topic> LikeTopicViewModel { get; private set; }

        public RefreshableViewModel<Album> AlbumViewModel { get; private set; }

        public UserDetailPageViewModel()
        {
            this.TopGroupList = new ObservableCollection<Group>();
            this.TopPhotoList = new ObservableCollection<Photo>();

            this.JoinedGroupViewModel = new RefreshableViewModel<Group>(this.LoadJoinedGroups, 100);
            this.RecommandTopicViewModel = new RefreshableViewModel<Topic>(this.LoadRecommandTopics, 30);
            this.LikeTopicViewModel = new RefreshableViewModel<Topic>(this.LoadLikeTopics, 30);
            this.AlbumViewModel = new RefreshableViewModel<Album>(this.LoadAlbums, 30);
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
                    this.JoinedGroupViewModel.ItemList.Add(group);
                }
            }

            foreach (var photo in photoList?.Photos)
            {
                this.TopPhotoList.Add(photo);
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.UserID = (long)e.Parameter;

            if (e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.New)
            {
                this.InitData();
            }
        }

        private async Task<IEnumerable<Group>> LoadJoinedGroups(int start, int count)
        {
            var groupList = await this.ApiClient.GetUserJoinedGroups(this.UserID, start, count);
            return groupList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadRecommandTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetUserRecommandTopics(this.UserID, start, count);
            return topicList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadLikeTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetUserLikeTopics(this.UserID, start, count);
            return topicList.Items;
        }

        private async Task<IEnumerable<Album>> LoadAlbums(int start, int count)
        {
            var albumList = await this.ApiClient.GetUserCreatedAlbums(this.UserID, start, count);
            return albumList.Albums;
        }

        private DelegateCommand<Photo> _viewImageCommand;

        public DelegateCommand<Photo> ViewImageCommand
        {
            get
            {
                if (_viewImageCommand == null)
                {
                    _viewImageCommand = new DelegateCommand<Photo>(ViewImage);
                }
                return _viewImageCommand;
            }
        }

        private void ViewImage(Photo parameter)
        {
            var imageList = this.TopPhotoList.Select(t => new Models.ImageItem
            {
                Description = t.Description,
                Height = t.Sizes.Image[1],
                Width = t.Sizes.Image[0],
                Source = t.Large,
                Title = t.AlbumTitle,
                SourceObject = t
            }).ToList();

            var currentImage = imageList.FirstOrDefault(t => t.SourceObject == parameter);

            new Views.ViewImagePage(imageList, currentImage).Show();
        }
    }
}
