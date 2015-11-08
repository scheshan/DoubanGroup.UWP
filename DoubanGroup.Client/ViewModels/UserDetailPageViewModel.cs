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
            this.IsLoading = true;

            var userDetail = await this.ApiClient.GetUserDetail(this.UserID, 20);
            var photoList = await this.ApiClient.GetUserCreatedPhotos(this.UserID, 0, 20);

            this.IsLoading = false;

            this.User = userDetail.User;

            if (userDetail.JoinedGroups?.Count > 0)
            {
                foreach (var group in userDetail.JoinedGroups.Items)
                {
                    this.TopGroupList.Add(group);
                    this.JoinedGroupList.Add(group);
                }
            }

            foreach (var photo in photoList.Photos)
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
            this.IsLoading = true;

            var groupList = await this.ApiClient.GetUserJoinedGroups(this.UserID, this.JoinedGroupList.Count, 100);

            if(groupList.Items.Count < 100)
            {
                this.JoinedGroupList.NoMore();
            }

            this.IsLoading = false;

            return groupList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadRecommandTopicList(uint count)
        {
            this.IsLoading = true;

            var topicList = await this.ApiClient.GetTopicByUserRecommand(this.UserID, this.RecommandTopicList.Count, 30);

            this.IsLoading = false;

            if(topicList.Items.Count < 30)
            {
                this.RecommandTopicList.NoMore();
            }

            return topicList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadLikeTopicList(uint count)
        {
            this.IsLoading = true;

            var topicList = await this.ApiClient.GetTopicByUserLike(this.UserID, this.LikeTopicList.Count, 30);

            this.IsLoading = false;

            if (topicList.Items.Count < 30)
            {
                this.LikeTopicList.NoMore();
            }

            return topicList.Items;
        }
    }
}
