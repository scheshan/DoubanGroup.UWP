using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class Comment : BindableBase
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        private int _voteCount;

        [JsonProperty("vote_count")]
        public int VoteCount
        {
            get { return _voteCount; }
            set { this.SetProperty(ref _voteCount, value); }
        }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("quote_comment")]
        public Comment QuoteComment { get; set; }

        private bool _canVote = true;

        [JsonIgnore]
        public bool CanVote
        {
            get { return _canVote; }
            set { this.SetProperty(ref _canVote, value); }
        }
    }
}
