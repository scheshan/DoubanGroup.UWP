using DoubanGroup.Core.Api.Entity;
using DoubanGroup.Client.Models;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoubanGroup.Core.Api;

namespace DoubanGroup.Client.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<ChannelDetailViewModel> ChannelList { get; private set; }

        private ChannelDetailViewModel _currentChannel;

        public ChannelDetailViewModel CurrentChannel
        {
            get { return _currentChannel; }
            set
            {
                if (this.SetProperty(ref _currentChannel, value))
                {
                    value?.Init();
                }
            }
        }

        public HomePageViewModel()
        {
            this.ChannelList = new ObservableCollection<ChannelDetailViewModel>();

            this.InitChannels();
        }

        private async Task InitChannels()
        {
#if DEBUG
            var channels = new List<Channel>();
            channels.Add(new Channel { NameCN = "精选", Name = "all" });
            channels.Add(new Channel { NameCN = "文化", Name = "culture" });
            channels.Add(new Channel { NameCN = "行摄", Name = "travel" });
            channels.Add(new Channel { NameCN = "娱乐", Name = "ent" });
            channels.Add(new Channel { NameCN = "时尚", Name = "fashion" });
            channels.Add(new Channel { NameCN = "生活", Name = "life" });
            channels.Add(new Channel { NameCN = "科技", Name = "tech" });
#else
            var channels = await this.ApiClient.GetChannelList();
#endif
            foreach (var channel in channels)
            {
                var vm = new ChannelDetailViewModel(channel);
                this.ChannelList.Add(vm);
            }
        }

        private DelegateCommand _refreshCommand;

        public DelegateCommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new DelegateCommand(this.Refresh);
                }
                return _refreshCommand;
            }
        }

        private void Refresh()
        {
            this.CurrentChannel?.Refresh();
        }
    }
}
