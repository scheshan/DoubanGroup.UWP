using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Core.Api
{
    public sealed class Parameters : NameValueCollection
    {
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            foreach (string key in this)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.AppendFormat("{0}={1}", key, this[key]);
            }

            return sb.ToString();
        }

        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            List<KeyValuePair<string, string>> kvList = new List<KeyValuePair<string, string>>();

            foreach (string key in this)
            {
                kvList.Add(new KeyValuePair<string, string>(key, this[key]));
            }

            return kvList;
        }
    }
}
