using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter.LoadMore;
using static Android.Support.V7.Widget.RecyclerView;

namespace cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter
{
    public class LoadMoreWrapper : RecyclerView.Adapter, View.IOnClickListener
    {
        LoadMoreView loadMoreView = new SimpleLoadMoreView();
        IOnLoadMoreListener onLoadMoreListener;
        private RecyclerView.Adapter innerAdapter;
        private const int ITEM_TYPE_LOADMORE = int.MaxValue - 10;
        private bool loadmoreing;
        private LayoutInflater layoutInflater;
        private bool isAutoLoadMore = true; //true 自动加载更多,false 需要点击加载更多
        public override int ItemCount => innerAdapter.ItemCount + GetLoadMoreViewCount();

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            if (holder.ItemViewType == ITEM_TYPE_LOADMORE)
            {
                holder = holder as BaseViewHolder;
                loadMoreView.Convert(holder as BaseViewHolder);
            }
            innerAdapter.OnBindViewHolder(holder, position);
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //var context = parent.Context; 
            RecyclerView.ViewHolder baseViewHolder = null;
            this.layoutInflater = LayoutInflater.From(parent.Context);
            if (viewType == ITEM_TYPE_LOADMORE)
            {
                baseViewHolder = GetLoadingView(parent);
                return baseViewHolder;
            }
            return innerAdapter.OnCreateViewHolder(parent, viewType);
        }
        public override int GetItemViewType(int position)
        {
            AutoLoadMore(position);
            if (position >= innerAdapter.ItemCount)
                return ITEM_TYPE_LOADMORE;
            return innerAdapter.GetItemViewType(position);
        }
        public LoadMoreWrapper(RecyclerView.Adapter adapter)
        {
            innerAdapter = adapter;
            isAutoLoadMore = true;
        }
        public LoadMoreWrapper(RecyclerView.Adapter adapter, bool isAutoLoadMore)
        {
            innerAdapter = adapter;
            this.isAutoLoadMore = isAutoLoadMore;
        }
        //public void AddData(List<T> list)
        //{

        //}
        public int GetLoadMoreViewCount()
        {
            if (onLoadMoreListener == null)
                return 0;
            if (innerAdapter.ItemCount == 0)
                return 0;
            return 1;
        }

        public void SetOnLoadMoreListener(IOnLoadMoreListener onLoadMoreListener)
        {
            this.onLoadMoreListener = onLoadMoreListener;
            loadmoreing = false;
        }

        protected View GetItemView(int layoutResId, ViewGroup parent)
        {
            return layoutInflater.Inflate(layoutResId, parent, false);
        }

        private RecyclerView.ViewHolder GetLoadingView(ViewGroup parent)
        {
            View view = GetItemView(loadMoreView.getLayoutId(), parent);
            var holder = new BaseViewHolder(parent.Context, view);
            holder.ItemView.SetOnClickListener(this);
            return holder;
        }
        public void NotifyAddData()
        {
            //innerAdapter.NotifyDataSetChanged();
            var loadViewStatus = loadMoreView.getLoadMoreStatus();
            loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_DEFAULT);
            NotifyDataSetChanged();
        }
        ///<summary>
        ///加载更多,完成
        /// </summary>
        public void LoadMoreComplete()
        {
            loadmoreing = false;
            loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_DEFAULT);
            NotifyItemChanged(innerAdapter.ItemCount);
        }

        /// <summary>
        /// 刷新结束，没有更多数据
        /// </summary>
        public void LoadMoreEndEmpty()
        {
            LoadMoreEndEmpty(false);
        }
        public void LoadMoreEndEmpty(bool isEnd)
        {
            loadmoreing = false;
            loadMoreView.setLoadMoreEndGone(isEnd);
            if (isEnd)
            {
                NotifyItemRemoved(innerAdapter.ItemCount);
            }
            else
            {
                loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_END);
                NotifyItemChanged(innerAdapter.ItemCount);
            }
        }

        /// <summary>
        /// 自动加载更多
        /// </summary>
        /// <param name="position"></param>
        private void AutoLoadMore(int position)
        {
            if (GetLoadMoreViewCount() == 0)
                return;
            if (position < ItemCount - 1)
                return;
            if (loadMoreView.getLoadMoreStatus() != LoadMoreView.STATUS_DEFAULT)
            {
                return;
            }
            if (isAutoLoadMore)
            {
                loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_LOADING);
                if (!loadmoreing)
                {
                    loadmoreing = true;
                    onLoadMoreListener.OnLoadMoreRequested();
                }
            }
            else
            {
                loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_CLICK);

                //loadMoreView.autoLoadMoreEvent();
            }
        }

        public void OnClick(View v)
        {
            if (loadMoreView.getLoadMoreStatus() == LoadMoreView.STATUS_FAIL)
            {
                loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_DEFAULT);
                NotifyItemChanged(innerAdapter.ItemCount);
            }
            if (loadMoreView.getLoadMoreStatus() == LoadMoreView.STATUS_CLICK)
            {
                if (!loadmoreing)
                {
                    loadMoreView.setLoadMoreStatus(LoadMoreView.STATUS_LOADING);
                    NotifyItemChanged(innerAdapter.ItemCount);
                    loadmoreing = true;
                    onLoadMoreListener.OnLoadMoreRequested();
                }
            }
        }
    }
}