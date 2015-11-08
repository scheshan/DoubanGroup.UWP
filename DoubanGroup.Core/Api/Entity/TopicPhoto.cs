using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class TopicPhoto
    {
        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("author_id")]
        public long AuthorID { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("seq_id")]
        public int SeqID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("topic_id")]
        public long TopicID { get; set; }

        [JsonProperty("size")]
        public TopicPhotoSize Size { get; set; }
    }
}
