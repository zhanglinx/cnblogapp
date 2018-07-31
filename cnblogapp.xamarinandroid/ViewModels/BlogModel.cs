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
    public class BlogModel
    {
        [PrimaryKey, Indexed]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BlogApp { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public int ViewCount { get; set; }

        public int CommentCount { get; set; }
        public int Diggcount { get; set; }
        public DateTime PostDate { get; set; }
        public bool MySelf { get; set; }

        public int CategoryId { get; set; }
    }
}