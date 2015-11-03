using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DoubanGroup.Client.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static T FindByName<T>(this DependencyObject obj, string name)
            where T : class
        {
            if (obj.GetType() == typeof(T) && (obj as FrameworkElement)?.Name == name)
            {
                return obj as T;
            }

            var childrenCount = VisualTreeHelper.GetChildrenCount(obj);

            if (childrenCount == 0)
            {
                return default(T);
            }

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                var t = FindByName<T>(child, name);

                if (t != null)
                {
                    return t;
                }
            }

            return default(T);
        }
    }
}
