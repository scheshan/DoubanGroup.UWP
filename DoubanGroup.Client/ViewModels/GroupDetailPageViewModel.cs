using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;

namespace DoubanGroup.Client.ViewModels
{
    public class GroupDetailPageViewModel : ViewModelBase
    {
        private Group _group;

        public Group Group
        {
            get { return _group; }
            set { this.SetProperty(ref _group, value); }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            this.Group = e.Parameter as Group;
        }
    }
}
