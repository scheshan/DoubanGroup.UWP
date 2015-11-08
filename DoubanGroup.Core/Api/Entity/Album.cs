using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class Album
    {
        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("liked")]
        public bool Liked { get; set; }

        [JsonProperty("liked_count")]
        public int LikedCount { get; set; }

        [JsonProperty("need_watermark")]
        public bool NeedWatermark { get; set; }

        [JsonProperty("photo_order")]
        public string PhotoOrder { get; set; }

        [JsonProperty("recs_count")]
        public int RecommandsCount { get; set; }

        [JsonProperty("reply_limit")]
        public bool ReplyLimit { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("sizes")]
        public PhotoSizes Sizes { get; set; }
    }
}
