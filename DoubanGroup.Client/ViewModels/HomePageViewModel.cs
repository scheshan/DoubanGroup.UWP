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
        public ObservableCollection<Channel> ChannelList { get; private set; }

        public HomePageViewModel()
        {
            this.ChannelList = new ObservableCollection<Channel>();

            this.InitChannels();
        }

        private void InitChannels()
        {
            this.ChannelList.Add(new Channel { NameCN = "精选", Name = "all" });
            this.ChannelList.Add(new Channel { NameCN = "文化", Name = "culture" });
            this.ChannelList.Add(new Channel { NameCN = "行摄", Name = "travel" });
            this.ChannelList.Add(new Channel { NameCN = "娱乐", Name = "ent" });
            this.ChannelList.Add(new Channel { NameCN = "时尚", Name = "fashion" });
            this.ChannelList.Add(new Channel { NameCN = "生活", Name = "life" });
            this.ChannelList.Add(new Channel { NameCN = "科技", Name = "tech" });
        }
    }
}
