using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace DoubanGroup.Client.Models
{
    public class IncrementalLoadingList<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private Func<uint, Task<IEnumerable<T>>> func;

        public bool HasMoreItems
        {
            get; private set;
        }

        public IncrementalLoadingList(Func<uint, Task<IEnumerable<T>>> _func)
        {
            func = _func;
            this.HasMoreItems = true;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(ct => InnerLoadMoreItems(count));
        }

        public void NoMoreItems()
        {
            this.HasMoreItems = false;
        }

        private async Task<LoadMoreItemsResult> InnerLoadMoreItems(uint count)
        {
            var list = await func.Invoke(count);

            if (list == null)
            {
                return new LoadMoreItemsResult { Count = 0 };
            }

            foreach (var item in list)
            {
                this.Add(item);
            }

            return new LoadMoreItemsResult { Count = (uint)list.Count() };
        }
    }
}
