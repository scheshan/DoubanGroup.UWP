using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class User
    {
        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("is_suicide")]
        public bool IsSuicide { get; set; }

        [JsonProperty("large_avatar")]
        public string LargeAvatar { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uid")]
        public string UID { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
