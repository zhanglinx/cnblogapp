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
using SQLite;

namespace cnblogapp.xamarinandroid.ViewModels
{
    public class NewsModel
    {
        [PrimaryKey, Indexed]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int TopicId { get; set; }
        public string TopicIcon { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public int DiggCount { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsHot { get; set; }
        public bool IsDigg { get; set; }
        public string Body { get; set; }
    }
}