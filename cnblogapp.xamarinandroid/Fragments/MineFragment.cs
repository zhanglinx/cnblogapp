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
using cnblogapp.xamarinandroid.Utils;
using cnblogapp.xamarinandroid.Widgets;

namespace cnblogapp.xamarinandroid.Fragments
{
    public class MineFragment : BaseFragment, View.IOnClickListener
    {
        protected override int GetLayoutId() => Resource.Layout.fragment_mine;
        private TextView tvNoLogin;
        private XCircleImageView imageNoLogin;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        protected override void InitView()
        {
            tvNoLogin = rootView.FindViewById<TextView>(Resource.Id.tv_no_login);
            imageNoLogin = rootView.FindViewById<XCircleImageView>(Resource.Id.iv_avatar_mine);
            tvNoLogin.SetOnClickListener(this);
            imageNoLogin.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.tv_no_login||v.Id==Resource.Id.iv_avatar_mine)
            {
                ActivityRoute.jumpToLogin(Activity); 
            }
            else
            {

            }
        }
    }
}