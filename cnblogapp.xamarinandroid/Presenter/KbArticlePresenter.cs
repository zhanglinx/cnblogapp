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
using cnblogapp.xamarinandroid.Db;
using cnblogapp.xamarinandroid.Utils;
using cnblogapp.xamarinandroid.ViewModels;
using cnblogapp.xamarinandroid.Views;

namespace cnblogapp.xamarinandroid.Presenter
{
    public class KbArticlePresenter
    {
        private IKbArticleView kbArticleView;
        public KbArticlePresenter(IKbArticleView  kbArticleView)
        {
            this.kbArticleView = kbArticleView;
        }
        public async System.Threading.Tasks.Task GetServiceKbArticleListAsync(int pageIndex,int pageSize)
        {
            string url = string.Format(Constant.KBARTICLE_LIST,pageIndex,pageSize);
            await HttpClientUtil.GetAsync<List<KbArticleModel>>(url, null,
                async (list) =>
                {
                    kbArticleView.GetServiceKbArticleSuccess(list);
                    await SqliteDatabase.Instance().UpdateKbArticleList(list);
                },
                (error) =>
                {
                    kbArticleView.GetServiceKbArticleFail(error);
                });
        }
         
        public async System.Threading.Tasks.Task GetLocalKbArticleListAsync(int  pageSize)
        {
            var  list =  await SqliteDatabase.Instance().GetKbArticleList(pageSize);
            kbArticleView.GetLocalKbArticleSuccess(list);
        }
    }
}