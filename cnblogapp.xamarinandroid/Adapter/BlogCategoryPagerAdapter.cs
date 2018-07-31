using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Fragments;

namespace cnblogapp.xamarinandroid.Adapter
{
    public class BlogCategoryPagerAdapter : FragmentPagerAdapter
    {

        private Android.Support.V4.App.FragmentManager fragmentManager;
        private List<BlogCategoryFragment> fragmentList;
        private string[] tabTitleArray;  
        public override int Count{
            get
            {
                return tabTitleArray.Length;
            }
        }
        public BlogCategoryPagerAdapter(Android.Support.V4.App.FragmentManager  fm,string[] tabTitles):base(fm)
        {
            fragmentManager = fm;
            fragmentList = new List<BlogCategoryFragment>();
            this.tabTitleArray = tabTitles;
        }
        public new string GetPageTitle(int position)
        {
            return tabTitleArray[position];
        }
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            var fragment = BlogCategoryFragment.GetFragment(position);
            if (!fragmentList.Contains(fragment))
            {
                fragmentList.Add(fragment);
            }
            return fragment;
        }
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int p0)
        {
            return new Java.Lang.String(tabTitleArray[p0]);
        }
        public void OnRefresh(int  position)
        {
            fragmentList[position].Refresh();
        }
    }
}