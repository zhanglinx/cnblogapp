using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace cnblogapp.xamarinandroid.Activities
{
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : AppCompatActivity,Toolbar.IOnMenuItemClickListener
    {
        private Toolbar toolbar;
        protected abstract int LayoutResourceId { get; }
        protected abstract string ToolbarTitle { get; }
        protected void SetToolbarTitle(string title)
        {
            SupportActionBar.Title = title;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(LayoutResourceId);
                toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                if (toolbar != null)
                {
                    SetSupportActionBar(toolbar);
                    SupportActionBar.Title = ToolbarTitle;
                    toolbar.SetOnMenuItemClickListener(this);
                }
            }
            catch (Exception ex)
            {

            }
            // Create your application here
        }

        public virtual bool OnMenuItemClick(IMenuItem item)
        {
            return true;
        }
    }
}