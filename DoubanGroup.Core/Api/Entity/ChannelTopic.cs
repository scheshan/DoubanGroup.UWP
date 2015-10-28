using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class ChannelTopic
    {
        [JsonProperty("topic_channel_datetime")]
        public DateTime TopicChannelDatetime { get; set; }

        [JsonProperty("topic")]
        public Topic Topic { get; set; }
    }
}
