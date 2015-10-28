using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api
{
    public class ApiException : Exception
    {
        public ApiError Error { get; private set; }

        public ApiException(ApiError error)
            : base(error.Message)
        {
            this.Error = error;
        }
    }

    public class ApiError
    {
        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }
    }
}
