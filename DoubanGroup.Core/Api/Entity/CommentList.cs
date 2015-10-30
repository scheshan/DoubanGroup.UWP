using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class CommentList
    {
        [JsonProperty("popular_comments")]
        public List<Comment> PopularComments { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
        
        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }
    }
}
