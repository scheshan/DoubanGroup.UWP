using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoubanGroup.Client.Commands
{
    public class ViewTopicCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var topic = parameter as Topic;

            this.NavigationService.Navigate("TopicDetail", topic.ID);
        }
    }
}
