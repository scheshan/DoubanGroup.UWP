using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Service
{
    public interface ICacheService
    {
        Task Set(string key, object data);

        Task<T> Get<T>(string key);

        Task Remove(string key);
    }
}
