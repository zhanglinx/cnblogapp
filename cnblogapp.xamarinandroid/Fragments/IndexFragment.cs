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
    public class IndexFragment : BaseFragment, TabLayout.IOnTabSelectedListener
    {
        private ViewPager vpIndex;
        private TabLayout  tabLayoutIndex;
        private BlogCategoryPagerAdapter adapter;
        protected override int GetLayoutId() => Resource.Layout.fragment_index;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu =true;
            // Create your fragment here
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.search, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }
        protected override void InitView()
        {
            HasOptionsMenu = true;
            vpIndex =rootView.FindViewById<ViewPager>(Resource.Id.vp_index);
            tabLayoutIndex = rootView.FindViewById<TabLayout>(Resource.Id.tabLayout_index);
            adapter = new BlogCategoryPagerAdapter(this.ChildFragmentManager, MyApplication.blogCategoryList.OrderBy(f => f.Position).Select(f => f.CategoryName).ToArray());

            vpIndex.Adapter = adapter;
            vpIndex.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayoutIndex));
            tabLayoutIndex.TabMode = TabLayout.ModeScrollable;
            tabLayoutIndex.TabGravity = TabLayout.GravityFill;
            tabLayoutIndex.SetupWithViewPager(vpIndex);
            tabLayoutIndex.SetOnTabSelectedListener(this);
            tabLayoutIndex.Post(() =>
            {
                adapter.OnRefresh(0);
            });
        }
        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_search)
            {
                ToastUtil.ToastShort(Activity,"搜索"); 
            }
            return base.OnContextItemSelected(item);
        }

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    // Use this to return your custom view for this Fragment
        //    // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
        //    //return base.OnCreateView(inflater, container, savedInstanceState);
        //    return inflater.Inflate(Resource.Layout.fragment_index,container,false);
        //}
        //public override void OnViewCreated(View view, Bundle savedInstanceState)
        //{
        //    base.OnViewCreated(view, savedInstanceState);
        //    HasOptionsMenu = true;
        //    vpIndex = view.FindViewById<ViewPager>(Resource.Id.vp_index);
        //    tabLayoutIndex = view.FindViewById<TabLayout>(Resource.Id.tabLayout_index);
        //    adapter = new BlogCategoryPagerAdapter(this.ChildFragmentManager,MyApplication.blogCategoryList.OrderBy(f=>f.Position).Select(f=>f.CategoryName).ToArray());

        //    vpIndex.Adapter = adapter;
        //    vpIndex.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayoutIndex));
        //    tabLayoutIndex.TabMode = TabLayout.ModeScrollable;
        //    tabLayoutIndex.TabGravity = TabLayout.GravityFill;
        //    tabLayoutIndex.SetupWithViewPager(vpIndex);
        //    tabLayoutIndex.SetOnTabSelectedListener(this);
        //    tabLayoutIndex.Post(() =>
        //    {
        //        adapter.OnRefresh(0);
        //    });
        //}
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

        public void OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
           //throw new NotImplementedException();
        }
    }
}