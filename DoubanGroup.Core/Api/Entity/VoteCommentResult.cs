using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class VoteCommentResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
    }
}
