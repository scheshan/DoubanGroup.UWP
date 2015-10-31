using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Commands
{
    public class ViewUserCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var user = parameter as User;

            this.NavigationService.Navigate("UserDetail", user.ID);
        }
    }
}
