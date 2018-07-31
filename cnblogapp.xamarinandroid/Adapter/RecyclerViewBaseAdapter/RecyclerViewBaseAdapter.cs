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
using static Android.Support.V7.Widget.RecyclerView;

namespace cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter
{
    public abstract class BaseQuickAdapter<T> : RecyclerView.Adapter
    {
        List<T> data;
        private const int ITEM_TYPE_EMPTY = int.MaxValue - 1;
        private const int ITEM_TYPE_DEFAULT = int.MaxValue - 2;
        private View emptyView;
        private int emptyLayoutId;
        LayoutInflater layoutInflater;
        private int itemLayoutId;
        public BaseQuickAdapter(int itemLayoutId, List<T> data)
        {
            this.itemLayoutId = itemLayoutId;
            this.data = data == null ? new List<T>() : data;
        }
        public BaseQuickAdapter(int itemLayoutId) : this(itemLayoutId, null)
        {

        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            this.layoutInflater = LayoutInflater.From(parent.Context);
            ViewHolder viewHolder = null;
            if (viewType == ITEM_TYPE_EMPTY)
            {
                if (emptyView != null)
                {
                    viewHolder = BaseViewHolder.CreateViewHolder(parent.Context, emptyView);
                }
                else
                {
                    viewHolder = BaseViewHolder.CreateViewHolder(parent.Context, parent, emptyLayoutId);
                }
            }
            if (viewType == ITEM_TYPE_DEFAULT)
            {
                var itemView = GetItemView(itemLayoutId, parent);
                viewHolder = new BaseViewHolder(itemView);
            }
            return viewHolder;
        }
        View GetItemView(int layoutResId, ViewGroup parent)
        {
                return layoutInflater.Inflate(layoutResId, parent, false);
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            int viewType = viewHolder.ItemViewType;
            switch (viewType)
            {
                case ITEM_TYPE_EMPTY:
                    break;
                case ITEM_TYPE_DEFAULT:
                    ConvertAsync(viewHolder, data[viewHolder.LayoutPosition]);
                    break;
            }
        }

        public override int ItemCount => IsEmpty() ? 1 : data.Count;

        public override int GetItemViewType(int position)
        {
            if (IsEmpty())
                return ITEM_TYPE_EMPTY;
            return ITEM_TYPE_DEFAULT;
        }
        public void SetNewData(List<T> data)
        {
            this.data = data == null ? new List<T>() : data;
            NotifyDataSetChanged();
        }
        public void AddData(List<T> list)
        {
            this.data.AddRange(list);
            NotifyDataSetChanged();
        }
        protected abstract void ConvertAsync(RecyclerView.ViewHolder helper, T item);
        //void OnClick(HongyangAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        //void OnLongClick(HongyangAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        bool IsEmpty()
        {
            var result = (emptyView != null || emptyLayoutId != 0) && data.Count == 0;
            return result;
        }
        public void SetEmptyView(View emptyView)
        {
            this.emptyView = emptyView;
        }
        public void SetEmptyView(int emptyLayoutId)
        {
            this.emptyLayoutId = emptyLayoutId;
        }
    }
}