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

        public List<Comment> PopularCommentList { get; set; }

        public List<Comment> CommentList { get; set; }

        public TopicDetailData()
        {
            this.Topic = new Topic
            {
                Title = "哪家医院有做性别鉴定的吗",
                Content = "想说这个问题已经讨论烂了。各大社交翻了都有这个话题，的确英语专业的人太多了，包括我自己也是英语专业（日语二外） 其实本身会英语是个很大的优势，你可以去很多只要专业性不太强的行业，从助理开始做起，之所以楼主你会迷茫是对你所从事的行业没什么兴趣，一方面可能是公司人情世故累 一方面也可能是觉得没什么核心竞争力容易被替代。 要问英语专业可以做什么？私认为有三个主的方向。 【1】 靠英语吃饭的工作。 如 英语老师（培训机构和公私立学校） 留学文案（专门帮忙撰写申请国外大学的PS 等文件） 英语笔译口译。 【2】.与英语沾边的工作，用英语作为工具的工作。 如 金融面：电子商务外贸（需要匹配具体某行业一定的外贸知识销售技巧） 会计 （很多女生都选择学外语专业另外修会计，的确是可以做到老的工作， 办公室也算比较不用外面晒太阳，稳定.......） 行政面： 外企总助（大的环境氛围需要你会英语而且门槛还不低，这需要会人情世故 会做人） IT技术面：（这个范围很广 不好细说，有做软件测试的，有写程式的，有设计的，但是这都是要去匹配专业技术性强的知识） 法律合同方面：（这个我有认识的人是做这个的，专门是核对起草英语商务合同，但是也是需要一定的法律知识的） 旅游观光方面：会外语还要会导游相关的知识，带国外团也是好赚，但是辛苦阿！ .......................................................... 这一系列太多了 不赘述，只说了几个主流，用到英语的工作 其实非常非常多，沾到边的情况下，再找到你喜欢的方向，坚持下去，这样也是核心竞争力阿！曾经我也这样跟我朋友说：XX，你说学个英语有什么用，我怎么老是做沟通传话打杂的工作呢？？ “会沟通才是最强大的技术！” 顿时，觉得好燃··················· 【3】完全不用到英语的工作。 哈哈·~说到这个 好像有点离题了。但是人生苦短 别太局限自己嘛 不一定要用到英语阿~。这一项里又有好多可以说了，你可以卖车卖保险，你也可以开早餐店开淘宝，考公务员·~~太多太多了不举例了~ 废话那么多，别嫌弃，其实我也一直在思索这个问题，从大四想到毕业工作两年还没想清楚，总之是下班有时间就充实下自己，说不定哪天转行用到了呢━━∑(￣□￣*|||━━",
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

            this.PopularCommentList = new List<Comment>();
            this.CommentList = new List<Comment>();

            for (var i = 0; i < 10; i++)
            {
                var comment = new Comment
                {
                    Author = new BasicUserInfo
                    {
                        Name = "澈目",
                        Avatar = "http://img3.douban.com/icon/u80138337-11.jpg",
                        LargeAvatar = "http://img3.douban.com/icon/up80138337-11.jpg"
                    },
                    Text = "留学中介好像就一二线城市城市都多点，其他城市就业也比较少，有同学做了一年也出来了",
                    Time = DateTime.Now.AddMinutes(-20),
                    QuoteComment = new Comment
                    {
                        Text = "倒数第二张，还没有开的时候真的很像双色冰淇淋",
                        Author = new BasicUserInfo
                        {
                            Name = "梅子是六月成熟"
                        }
                    }
                };

                if (i < 3)
                {
                    this.PopularCommentList.Add(comment);
                }
                this.CommentList.Add(comment);
            }
        }
    }
}
