using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class UserDetail
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("join_group_count")]
        public int JoinGroupCount { get; set; }

        [JsonProperty("join_groups")]
        public GroupList JoinedGroups { get; set; }

        [JsonProperty("like_topic_count")]
        public int LikeTopicCount { get; set; }

        [JsonProperty("rec_topic_count")]
        public int RecommandTopicCount { get; set; }

        [JsonProperty("album_count")]
        public int AlbumCount { get; set; }
    }
}
