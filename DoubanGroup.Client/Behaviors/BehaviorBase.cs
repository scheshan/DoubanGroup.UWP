using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DoubanGroup.Client.Behaviors
{
    public abstract class BehaviorBase : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }

        public virtual void Attach(DependencyObject associatedObject)
        {
            this.AssociatedObject = associatedObject;
        }

        public virtual void Detach()
        {

        }
    }
}
