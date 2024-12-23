using System;
using System.Globalization;
using System.Windows.Data;

namespace WindowsApp
{
    public class NatureFinalBalanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // بررسی مقدار و تبدیل به رشته متناظر
            if (value is int)
            {
                int balance = (int)value;
                switch (balance)
                {
                    case 0:
                        return "بی حساب";
                    case 1:
                        return "بدهکار";
                    case 2:
                        return "بستانکار";
                    default:
                        return "مقدار نامشخص";
                }
            }
            return "مقدار نامشخص";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // بازگشت مقدار به عدد برای ویرایش (در صورت نیاز)
            return null;
        }
    }
}
