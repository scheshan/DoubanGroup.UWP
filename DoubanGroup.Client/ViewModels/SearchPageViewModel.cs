using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using DoubanGroup.Client.Models;

namespace DoubanGroup.Client.ViewModels
{
    public class SearchPageViewModel : NavigationViewModelBase
    {
        public RefreshableViewModel<Group> GroupViewModel { get; private set; }

        public RefreshableViewModel<Topic> TopicViewModel { get; private set; }

        private string _keywords;

        public string Keywords
        {
            get { return _keywords; }
            set { this.SetProperty(ref _keywords, value); }
        }

        public SearchPageViewModel()
        {
            this.GroupViewModel = new RefreshableViewModel<Group>(this.LoadGroups, 50);
            this.TopicViewModel = new RefreshableViewModel<Topic>(this.LoadTopics, 30);
        }

        private async Task<IEnumerable<Group>> LoadGroups(int start, int count)
        {
            var groupList = await this.ApiClient.SearchGroup(this.Keywords, start, count);

            return groupList.Items;
        }

        private async Task<IEnumerable<Topic>> LoadTopics(int start, int count)
        {
            var topicList = await this.ApiClient.SearchTopic(this.Keywords, start, count);

            return topicList.Items;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Keywords = e.Parameter as string;
        }
    }
}
