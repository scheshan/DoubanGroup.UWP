using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DoubanGroup.Client.Extensions
{
    public static class ApplicationDataContainerExtensions
    {
        public static void Set(this ApplicationDataContainer container, string key, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            container.Values[key] = json;
        }

        public static T Get<T>(this ApplicationDataContainer container, string key)
        {
            if (container.Values.ContainsKey(key))
            {
                string json = (string)container.Values[key];

                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        public static void Remove(this ApplicationDataContainer container, string key)
        {
            if (container.Values.ContainsKey(key))
            {
                container.Values.Remove(key);
            }
        }
    }
}
