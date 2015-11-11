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
        private static readonly Regex regex_photo = new Regex(@"<图片(\d{1,})>+");

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
                Block block;

                if (regex_photo.IsMatch(content))
                {
                    block = this.CreateImageBlock(content);
                }
                else
                {
                    block = this.CreateTextBlock(content);
                }

                tb.Blocks.Add(block);
            }
        }

        private Block CreateTextBlock(string content)
        {
            var para = new Paragraph();

            var run = new Run() { Text = content };

            para.Inlines.Add(run);

            return para;
        }

        private Block CreateImageBlock(string content)
        {
            var para = new Paragraph();

            var matches = regex_photo.Matches(content);

            int lastIndex = 0;

            InlineUIContainer container = new InlineUIContainer();
            container.Child = new StackPanel();
            para.Inlines.Add(container);

            foreach (Match match in matches)
            {
                if (match.Index != lastIndex)
                {
                    string text = content.Substring(lastIndex, match.Index - lastIndex);
                    para.Inlines.Add(new Run() { Text = text });
                    para.Inlines.Add(new LineBreak());

                    container = new InlineUIContainer();
                    container.Child = new StackPanel();
                    para.Inlines.Add(container);
                }

                lastIndex = match.Index + match.Length;

                int photoIndex = Convert.ToInt32(match.Groups[1].Value);

                if (this.Topic.Photos.Count >= photoIndex)
                {
                    var photo = this.Topic.Photos[photoIndex - 1];

                    var img = new Image();
                    
                    img.Tapped += OnImageTapped;

                    BitmapImage bi = new BitmapImage();
                    bi.UriSource = new Uri(photo.Alt);
                    img.Source = bi;
                    img.Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill;
                    img.MaxWidth = photo.Size.Width;
                    img.MaxHeight = photo.Size.Height;
                    img.Margin = new Thickness(0, 10, 0, 0);

                    ((StackPanel)container.Child).Children.Add(img);
                }
            }

            return para;
        }

        private void OnImageTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var imageList = this.Topic.Photos.Select(t => new Models.ImageItem
            {
                Description = "",
                Height = t.Size.Height,
                Width = t.Size.Width,
                Title = t.Title,
                Source = t.Alt
            }).ToList();

            new Views.ViewImagePage(imageList).Show();
        }
    }
}
