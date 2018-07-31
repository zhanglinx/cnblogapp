using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Db;
using cnblogapp.xamarinandroid.Utils;
using cnblogapp.xamarinandroid.ViewModels;
using cnblogapp.xamarinandroid.Views;

namespace cnblogapp.xamarinandroid.Presenter
{
    public class BlogPresenter
    {
        private IBlogView blogView;
        public BlogPresenter(IBlogView blogView)
        {
            this.blogView = blogView;
        }
        public async Task GetServiceBlog(int pageIndex, int pageSize, int position)
        {
            string url = string.Empty;
            //开放平台上请求数据
            if (position < 2)
            {
                if (position == 0)
                {
                    url = string.Format(Constant.BLOG_HOME_LIST, pageIndex, pageSize);
                }
                else
                {
                    url = string.Format(Constant.BLOG_PICKED_LIST, pageIndex, pageSize);
                }
                await HttpClientUtil.GetAsync<List<BlogModel>>(url, null,
                async (list) =>
                {
                    blogView.GetServiceBlogSuccess(list);
                    BlogCategoryModel model = MyApplication.blogCategoryList.Where(f => f.Position == position).FirstOrDefault();
                    list.ForEach(f => f.CategoryId = model.CategoryId);
                    await SqliteDatabase.Instance().UpdateBlogList(list);
                },
                (error) =>
                {
                    blogView.GetServiceBlogFail(error);
                });
            }
            //从网页总抓取数据
            else {
                url = string.Format(Constant.BLOG_CATEGORY_LIST, pageIndex, pageSize);
                BlogCategoryModel model= MyApplication.blogCategoryList.Where(f=>f.Position==position).FirstOrDefault();
                model.PageIndex = pageIndex;
                await HttpClientUtil.PostBlogCategoryAsync(url,model,
                    async (list)=> {
                        blogView.GetServiceBlogSuccess(list);
                        list.ForEach(f => f.CategoryId = model.CategoryId);
                        await SqliteDatabase.Instance().UpdateBlogList(list);
                    },
                    (error) => {
                        blogView.GetServiceBlogFail(error);
                    });
            }
        }
        public async Task GetLocalBlog(int pageSize,int position)
        {
            int categoryId = MyApplication.blogCategoryList.Where(s => s.Position == position).FirstOrDefault().CategoryId;
            var blogList = await SqliteDatabase.Instance().GetBlogList(pageSize,categoryId);
            blogView.GetLocalBlogSuccess(blogList);
        }
    }
}