using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DoubanGroup.Client.Service
{
    public class FileCacheService : ICacheService
    {
        public StorageFolder CacheFolder
        {
            get
            {
                return ApplicationData.Current.LocalCacheFolder;
            }
        }

        public async Task<T> Get<T>(string key)
        {
            string fileName = this.GetFileName(key);
            var file = await this.CacheFolder.GetFileAsync(fileName);

            if (file == null)
            {
                return default(T);
            }

            string json = await FileIO.ReadTextAsync(file);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task Remove(string key)
        {
            string fileName = this.GetFileName(key);

            var file = await this.CacheFolder.GetFileAsync(fileName);

            if (file != null)
            {
                await file.DeleteAsync();
            }
        }

        public async Task Set(string key, object data)
        {
            string fileName = this.GetFileName(key);
            var file = await this.CacheFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            string json = JsonConvert.SerializeObject(data);

            await FileIO.WriteTextAsync(file, json);
        }

        private string GetFileName(string key)
        {
            return $"cache_{key}.json";
        }
    }
}
