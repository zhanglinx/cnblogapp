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
    public class  QuestionCategoryFragment : BaseFragment,IOnLoadMoreListener,SwipeRefreshLayout.IOnRefreshListener,IQuestionView,View.IOnClickListener
    {
        private int position;
        private bool isme;
        private QuestionPresenter   questionPresenter;
        private SwipeRefreshLayout srlBlog;
        private RecyclerView recyclerview_blog;
        private QuestionAdapter adapter;
        private LoadMoreWrapper loadMoreWrapper;
        private int pageIndex = 1, pageSize = 10;
        private View emptyView, failView;
        private DateTime refreshTime;
        protected override int GetLayoutId() => Resource.Layout.fragment_category_item;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");
            isme = Arguments.GetBoolean("isme");
            // Create your fragment here
        }

        protected override void InitView()
        {
            srlBlog = rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.srl_blog);
            recyclerview_blog = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerview_blog);
            srlBlog.SetColorSchemeResources(Resource.Color.colorPrimary);
            recyclerview_blog.SetLayoutManager(new LinearLayoutManager(this.Activity));

            questionPresenter = new QuestionPresenter(this);
            adapter = new QuestionAdapter();
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
                await questionPresenter.GetLocalQuestionListAsync(pageSize,GetQuestionType());
            });
        }
        private QuestionType GetQuestionType()
        {
            QuestionType questionType =QuestionType.最新;
            if (isme)
            {
                switch (position)
                {
                    case 0:
                        questionType = QuestionType.我的提问;
                        break;
                    case 1:
                        questionType = QuestionType.我的回答;
                        break;
                    case 2:
                        questionType = QuestionType.被采纳;
                        break;
                }
            }
            else
            {
                switch (position)
                {
                    case 0:
                        questionType = QuestionType.最新;
                        break;
                    case 1:
                        questionType = QuestionType.高分;
                        break;
                    case 2:
                        questionType = QuestionType.待解决;
                        break;
                    case 3:
                        questionType = QuestionType.已解决;
                        break;
                }
            }
            return questionType;
        }
        public static  QuestionCategoryFragment GetFragment(int position,bool isme)
        {
            QuestionCategoryFragment fragment = new QuestionCategoryFragment();
            Bundle b = new Bundle();
            b.PutInt("position", position);
            b.PutBoolean("isme", isme);
            fragment.Arguments = b;
            return fragment;
        }

        public void GetServiceQuestionSuccess(List<QuestionModel> list)
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

        public void GetServiceQuestionFail(string error)
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

        public void GetLocalQuestionSuccess(List<QuestionModel> list)
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
            await  questionPresenter.GetServiceQuestionListAsync(pageIndex, pageSize, GetQuestionType());
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
                await  questionPresenter.GetServiceQuestionListAsync(pageIndex, pageSize, GetQuestionType());
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