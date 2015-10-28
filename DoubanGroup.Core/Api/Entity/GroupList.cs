using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class GroupList
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("groups")]
        public List<Group> Items { get; set; }
    }
}
