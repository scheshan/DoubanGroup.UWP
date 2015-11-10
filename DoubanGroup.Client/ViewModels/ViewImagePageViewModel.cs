using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class ViewImagePageViewModel : DialogViewModelBase<bool>
    {
        public List<string> PhotoList { get; set; }

        public ViewImagePageViewModel()
            : base(typeof(Views.ViewImagePage))
        {
            this.PhotoList = new List<string>();

            for (var i = 0; i < 10; i++)
            {
                this.PhotoList.Add("http://img4.cache.netease.com/sports/2015/11/10/201511101012302da87_550.png");
            }
        }
    }
}
