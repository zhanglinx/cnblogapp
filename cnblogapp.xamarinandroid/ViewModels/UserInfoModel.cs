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
using SQLite;

namespace cnblogapp.xamarinandroid.ViewModels
{
    public class UserInfoModel
    {
        [PrimaryKey, Indexed]
        public Guid UserId { get; set; }
        public int SpaceUserId { get; set; }
        public int BlogId { get; set; }
        public string DisplayName { get; set; }
        public string Face { get; set; }
        public string Avatar { get; set; }
        public string Seniority { get; set; }
        public string BlogApp { get; set; }
        public int Score { get; set; }
    }
}