using MyToolkit.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace DoubanGroup.Client.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TestPage : MtPage
    {
        public TestPage()
        {
            this.InitializeComponent();

            this.Loaded += TestPage_Loaded;
        }

        private void TestPage_Loaded(object sender, RoutedEventArgs e)
        {
            var imageList = new List<Models.ImageItem>
            {
                new Models.ImageItem { Source = "http://img3.douban.com/view/photo/photo/public/p1180907094.jpg", Height = 333, Width = 500, Description = "小时候的梦想是成为画家，后来偶然的机会每周一次这样正统地学了一年的素描水粉。周末闲时会在家画油画。希望自己的画能挂满一屋子。（大多数画非原创）" },
                new Models.ImageItem { Source = "http://img3.douban.com/view/photo/photo/public/p1180907094.jpg", Height = 333, Width = 500, Description = "小时候的梦想是成为画家，后来偶然的机会每周一次这样正统地学了一年的素描水粉。周末闲时会在家画油画。希望自己的画能挂满一屋子。（大多数画非原创）" },
                new Models.ImageItem { Source = "http://img3.douban.com/view/photo/photo/public/p1180907094.jpg", Height = 333, Width = 500, Description = "小时候的梦想是成为画家，后来偶然的机会每周一次这样正统地学了一年的素描水粉。周末闲时会在家画油画。希望自己的画能挂满一屋子。（大多数画非原创）" },
                new Models.ImageItem { Source = "http://img3.douban.com/view/photo/photo/public/p1180907094.jpg", Height = 333, Width = 500, Description = "小时候的梦想是成为画家，后来偶然的机会每周一次这样正统地学了一年的素描水粉。周末闲时会在家画油画。希望自己的画能挂满一屋子。（大多数画非原创）" },
                new Models.ImageItem { Source = "http://img3.douban.com/view/photo/photo/public/p1180907094.jpg", Height = 333, Width = 500, Description = "小时候的梦想是成为画家，后来偶然的机会每周一次这样正统地学了一年的素描水粉。周末闲时会在家画油画。希望自己的画能挂满一屋子。（大多数画非原创）" },
                new Models.ImageItem { Source = "http://img3.douban.com/view/photo/photo/public/p1180907094.jpg", Height = 333, Width = 500, Description = "小时候的梦想是成为画家，后来偶然的机会每周一次这样正统地学了一年的素描水粉。周末闲时会在家画油画。希望自己的画能挂满一屋子。（大多数画非原创）" }
            };

            new ViewImagePage(imageList, imageList[0]).Show();
        }
    }
}
