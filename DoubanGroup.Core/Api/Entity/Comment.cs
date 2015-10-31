using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class Comment
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("quote_comment")]
        public Comment QuoteComment { get; set; }
    }
}
