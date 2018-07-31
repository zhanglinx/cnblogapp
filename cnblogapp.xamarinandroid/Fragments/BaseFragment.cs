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
using Fragment = Android.Support.V4.App.Fragment;
namespace cnblogapp.xamarinandroid.Fragments
{
    public abstract class BaseFragment : Fragment
    {
        protected abstract int GetLayoutId();
        protected View rootView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            rootView = inflater.Inflate(GetLayoutId(), container, false);
            InitView();
            return  rootView;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }
        protected T FindViewById<T>(int id) where T : View
        {
             return  rootView.FindViewById<T>(id);
        }
        protected  virtual void InitView()
        {

        }
    }
}