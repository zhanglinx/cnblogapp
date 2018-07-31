using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Adapter;
using Fragment = Android.Support.V4.App.Fragment;
namespace cnblogapp.xamarinandroid.Fragments
{
    public class NewsFragment : BaseFragment, TabLayout.IOnTabSelectedListener
    {
        private ViewPager vpNews;
        private TabLayout tabLayoutNews;
        private NewsCategoryPagerAdapter adapter;
        protected override int GetLayoutId() => Resource.Layout.fragment_news;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here
        }
        protected override void InitView()
        {
            HasOptionsMenu = true;
            vpNews = rootView.FindViewById<ViewPager>(Resource.Id.vp_);
            tabLayoutNews = rootView.FindViewById<TabLayout>(Resource.Id.tabLayout_news);
            adapter = new NewsCategoryPagerAdapter(this.ChildFragmentManager, new string[] { "最新","热门","推荐","知识库"});

            vpNews.Adapter = adapter;
            vpNews.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayoutNews));
            tabLayoutNews.TabMode = TabLayout.ModeFixed;
            tabLayoutNews.TabGravity = TabLayout.GravityFill;
            tabLayoutNews.SetupWithViewPager(vpNews);
            tabLayoutNews.SetOnTabSelectedListener(this);
            tabLayoutNews.Post(() =>
            {
                adapter.OnRefresh(0);
            });
        }
        public void OnTabReselected(TabLayout.Tab tab)
        {
        }

        public void OnTabSelected(TabLayout.Tab tab)
        {
            vpNews.CurrentItem = tab.Position;
            adapter.OnRefresh(tab.Position);
        }

        public void OnTabUnselected(TabLayout.Tab tab)
        {

        }
    }
}