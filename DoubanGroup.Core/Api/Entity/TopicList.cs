using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class TopicList
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("topics")]
        public List<Topic> Items { get; set; }
    }
}
