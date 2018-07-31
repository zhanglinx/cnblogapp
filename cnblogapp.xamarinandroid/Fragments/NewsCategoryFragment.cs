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
using cnblogapp.xamarinandroid.ViewModels;
using cnblogapp.xamarinandroid.Views;

namespace cnblogapp.xamarinandroid.Fragments
{
    public class NewsCategoryFragment : BaseFragment,IOnLoadMoreListener,SwipeRefreshLayout.IOnRefreshListener,INewsView,View.IOnClickListener
    {
        private int position;
        private NewsPresenter newsPresenter;
        private SwipeRefreshLayout srlBlog;
        private RecyclerView recyclerview_blog;
        private NewsAdapter adapter;
        private LoadMoreWrapper loadMoreWrapper;
        private int pageIndex = 1, pageSize = 10;
        private View emptyView, failView;
        private DateTime refreshTime;
        protected override int GetLayoutId() => Resource.Layout.fragment_category_item;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");
            // Create your fragment here
        }

        protected override void InitView()
        {
            srlBlog = rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.srl_blog);
            recyclerview_blog = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerview_blog);
            srlBlog.SetColorSchemeResources(Resource.Color.colorPrimary);
            recyclerview_blog.SetLayoutManager(new LinearLayoutManager(this.Activity));

            newsPresenter = new NewsPresenter(this);
            adapter = new NewsAdapter();
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
                await newsPresenter.GetLocalNews(pageSize, position);
            });
        }
        public static NewsCategoryFragment GetFragment(int position)
        {
            NewsCategoryFragment fragment = new NewsCategoryFragment();
            Bundle b = new Bundle();
            b.PutInt("position", position);
            fragment.Arguments = b;
            return fragment;
        }

        public void GetServiceNewsSuccess(List<NewsModel> newsList)
        {
            refreshTime = DateTime.Now;
            recyclerview_blog.Post(() =>
            {
                if (!srlBlog.Enabled)
                {
                    srlBlog.Enabled = true;
                }
                if (srlBlog.Refreshing)
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
                    adapter.SetNewData(newsList);
                    loadMoreWrapper.NotifyAddData();
                    pageIndex++;
                }
                else
                {
                    if (newsList.Count > 0)
                    {
                        adapter.AddData(newsList);
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

        public void GetServiceNewsFail(string error)
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

        public void GetLocalNewsSuccess(List<NewsModel> newsList)
        {
            recyclerview_blog.Post(() =>
            {
                if (newsList.Count > 0)
                {
                    adapter.SetNewData(newsList);
                    loadMoreWrapper.NotifyAddData();
                }
            });
        }

        public async void OnRefresh()
        {
            pageIndex = 1;
            srlBlog.Refreshing = true;
            await newsPresenter.GetServiceNews(pageIndex, pageSize, position);
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
                await newsPresenter.GetServiceNews(pageIndex, pageSize, position);
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
        }
    }
}