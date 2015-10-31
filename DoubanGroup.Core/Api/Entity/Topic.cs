using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class Topic
    {
        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("comments_count")]
        public int CommentsCount { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("liked")]
        public bool Liked { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
        
        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonIgnore]
        public string ShortContent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Content))
                {
                    return string.Empty;
                }

                var content = this.Content.Replace("\r", "").Replace("\n", "").Replace(" ", "");

                if (content.Length <= 200)
                {
                    return content;
                }

                return content.Substring(0, 200) + "...";
            }
        }

        [JsonIgnore]
        public string LikeCountString
        {
            get
            {
                if (this.LikeCount < 1000)
                {
                    return this.LikeCount.ToString();
                }
                else if (this.LikeCount < 10000)
                {
                    return $"{this.LikeCount / 1000}K";
                }
                else
                {
                    return "10K+";
                }
            }
        }

        [JsonIgnore]
        public Photo Avatar
        {
            get
            {
                return this.Photos.FirstOrDefault();
            }
        }
    }
}
