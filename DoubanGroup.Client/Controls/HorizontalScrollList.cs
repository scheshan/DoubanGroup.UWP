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
    [TemplatePart(Name = "ItemsPresenter", Type = typeof(ItemsPresenter))]
    public sealed class HorizontalScrollList : ItemsControl
    {
        private Button prevBtn = null;

        private Button nextBtn = null;

        private ItemsPresenter itemsPresenter = null;

        private Storyboard sb_scroll = null;

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

        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(HorizontalScrollList), new PropertyMetadata(0));

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
            itemsPresenter = this.GetTemplateChild("ItemsPresenter") as ItemsPresenter;
            sb_scroll = (this.GetTemplateChild("container") as FrameworkElement).Resources["scroll"] as Storyboard;

            itemsPresenter.SizeChanged += ItemsPresenter_SizeChanged;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeTransform(-300);
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeTransform(300);
        }

        private void ChangeTransform(double offset)
        {
            sb_scroll.Stop();

            var scrollableWidth = itemsPresenter.ActualWidth;
            var viewportWidth = this.ActualWidth;

            var current = ((TranslateTransform)itemsPresenter.RenderTransform);
            var target = current.X + offset;

            if (target > 0)
            {
                target = 0;
            }
            else if (viewportWidth - target > scrollableWidth)
            {
                target = viewportWidth - scrollableWidth;
            }

            var anamination = (DoubleAnimation)sb_scroll.Children[0];
            anamination.To = target;

            sb_scroll.Begin();
        }

        private void ItemsPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }

    public class HorizontalScrollItem : ContentControl
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
