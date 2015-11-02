using Prism.Windows.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Extensions
{
    public static class ISessionStateServiceExtensions
    {
        public static void Set(this ISessionStateService service, string key, object value)
        {
            if (service.SessionState.ContainsKey(key))
            {
                service.SessionState.Add(key, value);
            }
            else
            {
                service.SessionState[key] = value;
            }
        }

        public static T Get<T>(this ISessionStateService service, string key)
        {
            if (service.SessionState.ContainsKey(key))
            {
                return (T)service.SessionState[key];
            }
            else
            {
                return default(T);
            }
        } 

        public static void Remove(this ISessionStateService service, string key)
        {
            if (service.SessionState.ContainsKey(key))
            {
                service.SessionState.Remove(key);
            }
        }
    }
}
