﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cnblogapp.xamarinandroid.ViewModels
{
    public class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public bool IsIdentityUser { get; set; }
        public DateTime RefreshTime
        {
            get; set;
        }
        public bool IsExpire
        {
            get
            {
                return DateTime.Now > RefreshTime.AddSeconds(expires_in);
            }
        }
    }
}