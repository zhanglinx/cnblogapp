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
using cnblogapp.xamarinandroid.Utils;
namespace cnblogapp.xamarinandroid.Fragments
{
    public class TalkFragment : BaseFragment, TabLayout.IOnTabSelectedListener
    {
        private ViewPager vpIndex;
        private TabLayout tabLayoutIndex;
        private TalkCategoryPagerAdapter adapter;
        protected override int GetLayoutId() => Resource.Layout.fragment_talk;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.search, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }
        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_search)
            {
                ToastUtil.ToastShort(Activity, "搜索");
            }
            return base.OnContextItemSelected(item);
        }
        protected override void InitView()
        {
            HasOptionsMenu = true;
            vpIndex = rootView.FindViewById<ViewPager>(Resource.Id.vp_index);
            tabLayoutIndex = rootView.FindViewById<TabLayout>(Resource.Id.tabLayout_index);
            adapter = new TalkCategoryPagerAdapter(this.ChildFragmentManager,new string[] { "全站","最新回复","关注","我的"});

            vpIndex.Adapter = adapter;
            vpIndex.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayoutIndex));
            tabLayoutIndex.TabMode = TabLayout.ModeFixed;
            tabLayoutIndex.TabGravity = TabLayout.GravityFill;
            tabLayoutIndex.SetupWithViewPager(vpIndex);
            tabLayoutIndex.SetOnTabSelectedListener(this);
            tabLayoutIndex.Post(() =>
            {
                adapter.OnRefresh(0);
            });
        }
        public void OnTabReselected(TabLayout.Tab tab)
        {
        }

        public void OnTabSelected(TabLayout.Tab tab)
        {
            vpIndex.CurrentItem = tab.Position;
            adapter.OnRefresh(tab.Position);
        }

        public void OnTabUnselected(TabLayout.Tab tab)
        {

        }
    }
}