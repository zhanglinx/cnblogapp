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

namespace cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter.LoadMore
{
    public class SimpleLoadMoreView : LoadMoreView
    {
        public override int getLayoutId()
        {
            return Resource.Layout.quick_view_load_more;
        }

        protected override int getLoadEndViewId()
        {
            return Resource.Id.load_more_load_end_view;
        }

        protected override int getLoadFailViewId()
        {
            return Resource.Id.load_more_load_fail_view;
        }

        protected override int getLoadingViewId()
        {
            return Resource.Id.load_more_loading_view;
        }
        protected override int getLoadClickViewId()
        {
            return Resource.Id.load_more_click_view;
        }
    }
}