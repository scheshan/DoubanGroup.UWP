using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using DoubanGroup.Client.Models;
using DoubanGroup.Core.Api;
using Prism.Commands;

namespace DoubanGroup.Client.ViewModels
{
    public class GroupDetailPageViewModel : ViewModelBase
    {
        private long _groupID;

        public long GroupID
        {
            get { return _groupID; }
            set { this.SetProperty(ref _groupID, value); }
        }

        private Group _group;

        public Group Group
        {
            get { return _group; }
            set { this.SetProperty(ref _group, value); }
        }

        private ApiClient ApiClient { get; set; }

        private INavigationService NavigationService { get; set; }

        public IncrementalLoadingList<Topic> TopicList { get; private set; }

        public GroupDetailPageViewModel(ApiClient apiClient, INavigationService navigationService)
        {
            this.ApiClient = apiClient;
            this.NavigationService = navigationService;
            this.TopicList = new IncrementalLoadingList<Topic>(this.LoadTopics);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            this.GroupID = (long)e.Parameter;

            this.LoadGroup();
        }

        private async Task<IEnumerable<Topic>> LoadTopics(uint count)
        {
            this.IsLoading = true;

            var topicList = await this.ApiClient.GetTopicByGroup(this.GroupID, this.TopicList.Count, 30);

            this.IsLoading = false;

            return topicList.Items;
        }

        private async Task LoadGroup()
        {
            var group = await this.ApiClient.GetGroup(this.GroupID);
            this.Group = group;
        }

        private DelegateCommand _viewMembersCommand;

        public DelegateCommand ViewMembersCommand
        {
            get
            {
                if (_viewMembersCommand == null)
                {
                    _viewMembersCommand = new DelegateCommand(this.ViewMembers);
                }
                return _viewMembersCommand;
            }
        }

        private void ViewMembers()
        {
            this.NavigationService.Navigate("GroupMembers", this.GroupID);
        }
    }
}
