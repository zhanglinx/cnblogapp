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
using Fragment = Android.Support.V4.App.Fragment;
namespace cnblogapp.xamarinandroid.Adapter
{
    public class NewsCategoryPagerAdapter : FragmentPagerAdapter
    {
        private Android.Support.V4.App.FragmentManager fragmentManager;
        //private List<NewsCategoryFragment> fragmentList;
        private Dictionary<int, Fragment> fragmentDict;
        private string[] tabTitleArray;  
        public override int Count{
            get
            {
                return tabTitleArray.Length;
            }
        }
        public NewsCategoryPagerAdapter(Android.Support.V4.App.FragmentManager  fm,string[] tabTitles):base(fm)
        {
            fragmentManager = fm;
            //fragmentList = new List<NewsCategoryFragment>();
            fragmentDict = new Dictionary<int, Fragment>();
            this.tabTitleArray = tabTitles;
        }
        public new string GetPageTitle(int position)
        {
            return tabTitleArray[position];
        }
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            
            if (position < 3)
            {
                var fragment = NewsCategoryFragment.GetFragment(position);
                if (!fragmentDict.ContainsKey(position))
                {
                    fragmentDict.Add(position,fragment);
                }
                return fragment;
            }
            else
            {
                var fragment = KbArticleFragment.GetFragment(3);
                if (!fragmentDict.ContainsKey(3))
                {
                    fragmentDict.Add(3, fragment);
                }
                return  fragment;
            }
        }
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int p0)
        {
            return new Java.Lang.String(tabTitleArray[p0]);
        }
        public void OnRefresh(int  position)
        {
            //fragmentList[position].Refresh();
            if (position < 3)
            {
                var newsCategoryFragment = fragmentDict[position] as NewsCategoryFragment;
                newsCategoryFragment.Refresh();
            }
            else
            {
                var  kbArticleFragment = fragmentDict[3] as KbArticleFragment;
                kbArticleFragment.Refresh();
            }
        }
    }
}