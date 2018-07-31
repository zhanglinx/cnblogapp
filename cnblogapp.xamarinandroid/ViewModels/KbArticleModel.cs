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
    public class KbArticleModel
    {
        [PrimaryKey, Indexed]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public int ViewCount { get; set; }
        public int Diggcount { get; set; }
        public DateTime DateAdded { get; set; }
        public string Content { get; set; }
    }
}