using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using DoubanGroup.Core.Api;

namespace DoubanGroup.Client.ViewModels
{
    public class GroupMembersPageViewModel : ViewModelBase
    {
        private long GroupID { get; set; }

        private Group _group;

        public Group Group
        {
            get { return _group; }
            set { this.SetProperty(ref _group, value); }
        }

        private long _totalMembers;

        public long TotalMembers
        {
            get { return _totalMembers; }
            set { this.SetProperty(ref _totalMembers, value); }
        }

        public Models.IncrementalLoadingList<BasicUserInfo> UserList { get; private set; }

        private ApiClient ApiClient { get; set; }

        private INavigationService NavigationService { get; set; }

        public GroupMembersPageViewModel(ApiClient apiClient, INavigationService navigationService)
        {
            this.ApiClient = apiClient;
            this.NavigationService = navigationService;

            this.UserList = new Models.IncrementalLoadingList<BasicUserInfo>(this.LoadUser);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            this.GroupID = (long)e.Parameter;

            base.OnNavigatedTo(e, viewModelState);

            this.LoadGroup();
        }

        private async Task LoadGroup()
        {
            var group = await this.ApiClient.GetGroup(this.GroupID);
            this.Group = group;
        }

        private async Task<IEnumerable<BasicUserInfo>> LoadUser(uint count)
        {
            this.IsLoading = true;

            var userList = await this.ApiClient.GetGroupMembers(this.GroupID, this.UserList.Count, 50);

            this.IsLoading = false;

            this.TotalMembers = userList.Total;

            return userList.Items;
        }
    }
}
