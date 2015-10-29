using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DoubanGroup.Client.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dt = (DateTime)value;

            var now = DateTime.Now;

            if (dt.Year != now.Year)
            {
                return dt.ToString("yyyy-MM-dd");
            }

            var ts = DateTime.Now - dt;
            
            if (ts.TotalDays > 30)
            {
                return dt.ToString("MM-dd");
            }

            if (ts.TotalDays > 1)
            {
                return string.Format("{0}天前", ts.Days.ToString());
            }

            if (ts.TotalHours > 1)
            {
                return string.Format("{0}小时前", ts.Hours.ToString());
            }

            if (ts.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", ts.Minutes.ToString());
            }

            return "刚刚";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
