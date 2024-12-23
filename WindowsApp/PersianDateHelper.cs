using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApp
{
    public class PersianDateHelper
    {
        private static PersianCalendar persianCalendar = new PersianCalendar();

        public static string ToPersianDate(DateTime dateTime)
        {
            return persianCalendar.GetYear(dateTime) + "/" +
                   persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        }

        public static DateTime ToGregorianDate(string persianDate)
        {
            var dateParts = persianDate.Split('/');
            int year = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int day = int.Parse(dateParts[2]);

            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }
}
