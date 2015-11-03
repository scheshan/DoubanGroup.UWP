using DoubanGroup.Core.Api.Entity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Events
{
    public class QuitGroupEvent : PubSubEvent<Group>
    {
    }
}
