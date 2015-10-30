using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DoubanGroup.Client.Controls
{
    public class CommentListStyleSelector : StyleSelector
    {
        public Style TopicStyle { get; set; }

        public Style NoTopicStyle { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var vm = item as ViewModels.CommentListViewModel;

            if (vm?.ShowTopic == true)
            {
                return this.TopicStyle;
            }

            return this.NoTopicStyle;
        }
    }
}
