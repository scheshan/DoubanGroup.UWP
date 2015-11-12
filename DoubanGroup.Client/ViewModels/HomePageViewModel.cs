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
            var channels = await this.RunTaskAsync(this.ApiClient.GetChannelList());

            if (channels == null)
            {
                return;
            }

            foreach (var channel in channels)
            {
                var vm = new ChannelDetailViewModel(channel);
                this.ChannelList.Add(vm);
            }
        }
    }
}
