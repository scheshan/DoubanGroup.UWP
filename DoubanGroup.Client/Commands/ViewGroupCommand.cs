using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Commands
{
    public class ViewGroupCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var group = parameter as Group;

            this.NavigationService.Navigate("GroupDetail", group.ID);
        }
    }
}
