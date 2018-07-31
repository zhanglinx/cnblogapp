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
    public class KbArticleFragment : BaseFragment,IOnLoadMoreListener,SwipeRefreshLayout.IOnRefreshListener,IKbArticleView,View.IOnClickListener
    {
        private int position;
        private KbArticlePresenter kbArticlePresenter;
        private SwipeRefreshLayout srlBlog;
        private RecyclerView recyclerview_blog;
        private KbArticleAdapter adapter;
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
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false)            
            srlBlog = rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.srl_blog);
            recyclerview_blog = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerview_blog);
            srlBlog.SetColorSchemeResources(Resource.Color.colorPrimary);
            recyclerview_blog.SetLayoutManager(new LinearLayoutManager(this.Activity));

            kbArticlePresenter = new  KbArticlePresenter(this);
            adapter = new KbArticleAdapter();
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
                await kbArticlePresenter.GetLocalKbArticleListAsync(pageSize);
            });
        }
        public static KbArticleFragment GetFragment(int position)
        {
            KbArticleFragment fragment = new KbArticleFragment();
            Bundle b = new Bundle();
            b.PutInt("position", position);
            fragment.Arguments = b;
            return fragment;
        }

        public void GetServiceKbArticleSuccess(List<KbArticleModel> list)
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

        public void GetServiceKbArticleFail(string error)
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

        public void GetLocalKbArticleSuccess(List<KbArticleModel> list)
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

        public async void OnRefresh()
        {
            pageIndex = 1;
            srlBlog.Refreshing = true;
            await kbArticlePresenter.GetServiceKbArticleListAsync(pageIndex, pageSize);
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
                await kbArticlePresenter.GetServiceKbArticleListAsync(pageIndex,pageSize);
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