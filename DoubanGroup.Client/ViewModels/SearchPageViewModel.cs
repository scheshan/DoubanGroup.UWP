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
    public class SearchPageViewModel : ViewModelBase
    {
        public IncrementalLoadingList<Group> GroupList { get; private set; }

        public IncrementalLoadingList<Topic> TopicList { get; private set; }

        private string _keywords;

        public string Keywords
        {
            get { return _keywords; }
            set { this.SetProperty(ref _keywords, value); }
        }

        public SearchPageViewModel()
        {
            this.GroupList = new IncrementalLoadingList<Group>(this.LoadGroups);
            this.TopicList = new IncrementalLoadingList<Topic>(this.LoadTopics);
        }

        private async Task<IEnumerable<Group>> LoadGroups(uint count)
        {
            var groupList = await this.RunTaskAsync(this.ApiClient.SearchGroup(this.Keywords, this.GroupList.Count, 50));

            if (groupList == null || groupList.Items.Count < 50)
            {
                this.GroupList.NoMore();
            }

            return groupList?.Items;
        }

        private async Task<IEnumerable<Topic>> LoadTopics(uint count)
        {
            var topicList = await this.RunTaskAsync(this.ApiClient.SearchTopic(this.Keywords, this.TopicList.Count, 30));

            if (topicList == null || topicList.Items.Count < 30)
            {
                this.TopicList.NoMore();
            }

            return topicList?.Items;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            this.Keywords = e.Parameter as string;
        }
    }
}
