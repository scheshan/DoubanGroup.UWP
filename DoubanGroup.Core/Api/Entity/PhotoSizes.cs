using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class PhotoSizes
    {
        [JsonProperty("cover")]
        public long[] Cover { get; set; }

        [JsonProperty("icon")]
        public long[] Icon { get; set; }

        [JsonProperty("image")]
        public long[] Image { get; set; }

        [JsonProperty("large")]
        public long[] Large { get; set; }

        [JsonProperty("thumb")]
        public long[] Thumb { get; set; }
    }
}
