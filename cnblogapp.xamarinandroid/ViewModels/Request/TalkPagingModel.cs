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
    public class TalkPagingModel
    {
        public string Tag { get; set; }
        public string IngListType { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}