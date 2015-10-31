using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.DesignData
{
    public class GroupMembersData
    {
        public Group Group { get; set; }

        public List<User> UserList { get; set; }

        public GroupMembersData()
        {
            this.Group = new Group
            {
                Name = "成都豆瓣",
                Description = "豆瓣新增功能：成都小站上线 http://site.douban.com/106925/ 成都豆瓣YY频道：★40364510【新】 成都豆瓣QQ群：49911873【满】 成都豆瓣80后群：48903391【满】 成都豆瓣90后群：★64117162 【新】 成都豆瓣FB群：★77976556【新】（聚会交友、技能交换） ★★成都豆瓣靠谱恋爱群：136792819【新】（婚恋交友、杜绝YP，限制男23岁以上，女22岁以上） 成都豆瓣公益群：7779271（紧急招募支教老师） ★★成都豆瓣微博群：http://q.t.sina.com.cn/202943 加群请慎重 请不要同时加入两个群··· 欢迎各位豆友加入，请注明你的豆瓣ID。~~~~~~~~~~~~ 在豆瓣的成都朋友。 欢迎新老朋友在成豆多发贴，发好贴，共建有聊有爱的小组氛围。即日起成豆管理员将参照以下原则进行小组管理，改善小组发言环境。希望大家理解配合，并欢迎各位提出意见建议。 ●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●● 吃喝玩乐在成都： http://www.douban.com/group/mocube/ ●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●● =============2010.08.24 更新============= 一、成豆话题关闭及删帖原则 1.一切违反社区指导原则的内容将被删除；（社区指导原则：http://www.douban.com/about?policy=guideline） 2.软硬广告贴、色情擦边贴将被删除； 3.应发帖人要求删帖； 4.纯水帖，挖坟贴，标题党可能在一定时间后被关闭； 5,标题中夹杂大量符号，粗俗言论，恶意吸引注意的，删； 6.房屋租赁与物品转让相关信息请统一发至置顶帖，未按要求发帖的将直接删除。 二、成豆成员被踢出小组及永久封禁原则 1. 凡是在组内发布的帖子或回复被豆瓣官方删除，发帖人将被直接封禁； 2. 粗口漫骂，将被直接封禁； 3. 人身攻击，语言恶毒将被直接封禁； 4.肆意爆组及关联人等将被永久封禁。 5.个人要求删除所有发帖的，一律按爆组处理，永久封禁。 成都相关信息&成都相关推荐小组 http://www.douban.com/group/topic/10751524/ ●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●● 一夜情，约炮等帖子和豆邮截图后请发Email至cdougo@163.com，定期处理 ●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●● 最新通告：未按要求发在置顶帖里的的租房帖、售物帖一律删除。连击回帖者视爆组行为一律封禁，请自觉规范，谢谢配合。 发帖约炮的，无故骚扰别人经人举报的，一律永久封禁！ 最近成豆舌战成风，凡是有个人纠纷的请私下解决。 开帖吵架的，搬弄他人是非的，直接删帖！！！！！！！",
                Avatar = "http://img3.douban.com/icon/g10156-13.jpg",
                LargeAvatar = "http://img4.douban.com/view/group/medium/public/b76db7fdba3c448.jpg",
                AdminRoleName = "管理员",                
                ID = 10156,
                MemberCount = 103745,
                Owner = new User
                {
                    Name = "冰海蓝",
                    Avatar = "http://img3.douban.com/icon/u1001081-1.jpg",
                    LargeAvatar = "http://img3.douban.com/icon/user_large.jpg"
                },
                AdminList = new List<User>
                {
                    new User
                    {
                        Name = "旯旯",
                        Avatar = "http://img3.douban.com/icon/u1001081-1.jpg",
                        LargeAvatar = "http://img3.douban.com/icon/user_large.jpg"
                    },
                    new User
                    {
                        Name = "倾城",
                        Avatar = "http://img3.douban.com/icon/u1001081-1.jpg",
                        LargeAvatar = "http://img3.douban.com/icon/user_large.jpg"
                    },
                    new User
                    {
                        Name = "。桑-♥.娃儿",
                        Avatar = "http://img3.douban.com/icon/u1001081-1.jpg",
                        LargeAvatar = "http://img3.douban.com/icon/user_large.jpg"
                    },
                    new User
                    {
                        Name = "冰海蓝",
                        Avatar = "http://img3.douban.com/icon/u1001081-1.jpg",
                        LargeAvatar = "http://img3.douban.com/icon/user_large.jpg"
                    }
                }
            };

            this.UserList = new List<User>();

            for (var i = 0; i < 50; i++)
            {
                this.UserList.Add(new User
                {
                    Name = "冰海蓝",
                    Avatar = "http://img3.douban.com/icon/u1001081-1.jpg",
                    LargeAvatar = "http://img3.douban.com/icon/user_large.jpg"
                });
            }
        }
    }
}
