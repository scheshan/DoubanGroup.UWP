using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace DoubanGroup.Client.Controls
{
    [TemplatePart(Name = "PrevButton", Type = typeof(Button))]
    [TemplatePart(Name = "NextButton", Type = typeof(Button))]
    [TemplatePart(Name = "ScrollViewer", Type = typeof(ScrollViewer))]
    public sealed class HorizontalScrollList : ItemsControl
    {
        private Button prevBtn = null;

        private Button nextBtn = null;

        private ScrollViewer scrollViewer = null;

        private bool isScrolling = false;

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(HorizontalScrollList), new PropertyMetadata(null));

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(HorizontalScrollList), new PropertyMetadata(null));

        public HorizontalScrollList()
        {
            this.DefaultStyleKey = typeof(HorizontalScrollList);
        }        

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new HorizontalScrollItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var container = element as HorizontalScrollItem;
            container?.SetData(item);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            prevBtn = this.GetTemplateChild("PrevButton") as Button;
            nextBtn = this.GetTemplateChild("NextButton") as Button;
            prevBtn.Click += PrevBtn_Click;
            nextBtn.Click += NextBtn_Click;
            scrollViewer = this.GetTemplateChild("ScrollViewer") as ScrollViewer;

            scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollViewer.HorizontalOffset < 2.3)
            {
                prevBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                prevBtn.Visibility = Visibility.Visible;
            }

            if (scrollViewer.ScrollableWidth - scrollViewer.HorizontalOffset < 0.3)
            {
                nextBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                nextBtn.Visibility = Visibility.Visible;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DoScroll(3);
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DoScroll(-3);
        }

        private void DoScroll(double offset)
        {
            if (isScrolling)
            {
                return;
            }

            isScrolling = true;

            double target = scrollViewer.HorizontalOffset + offset;

            if (target < 0)
            {
                target = 0;
            }
            if (target > scrollViewer.ScrollableWidth)
            {
                target = scrollViewer.ScrollableWidth;
            }

            scrollViewer.ChangeView(target, null, null, false);

            isScrolling = false;
        }
    }

    public class HorizontalScrollItem : ListViewItem
    {
        public HorizontalScrollItem()
        {

        }

        public void SetData(object data)
        {
            this.Content = data;
            this.DataContext = data;
        }
    }
}
