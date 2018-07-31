using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cnblogapp.xamarinandroid.Utils
{
    public  static class DatetimeUtil
    {
        public static DateTime StringToDateTime(string  str)
        {
            if (str.Length == 5)
            {
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                return DateTime.Parse(currentDate + " " + str);
            }
            else if (str.Length == 10)
            {
                string currentYear = DateTime.Now.Year.ToString();
                return DateTime.Parse(currentYear + "-" + str);
            }
            else
            {
                try
                {
                    return DateTime.Parse(str);
                }
                catch (Exception ex)
                {
                    throw new FormatException("datetime parse error");
                }
            }
        }
        public static string ToCommonString(this DateTime dt)
        {
             TimeSpan ts = DateTime.Now.Subtract(dt);
            if (ts.Days > 0)
            {
                int month = (DateTime.Now.Year - dt.Year) * 12 + DateTime.Now.Month - dt.Month;
                if (month >= 12)
                {
                    return $"{month / 12}年前";
                }
                else if (month > 0)
                {
                    return $"{month}月前";
                }
                else
                {
                    return $"{ts.Days}天前";
                }
            }
            else
            {
                if (ts.Hours > 0)
                {
                    return $"{ts.Hours}小时前";
                }
                else
                {
                    if (ts.Minutes > 0)
                    {
                        return $"{ts.Minutes}分钟前";
                    }
                    if (ts.Seconds > 30)
                    {
                        return $"{ts.Seconds}秒前";
                    }
                    else
                    {
                        return "刚刚";
                    }
                }
            }
        }
    }
}