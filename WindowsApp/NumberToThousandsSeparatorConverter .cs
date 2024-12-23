using System;
using System.Globalization;
using System.Windows.Data;

namespace WindowsApp
{

    public class NumberToThousandsSeparatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int || value is long || value is double || value is decimal)
            {
                // تبدیل عدد به رشته با جداکننده سه‌رقمی
                return string.Format(CultureInfo.InvariantCulture, "{0:N0}", value);
            }
            return value; // در صورتی که مقدار معتبر نباشد
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // در صورتی که نیاز به تبدیل به عقب باشد
            return value;
        }
    }
    }