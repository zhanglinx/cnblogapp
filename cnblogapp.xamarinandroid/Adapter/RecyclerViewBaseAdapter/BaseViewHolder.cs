using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Android.Resource;

namespace cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter
{
    public class BaseViewHolder : RecyclerView.ViewHolder
    {
        private SparseArray<View> views;
        private HashSet<Integer> nestViews;
        private HashSet<Integer> childClickViewIds;
        private HashSet<Integer> itemChildLongClickViewIds;
        Java.Lang.Object associtedObject;
        public View convertView;
        public Action AutoLoadMore;
        public BaseViewHolder(View view) : base(view)
        {
            this.views = new SparseArray<View>();
            this.childClickViewIds = new HashSet<Integer>();
            this.itemChildLongClickViewIds = new HashSet<Integer>();
            this.nestViews = new HashSet<Integer>();
            convertView = view;
        }

        public BaseViewHolder(Context context, View view) : base(view)
        {
            this.views = new SparseArray<View>();
            this.childClickViewIds = new HashSet<Integer>();
            this.itemChildLongClickViewIds = new HashSet<Integer>();
            this.nestViews = new HashSet<Integer>();
            convertView = view;
        }
        public View GetConvertView()
        {
            return convertView;
        }
        public BaseViewHolder SetVisible(int viewId, bool visible)
        {
            View view = GetView(viewId);
            view.Visibility = (visible ? ViewStates.Visible : ViewStates.Gone);
            return this;
        }
        public void SetOnLoadMoreClick(int viewId, Action onclick)
        {
            View view = GetView(viewId);
            view.Click += (s, e) => { onclick(); };
            // return this;
        }
        public static BaseViewHolder CreateViewHolder(Context context, View itemView)
        {
            BaseViewHolder holder = new BaseViewHolder(itemView);
            return holder;
        }

        public static BaseViewHolder CreateViewHolder(Context context, ViewGroup parent, int layoutId)
        {
            View itemView = LayoutInflater.From(context).Inflate(layoutId, parent, false);
            BaseViewHolder holder = new BaseViewHolder(itemView);
            return holder;
        }
        public View GetView(int viewId)
        {
            View view = views.Get(viewId);
            if (view == null)
            {
                view = convertView.FindViewById(viewId);
                views.Put(viewId, view);
            }
            return view;
        }

        public Java.Lang.Object GetAssociatedObject()
        {
            return associtedObject;
        }
        public void SetAssociatedObject(Java.Lang.Object _associatedObject)
        {
            associtedObject = _associatedObject;
        }
    }
}