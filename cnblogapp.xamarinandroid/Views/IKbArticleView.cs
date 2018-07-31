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
using cnblogapp.xamarinandroid.ViewModels;
namespace cnblogapp.xamarinandroid.Views
{
    public interface IKbArticleView
    {
        void GetServiceKbArticleSuccess(List<KbArticleModel> list);
        void GetServiceKbArticleFail(string  error);
        void GetLocalKbArticleSuccess(List<KbArticleModel> list);
    }
}