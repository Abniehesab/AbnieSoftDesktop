using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WindowsApp
{
    public class RowNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // چون 'value' در اینجا یک DataGridRow است، باید شماره ردیف را از این شیء دریافت کنیم.
            if (value is System.Windows.Controls.DataGridRow row)
            {
                // شماره ردیف از طریق Index به‌دست می‌آید
                var index = row.GetIndex() + 1;
                return index;
            }
            return 0; // در صورتی که مشکلی وجود داشته باشد، صفر بر می‌گردد
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
