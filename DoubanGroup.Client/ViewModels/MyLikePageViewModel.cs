using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class MyLikePageViewModel : ViewModelBase
    {
        public Models.IncrementalLoadingList<Topic> TopicList { get; private set; }

        public MyLikePageViewModel()
        {
            this.TopicList = new Models.IncrementalLoadingList<Topic>(this.LoadTopics);
        }

        private async Task<IEnumerable<Topic>> LoadTopics(uint count)
        {
            this.IsLoading = true;

            var start = this.TopicList.Count;

            var userTopicList = await this.ApiClient.GetLikedTopis(start, 50);

            this.IsLoading = false;

            if (userTopicList.Total <= this.TopicList.Count)
            {
                this.TopicList.NoMoreItems();
            }

            return userTopicList.Items;
        }
    }
}
