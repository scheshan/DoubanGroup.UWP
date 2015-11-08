using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class AlbumPhotoList
    {
        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("sortby")]
        public string SortBy { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
