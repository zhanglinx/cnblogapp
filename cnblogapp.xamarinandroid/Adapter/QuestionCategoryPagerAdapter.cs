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
    public class  QuestionCategoryPagerAdapter : FragmentPagerAdapter
    {
        private Android.Support.V4.App.FragmentManager fragmentManager;
        private List<QuestionCategoryFragment> fragmentList;
        private string[] tabTitleArray;
        private bool isme;
        public override int Count{
            get
            {
                return tabTitleArray.Length;
            }
        }
        public QuestionCategoryPagerAdapter(Android.Support.V4.App.FragmentManager  fm,string[] tabTitles,bool isme):base(fm)
        {
            fragmentManager = fm;
            fragmentList = new List<QuestionCategoryFragment>();
            this.tabTitleArray = tabTitles;
            this.isme = isme;
        }
        public new string GetPageTitle(int position)
        {
            return tabTitleArray[position];
        }
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            var fragment = QuestionCategoryFragment.GetFragment(position,isme);
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