using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Shared;
using cnblogapp.xamarinandroid.ViewModels;
using Com.Nostra13.Universalimageloader.Core;

namespace cnblogapp.xamarinandroid
{
    [Application]
    public class MyApplication:Application
    {
        public MyApplication() { }
        public MyApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public static List<BlogCategoryModel> blogCategoryList;
        public override void OnCreate()
        {
            base.OnCreate();
            ImageLoaderConfiguration configuration = new ImageLoaderConfiguration.Builder(this).WriteDebugLogs().Build();
            ImageLoader.Instance.Init(configuration);//初始化图片加载框架
            blogCategoryList = new List<BlogCategoryModel>();
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 1, CategoryName = "首页", ItemListActionName = "PostList", ParentCategoryId = 0, Position = 0, CategoryType = "TopSiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 2, CategoryName = "推荐", ItemListActionName = "PostList", ParentCategoryId = 0, Position = 1, CategoryType = "TopSiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId =108698,CategoryName=".NET",ItemListActionName="PostList",ParentCategoryId=0,Position=2,CategoryType="TopSiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 106876, CategoryName = "Java", ItemListActionName = "PostList", ParentCategoryId = 2, Position = 3, CategoryType = "SiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 108706, CategoryName = "Android", ItemListActionName = "PostList", ParentCategoryId = 108705, Position = 4, CategoryType = "SiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 807, CategoryName = "程序人生", ItemListActionName = "PostList", ParentCategoryId = 4, Position = 5, CategoryType = "SiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 108696, CategoryName = "Python", ItemListActionName = "PostList", ParentCategoryId = 2, Position = 6, CategoryType = "SiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 106882, CategoryName = "PHP", ItemListActionName = "PostList", ParentCategoryId = 2, Position = 7, CategoryType = "SiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 108703, CategoryName = "Web前端", ItemListActionName = "PostList", ParentCategoryId = 0, Position = 8, CategoryType = "TopSiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 108712, CategoryName = "数据库", ItemListActionName = "PostList", ParentCategoryId = 0, Position = 9, CategoryType = "TopSiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 108701, CategoryName = "软件设计", ItemListActionName = "PostList", ParentCategoryId = 0, Position = 10, CategoryType = "TopSiteCategory" });
            blogCategoryList.Add(new BlogCategoryModel() { CategoryId = 108724, CategoryName = "操作系统", ItemListActionName = "PostList", ParentCategoryId = 0, Position = 11, CategoryType = "TopSiteCategory" });
            
            //一些要求不高的初始化操作新开线程执行
            new Thread(() => {
                UserInfoManager.Init(this); 
            }).Start();
        }
    }
}