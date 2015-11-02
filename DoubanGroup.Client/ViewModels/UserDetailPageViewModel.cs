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

        public ObservableCollection<Group> JoinedGroupList { get; private set; }

        public UserDetailPageViewModel()
        {
            this.JoinedGroupList = new ObservableCollection<Group>();
        }

        private async Task InitData()
        {
            this.IsLoading = true;

            var userDetail = await this.ApiClient.GetUserDetail(this.UserID, 20);

            this.IsLoading = false;

            this.User = userDetail.User;

            if (userDetail.JoinedGroups?.Count > 0)
            {
                foreach (var group in userDetail.JoinedGroups.Items)
                {
                    this.JoinedGroupList.Add(group);
                }
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            this.UserID = (long)e.Parameter;

            this.InitData();
        }
    }
}
