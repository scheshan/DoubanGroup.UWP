using DoubanGroup.Core.Api;
using DoubanGroup.Core.Api.Entity;
using Prism.Commands;
using Prism.Windows.Navigation;
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

        public Models.IncrementalLoadingList<ChannelTopic> TopicList { get; private set; }

        private bool _initialized = false;

        public ChannelDetailViewModel(Channel channel)
        {
            this.Channel = channel;
            this.GroupList = new ObservableCollection<Group>();
            this.TopicList = new Models.IncrementalLoadingList<ChannelTopic>(this.LoadTopics);
        }

        public void Init()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            this.LoadGroups();
        }

        private async Task LoadGroups()
        {
            var groupList = await this.RunTaskAsync(this.ApiClient.GetChannelGroups(this.Channel.Name));

            foreach (var group in groupList?.Items)
            {
                this.GroupList.Add(group);
            }
        }

        private async Task<IEnumerable<ChannelTopic>> LoadTopics(uint count)
        {
            int queryCount = 30;

            var topicList = await this.RunTaskAsync(this.ApiClient.GetChannelTopics(this.Channel.Name, this.TopicList.Count, queryCount));

            if (topicList == null || topicList.Count < queryCount)
            {
                this.TopicList.NoMore();
            }

            return topicList.Topics;
        }

        private DelegateCommand<Group> _viewGroupCommand;

        public DelegateCommand<Group> ViewGroupCommand
        {
            get
            {
                if (_viewGroupCommand == null)
                {
                    _viewGroupCommand = new DelegateCommand<Group>(this.ViewGroup);
                }
                return _viewGroupCommand;
            }
        }

        private void ViewGroup(Group parameter)
        {
            this.NavigationService.Navigate("GroupDetail", parameter.ID);
        }

        private DelegateCommand<ChannelTopic> _viewTopicCommand;

        public DelegateCommand<ChannelTopic> ViewTopicCommand
        {
            get
            {
                if (_viewTopicCommand == null)
                {
                    _viewTopicCommand = new DelegateCommand<ChannelTopic>(ViewTopic);
                }
                return _viewTopicCommand;
            }
        }

        private void ViewTopic(ChannelTopic parameter)
        {
            this.NavigationService.Navigate("TopicDetail", parameter.Topic.ID);
        }

        public void Refresh()
        {
            this.GroupList.Clear();
            this.TopicList.Clear();
            this.TopicList.HasMore();            
            this.LoadGroups();
        }
    }
}
