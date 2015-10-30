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

        private const int QUERY_COUNT = 20;

        public ObservableCollection<Group> GroupList { get; private set; }

        public ObservableCollection<ChannelTopic> TopicList { get; private set; }

        public ChannelDetailViewModel()
        {
            this.GroupList = new ObservableCollection<Group>();
            this.TopicList = new ObservableCollection<ChannelTopic>();
        }

        public void Init(Channel channel)
        {
            this.Channel = channel;

            this.InitGroups();

            this.LoadTopics();
        }

        private async Task InitGroups()
        {
            var groupList = await this.ApiClient.GetGroupByChannel(this.Channel.Name);

            foreach (var group in groupList.Items)
            {
                this.GroupList.Add(group);
            }
        }

        private async Task LoadTopics()
        {
            if (this.IsLoading)
            {
                return;
            }

            try
            {
                this.IsLoading = true;

                var start = this.TopicList.Count;

                var topicList = await this.ApiClient.GetTopicByChannel(this.Channel.Name, start, QUERY_COUNT);

                foreach (var topic in topicList.Topics)
                {
                    this.TopicList.Add(topic);
                }
            }
            finally
            {
                this.IsLoading = false;
            }
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

        private DelegateCommand _loadMoreCommand;

        public DelegateCommand LoadMoreCommand
        {
            get
            {
                if (_loadMoreCommand == null)
                {
                    _loadMoreCommand = new DelegateCommand(this.LoadMore);
                }
                return _loadMoreCommand;
            }
        }

        private void LoadMore()
        {
            this.LoadTopics();
        }
    }
}
