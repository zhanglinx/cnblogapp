using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class NewsPresenter
    {
        private INewsView newsView;
        public NewsPresenter(INewsView newView)
        {
            this.newsView = newView;
        }
        public async Task GetServiceNews(int pageIndex, int pageSize, int position)
        {
            string url = string.Empty;
            if (position == 0)
            {
                url = string.Format(Constant.NEWS_NEWEST_LIST, pageIndex, pageSize);
            }
            else if (position == 1)
            {
                url = string.Format(Constant.NEWS_HOT_CURRENT_WEEK_LIST, pageIndex, pageSize);
            }
            else
            {
                url = string.Format(Constant.NEWS_HOT_CURRENT_WEEK_LIST, pageIndex, pageSize);
            }
            await HttpClientUtil.GetAsync<List<NewsModel>>(url, null,
            async (list) =>
            {
                newsView.GetServiceNewsSuccess(list);
                if (position == 1)
                {
                    list.ForEach(f => f.IsHot = true);
                }
                else if (position == 2)
                {
                    list.ForEach(f => f.IsDigg = true);
                }
                await SqliteDatabase.Instance().UpdateNewsList(list);
                },
            (error) =>
            {
                newsView.GetServiceNewsFail(error);
            });
        }

        public async Task GetLocalNews(int pageSize,int position)
        {
            List<NewsModel> list = new List<NewsModel>();
            if (position == 0)
            {
                list = await SqliteDatabase.Instance().GetNewsList(pageSize);
            }
            else if (position == 1)
            {
                list = await SqliteDatabase.Instance().GetNewsHotList(pageSize);
            }
            else
            {
                list = await SqliteDatabase.Instance().GetNewsDiggList(pageSize);
            }
            newsView.GetLocalNewsSuccess(list);
        }
    }
}