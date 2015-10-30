using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media.Imaging;

namespace DoubanGroup.Client.Behaviors
{
    public class TopicContentBehavior : BehaviorBase
    {
        public Topic Topic
        {
            get { return (Topic)GetValue(TopicProperty); }
            set { SetValue(TopicProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Topic.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopicProperty =
            DependencyProperty.Register("Topic", typeof(Topic), typeof(TopicContentBehavior), new PropertyMetadata(null, OnTopicChanged));

        private static void OnTopicChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as TopicContentBehavior;
            behavior?.SetContent();
        }

        private void SetContent()
        {
            var tb = this.AssociatedObject as RichTextBlock;
            if (tb == null)
            {
                return;
            }

            tb.Blocks.Clear();

            if (this.Topic == null)
            {
                return;
            }

            string[] contentArray = this.Topic.Content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var content in contentArray)
            {
                var para = new Paragraph();

                var inline = this.CreateContentInline(content);

                para.Inlines.Add(inline);

                tb.Blocks.Add(para);
            }
        }

        private Inline CreateContentInline(string content)
        {
            Regex regex = new Regex(@"<图片(\d+?)>");
            var m = regex.Match(content);

            if (m.Success)
            {
                int photoIndex = Convert.ToInt32(m.Groups[1].Value);

                if (this.Topic.Photos.Count >= photoIndex)
                {
                    var photo = this.Topic.Photos[photoIndex - 1];
                    var container = new InlineUIContainer();

                    var img = new Image();

                    BitmapImage bi = new BitmapImage();
                    bi.UriSource = new Uri(photo.Alt);
                    img.Source = bi;
                    img.Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill;
                    img.MaxWidth = photo.Size.Width;
                    img.MaxHeight = photo.Size.Height;
                    img.Margin = new Thickness(0, 0, 0, 10);

                    container.Child = img;

                    return container;
                }
                else
                {
                    return new Run() { Text = m.Value };
                }
            }
            else
            {
                return new Run() { Text = content };
            }
        }
    }
}
