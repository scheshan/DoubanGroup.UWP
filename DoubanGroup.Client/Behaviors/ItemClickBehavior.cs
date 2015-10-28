using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DoubanGroup.Client.Behaviors
{
    public class ItemClickBehavior : BehaviorBase
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ItemClickBehavior), new PropertyMetadata(null));

        public override void Attach(DependencyObject associatedObject)
        {
            base.Attach(associatedObject);

            var gridView = associatedObject as ListViewBase;

            if (gridView != null)
            {
                gridView.ItemClick += GridView_ItemClick;
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.Command != null)
            {
                this.Command.Execute(e.ClickedItem);
            }
        }

        public override void Detach()
        {
            var gridView = this.AssociatedObject as GridView;
            if (gridView != null)
            {
                gridView.ItemClick -= GridView_ItemClick;
            }

            base.Detach();
        }
    }
}
