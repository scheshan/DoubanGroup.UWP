using DoubanGroup.Core.Api;
using DoubanGroup.Core.Api.Entity;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class ChannelDetailViewModel : ViewModelBase
    {
        private Channel _channel;

        public Channel Channel
        {
            get { return _channel; }
            set { this.SetProperty(ref _channel, value); }
        }

        public ObservableCollection<Group> GroupList { get; private set; }

        private ApiClient ApiClient { get; set; }        

        public ChannelDetailViewModel(ApiClient apiClient)
        {
            this.ApiClient = apiClient;
            this.GroupList = new ObservableCollection<Group>();
        }

        public void Init(Channel channel)
        {
            this.Channel = channel;

            this.InitGroups();
        }

        private async Task InitGroups()
        {
            var groupList = await this.ApiClient.GetGroupByChannel(this.Channel.Name);

            foreach (var group in groupList.Items)
            {
                this.GroupList.Add(group);
            }
        }
    }
}
