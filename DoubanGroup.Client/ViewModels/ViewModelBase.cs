using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public abstract class ViewModelBase : Prism.Windows.Mvvm.ViewModelBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { this.SetProperty(ref _isLoading, value); }
        }

    }
}
