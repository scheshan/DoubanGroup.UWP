using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class PhotoList
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
