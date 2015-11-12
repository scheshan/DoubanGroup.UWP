using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.DesignData
{
    public class RefreshableData<T>
    {
        public List<T> ItemList { get; private set; }

        public RefreshableData()
        {
            this.ItemList = new List<T>();
        }
    }
}
