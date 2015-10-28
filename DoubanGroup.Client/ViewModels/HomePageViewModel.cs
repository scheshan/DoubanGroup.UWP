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

namespace DoubanGroup.Client.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<ChannelModel> ChannelList { get; private set; }

        private ChannelModel _currentChannel;

        public ChannelModel CurrentChannel
        {
            get { return _currentChannel; }
            set { this.SetProperty(ref _currentChannel, value); }
        }

        public ObservableCollection<Group> GroupList { get; private set; }

        public HomePageViewModel()
        {
            this.ChannelList = new ObservableCollection<ChannelModel>();

            this.GroupList = new ObservableCollection<Group>();

            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "精选" }));
            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "文化" }));
            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "行摄" }));
            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "娱乐" }));
            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "时尚" }));
            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "生活" }));
            this.ChannelList.Add(new ChannelModel(new Channel { NameCN = "科技" }));

            this.CurrentChannel = this.ChannelList[0];
            this.CurrentChannel.IsSelected = true;
        }

        private DelegateCommand<ChannelModel> _changeChannelCommand;

        public DelegateCommand<ChannelModel> ChangeChannelCommand
        {
            get
            {
                if (_changeChannelCommand == null)
                {
                    _changeChannelCommand = new DelegateCommand<ChannelModel>(ChangeChannel);
                }
                return _changeChannelCommand;
            }
        }

        private void ChangeChannel(ChannelModel parameter)
        {
            if (this.CurrentChannel == parameter)
            {
                return;
            }

            this.CurrentChannel.IsSelected = false;
            this.CurrentChannel = parameter;
            this.CurrentChannel.IsSelected = true;
        }

    }
}
