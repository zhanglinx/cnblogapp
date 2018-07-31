using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Adapter.RecyclerViewAdapter;
using cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter;
using cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter.LoadMore;
using cnblogapp.xamarinandroid.Presenter;
using cnblogapp.xamarinandroid.Shared;
using cnblogapp.xamarinandroid.ViewModels;
using cnblogapp.xamarinandroid.Views;

namespace cnblogapp.xamarinandroid.Fragments
{
    public class TalkCategoryFragment : BaseFragment,IOnLoadMoreListener,SwipeRefreshLayout.IOnRefreshListener,ITalkView,View.IOnClickListener
    {
        private int position;
        private TalkPresenter  talkPresenter;
        private SwipeRefreshLayout srlBlog;
        private LinearLayout ll_unlogin;
        private RecyclerView recyclerview_blog;
        private TalkAdapter adapter;
        private LoadMoreWrapper loadMoreWrapper;
        private int pageIndex = 1, pageSize = 10;
        private View emptyView, failView;
        private DateTime refreshTime;
        protected override int GetLayoutId() => Resource.Layout.fragment_category_item_login;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");
            // Create your fragment here
        }

        protected override   void   InitView()
        {
            ll_unlogin = rootView.FindViewById<LinearLayout>(Resource.Id.ll_unlogin);
            srlBlog = rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.srl_blog);
            if (!IsLogged()&&position>1)
            {
                ll_unlogin.Visibility = ViewStates.Visible;
                srlBlog.Visibility = ViewStates.Gone;
                ll_unlogin.SetOnClickListener(this);
            }
            else
            {
                ll_unlogin.Visibility = ViewStates.Gone;
                srlBlog.Visibility = ViewStates.Visible;

                recyclerview_blog = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerview_blog);
                srlBlog.SetColorSchemeResources(Resource.Color.colorPrimary);
                recyclerview_blog.SetLayoutManager(new LinearLayoutManager(this.Activity));
                talkPresenter = new TalkPresenter(this);
                adapter = new TalkAdapter();
                loadMoreWrapper = new LoadMoreWrapper(adapter);
                recyclerview_blog.SetAdapter(loadMoreWrapper);

                emptyView = this.Activity.LayoutInflater.Inflate(Resource.Layout.empty, (ViewGroup)recyclerview_blog.Parent, false);
                failView = this.Activity.LayoutInflater.Inflate(Resource.Layout.fail, (ViewGroup)recyclerview_blog.Parent, false);

                srlBlog.SetOnRefreshListener(this);
                failView.SetOnClickListener(this);
                emptyView.SetOnClickListener(this);
                loadMoreWrapper.SetOnLoadMoreListener(this);

                recyclerview_blog.Post(async () =>
                {
                    await talkPresenter.GetLocalTalkListAsync(pageSize, position);
                });
            }
        }
        public static TalkCategoryFragment GetFragment(int position)
        {
            TalkCategoryFragment fragment = new TalkCategoryFragment();
            Bundle b = new Bundle();
            b.PutInt("position", position);
            fragment.Arguments = b;
            return fragment;
        }

        public void GetServiceTalkSuccess(List<TalkModel> list)
        {
            refreshTime = DateTime.Now;
            recyclerview_blog.Post(() =>
            {
                if(!srlBlog.Enabled)
                {
                    srlBlog.Enabled = true;
                }
                if(srlBlog.Refreshing)
                {
                    srlBlog.Refreshing = false;
                }
                if (pageIndex == 1)
                {
                    if (srlBlog.Refreshing)
                    {
                        srlBlog.Post(() =>
                        {
                            srlBlog.Refreshing = false;
                        });
                    }
                    adapter.SetNewData(list);
                    loadMoreWrapper.NotifyAddData();
                    pageIndex++;
                }
                else
                {
                    if (list.Count > 0)
                    {
                        adapter.AddData(list);
                        loadMoreWrapper.LoadMoreComplete();
                        pageIndex++;
                    }
                    else
                    {
                        loadMoreWrapper.LoadMoreEndEmpty();
                    }
                }
            });
        }

        public void GetServiceTalkFail(string error)
        {
            recyclerview_blog.Post(() =>
            {
                if (srlBlog.Refreshing)
                {
                    srlBlog.Refreshing = false;
                }
                if (pageIndex > 1)
                {
                    //loadMoreWrapper.loadmore 
                }
            });
        }

        public void GetLocalTalkSuccess(List<TalkModel> list)
        {
            recyclerview_blog.Post(() =>
            {
                if (list.Count > 0)
                {
                    adapter.SetNewData(list);
                    loadMoreWrapper.NotifyAddData();
                }
            });
        }
        //关注和我的 需要登录auth授权的
        private bool IsLogged()
        {
            return  UserInfoManager.GetInstance().IsAuthLogin();
        }
        public async void OnRefresh()
        {
            if (!IsLogged() && position > 1)
                return;
            else
            { 
                pageIndex = 1;
                srlBlog.Refreshing = true;
                await talkPresenter.GetServiceTalkListAsync(pageIndex, pageSize, position);
            }
        }
        public void Refresh()
        {
            if (refreshTime != null && refreshTime.AddMinutes(5) < DateTime.Now)
            {
                OnRefresh();
            }
        }

        public void OnLoadMoreRequested()
        {
            recyclerview_blog.Post(async () =>
            {
                srlBlog.Enabled = false;
                await  talkPresenter.GetServiceTalkListAsync(pageIndex, pageSize, position);
            });
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Layout.empty)
            {
                OnRefresh();
            }
            if (v.Id == Resource.Layout.fail)
            {
                OnRefresh();
            }
            if (v.Id == Resource.Id.ll_unlogin)
            {

            }
        }
    }
}