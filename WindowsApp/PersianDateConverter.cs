using System;
using System.Globalization;
using System.Windows.Data;
using System.Globalization;

namespace WindowsApp
{
    public class PersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                PersianDateTime persianDate = new PersianDateTime(dateTime);
                return persianDate.ToString("yyyy/MM/dd");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateStr)
            {
                try
                {
                    PersianDateTime persianDate = PersianDateTime.Parse(dateStr);
                    return persianDate.ToDateTime();
                }
                catch
                {
                    return DateTime.Now;
                }
            }
            return value;
        }
    }
}