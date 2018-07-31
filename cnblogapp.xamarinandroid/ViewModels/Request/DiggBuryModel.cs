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
    public class DiggBuryModel
    {
        public string blogApp { get; set; }
        public int postId { get; set; }
        public string voteType { get; set; }
        public bool isAbandoned { get; set; }
        public DiggBuryModel(string  blogApp,int postId)
        {
            //var model = new DiddBuryModel();
            this.blogApp = blogApp;
            this.postId = postId;
            this.voteType = "Digg";
            this.isAbandoned = false;
        }
        public DiggBuryModel(int postId,string blogApp)
        {
            this.blogApp = blogApp;
            this.postId = postId;
            this.voteType = "Bury";
            this.isAbandoned = false;
        }
    }
}