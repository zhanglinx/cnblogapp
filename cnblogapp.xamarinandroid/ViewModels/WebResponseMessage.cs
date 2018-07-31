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

namespace cnblogapp.xamarinandroid.ViewModels
{
    public class WebResponseMessage
    {
        public object Data { get; set; }
        public int Id { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}