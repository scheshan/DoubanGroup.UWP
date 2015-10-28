using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class Group
    {
        [JsonProperty("admin_role_name")]
        public string AdminRoleName { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("join_type")]
        public string JoinType { get; set; }

        [JsonProperty("large_avatar")]
        public string LargeAvatar { get; set; }

        [JsonProperty("member_count")]
        public long MemberCount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("admins")]
        public List<BasicUserInfo> AdminList { get; set; }

        [JsonProperty("owner")]
        public BasicUserInfo Owner { get; set; }
    }
}
