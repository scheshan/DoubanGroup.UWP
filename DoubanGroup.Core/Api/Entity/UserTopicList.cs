using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class UserTopicList
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("topics")]
        public List<Topic> Topics { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
    }
}
