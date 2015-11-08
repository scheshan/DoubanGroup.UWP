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

        private string _largeAvatar;

        [JsonProperty("large_avatar")]
        public string LargeAvatar
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_largeAvatar))
                {
                    return _largeAvatar;
                }

                return this.Avatar;
            }
            set
            {
                _largeAvatar = value;
            }
        }

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

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("loc_id")]
        public long LocationID { get; set; }

        [JsonProperty("loc_name")]
        public string LocationName { get; set; }

        [JsonProperty("blocking")]
        public bool Blocking { get; set; }

        [JsonProperty("following")]
        public bool Following { get; set; }

        [JsonProperty("following_count")]
        public long FollowingCount { get; set; }
    }
}
