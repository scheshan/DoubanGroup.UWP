using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api.Entity
{
    public class Session : BindableBase
    {
        private string _accessToken;

        [JsonProperty("access_token")]
        public string AccessToken
        {
            get { return _accessToken; }
            set { this.SetProperty(ref _accessToken, value); }
        }

        private long _expiresIn;

        [JsonProperty("expires_in")]
        public long ExpiresIn
        {
            get { return _expiresIn; }
            set { this.SetProperty(ref _expiresIn, value); }
        }

        private string _refreshToken;

        [JsonProperty("refresh_token")]
        public string RefreshToken
        {
            get { return _refreshToken; }
            set { this.SetProperty(ref _refreshToken, value); }
        }

        private long _doubanUserID;

        [JsonProperty("douban_user_id")]
        public long DoubanUserID
        {
            get { return _doubanUserID; }
            set { this.SetProperty(ref _doubanUserID, value); }
        }

        private string _doubanUserName;

        [JsonProperty("douban_user_name")]
        public string DoubanUserName
        {
            get { return _doubanUserName; }
            set { this.SetProperty(ref _doubanUserName, value); }
        }
    }
}
