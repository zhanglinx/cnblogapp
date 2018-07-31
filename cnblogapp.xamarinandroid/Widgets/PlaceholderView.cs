using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace cnblogapp.xamarinandroid.Widgets
{
    public class PlaceholderView:FrameLayout
    {
        private View loginView;
        private View rootLayoutView;
        public PlaceholderView(Context context):base(context)
        {

        }
        public PlaceholderView(Context context, IAttributeSet attrs):base(context,attrs)
        {

        }
        public PlaceholderView(Context context, IAttributeSet attrs, int defStyleAttr):base(context,attrs,defStyleAttr)
        {

        }
        protected void InitView(IAttributeSet attrs ,  int defStyleAttr)
        {
            rootLayoutView = LayoutInflater.From(Context).Inflate(Resource.Layout.view_unlogin, this, false);
            loginView = rootLayoutView.FindViewById<LinearLayout>(Resource.Id.ll_unlogin);

        }
    }
}