using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class RefreshableViewModel<T> : ViewModelBase
    {
        public Models.IncrementalLoadingList<T> ItemList { get; private set; }

        public Func<int, int, Task<IEnumerable<T>>> LoadAction { get; private set; }

        public int FetchCount { get; private set; }

        public RefreshableViewModel(Func<int, int, Task<IEnumerable<T>>> loadAction, int fetchCount)
        {
            this.LoadAction = loadAction;
            this.FetchCount = fetchCount;
            this.ItemList = new Models.IncrementalLoadingList<T>(this.LoadData);
        }

        private async Task<IEnumerable<T>> LoadData(uint count)
        {
            var items = await this.RunTaskAsync(this.LoadAction.Invoke(this.ItemList.Count, this.FetchCount));

            if (items == null || items.Count() < this.FetchCount)
            {
                this.ItemList.NoMore();
            }

            return items;
        }

        private DelegateCommand _refreshCommand;

        public DelegateCommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new DelegateCommand(Refresh);
                }
                return _refreshCommand;
            }
        }

        private void Refresh()
        {
            this.ItemList.Clear();
            this.ItemList.HasMore();
        }
    }
}
