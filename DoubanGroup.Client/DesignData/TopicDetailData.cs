using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.DesignData
{
    public class TopicDetailData
    {
        public Topic Topic { get; set; }

        public TopicDetailData()
        {
            this.Topic = new Topic
            {
                Title = "哪家医院有做性别鉴定的吗",
                Content = "为什么看到有些妹子感觉我的幻肢硬了 上社交app从来不看男的 只看美女 我是不是其实有屌 只是太小了自己没找到 喵 <图片1>",
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        Alt = "http://img3.douban.com/view/group_topic/large/public/p37623231.jpg"
                    }
                },
                Author = new BasicUserInfo
                {
                    Name = "澈目",
                    Avatar = "http://img3.douban.com/icon/u80138337-11.jpg",
                    LargeAvatar = "http://img3.douban.com/icon/up80138337-11.jpg"
                }
            };
        }
    }
}
