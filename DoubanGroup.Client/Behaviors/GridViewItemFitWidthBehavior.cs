using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DoubanGroup.Client.Behaviors
{
    public class GridViewItemFitWidthBehavior : BehaviorBase
    {
        public double MinWidth { get; set; } = 200;

        public double MaxWidth { get; set; } = 300;

        public override void Attach(DependencyObject associatedObject)
        {
            base.Attach(associatedObject);

            var gridView = this.AssociatedObject as GridView;
            if (gridView != null)
            {
                gridView.SizeChanged += GridView_SizeChanged;
            }
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = e.NewSize.Width;

            var gridView = sender as GridView;
            var container = gridView.ItemsPanelRoot as ItemsWrapGrid;

            int columns = 1;
            double itemWidth = width / columns;

            while (itemWidth > MaxWidth)
            {
                columns++;
                itemWidth = width / columns;

                if (columns >= 10)
                {
                    break;
                }
            }

            container.ItemWidth = itemWidth;
        }
    }
}
