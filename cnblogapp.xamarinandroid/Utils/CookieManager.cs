using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace cnblogapp.xamarinandroid.Utils
{
    public class CookieUtil
    {
        public void GetCookies(string  webCookies)
        {
            if (Java.Net.CookieManager.Default == null || string.IsNullOrWhiteSpace(webCookies))
                return;
            CookieManager cookieManager = CookieManager.Instance;
            string cookies = cookieManager.GetCookie("https://www.cnblogs.com/");
            System.Diagnostics.Debug.Write(cookies);
        }
    }
}