using DoubanGroup.Core.Api.Entity;
using DoubanGroup.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.DesignData
{
    public class HomePageData
    {
        public List<Channel> ChannelList { get; set; }

        public HomePageData()
        {
            this.ChannelList = new List<Channel>();

            this.ChannelList.Add(new Channel { NameCN = "精选" });
            this.ChannelList.Add(new Channel { NameCN = "文化" });
            this.ChannelList.Add(new Channel { NameCN = "行摄" });
            this.ChannelList.Add(new Channel { NameCN = "娱乐" });
            this.ChannelList.Add(new Channel { NameCN = "时尚" });
            this.ChannelList.Add(new Channel { NameCN = "生活" });
            this.ChannelList.Add(new Channel { NameCN = "科技" });
        }
    }
}
