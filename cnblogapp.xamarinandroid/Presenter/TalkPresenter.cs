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
    public class TalkPresenter
    {
        private ITalkView talkView;
        public TalkPresenter(ITalkView view)
        {
            this.talkView = view;
        }
        public async Task GetServiceTalkListAsync(int pageIndex, int pageSize,int position)
        {
            TalkType type = TalkType.All;
            string talkType = "all";
            switch (position)
            {
                case 0:
                    type = TalkType.All;
                    talkType = "all";
                    break;
                case 1:
                    type = TalkType.RecentComment;
                    //talkType = "all";
                    break;
                case 2:
                    type = TalkType.All;
                    talkType = "all";
                    break;
                case 3:
                    type = TalkType.All;
                    talkType = "all";
                    break;
            }
            switch (type)
            {
                case TalkType.All:
                    string url = string.Format(Constant.TALK_ALL_LIST,talkType,pageIndex, pageSize);
                    await HttpClientUtil.GetAsync<List<TalkModel>>(url, null,
                        async (list) => {
                            talkView.GetServiceTalkSuccess(list);
                            list.ForEach(f => f.TalkType = type);
                            await SqliteDatabase.Instance().UpdateTalkList(list);
                        },
                        (error) => {
                            talkView.GetServiceTalkFail(error);
                        });
                    break;

                case TalkType.RecentComment:
                    string urlRecnetComment =string.Format(Constant.TAKL_RECENT_LIST,"RecentComment",pageIndex,30);
                    await HttpClientUtil.GetTalkRecentAsync(urlRecnetComment,
                        async (list) =>
                        {
                            talkView.GetServiceTalkSuccess(list);
                            list.ForEach(f => f.TalkType = type);
                            await SqliteDatabase.Instance().UpdateTalkList(list);
                            list.ForEach(f => f.TalkType = type);
                        },
                        (error) =>
                        {
                            talkView.GetServiceTalkFail(error);
                        });
                    break;
            }
        }

        public async Task GetLocalTalkListAsync(int pageSize,int position)
        {
            TalkType type = TalkType.All;
            switch (position)
            {
                case 0:
                    type = TalkType.All;
                    break;
                case 1:
                    type = TalkType.RecentComment;
                    break;
                case 2:
                    type = TalkType.All;
                    break;
                case 3:
                    type = TalkType.All;
                    break;
            }
            var  list = await SqliteDatabase.Instance().GetTalkList(pageSize,type);
            talkView.GetLocalTalkSuccess(list);
        }

    }
}