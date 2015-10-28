using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class ChannelTopicList
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("topics")]
        public List<ChannelTopic> Topics { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
