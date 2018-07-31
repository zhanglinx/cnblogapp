using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cnblogapp.xamarinandroid.Utils
{
    public class Constant
    {
        public const string client_id = "22f870a0-0e44-4439-bbb1-2cde1b5339c6";
        public const string client_secret = "Xdc4V-jrnPPfJtRi3d1amMM93FAgYup6nJcVZwKFPU6vxRDX21znCFamWCuwCUfs5gv8XpCEeVXFAnp6";
        public const string Host = "https://api.cnblogs.com";
        public const string HOST_WEB = "https://www.cnblogs.com";
        public const string CNBLOG_WEB_LOGIN = "https://passport.cnblogs.com/user/signin?ReturnUrl=https%3A%2F%2Fwww.cnblogs.com%2F";
        public const string redirect_uri = "https://oauth.cnblos.com/auth/callback"; //回调地址
        public const string CONNECT_TOKEN = "https://oauth.cnblogs.com/connect/token";//获取token
        public const string GetAuthrize = "https://oauth.cnblogs.com/connect/authorize?client_id={0}&scope=openid profile CnBlogsApi offline_access&response_type=code id_token&redirect_uri=https://oauth.cnblogs.com/auth/callback&state=cnblogs.com&nonce=zhanglin";
        public const string Callback = "https://oauth.cnblogs.com/auth/callback";
        public const string CNBLOG_PIC = "https://pic.cnblogs.com/avatar/";
        #region 博客
        /// <summary>
        /// 首页-分页博客园首页文章列表
        /// </summary>
        public const string BLOG_HOME_LIST= Host+"/api/blogposts/@sitehome?pageIndex={0}&pageSize={1}";
        /// <summary>
        /// 精华
        /// </summary>
        public const string BLOG_PICKED_LIST = Host + "/api/blogposts/@picked?pageIndex={0}&pageSize={1}";
        /// <summary>
        /// 博客分类，从网页上抓取
        /// </summary>
        public const string BLOG_CATEGORY_LIST = HOST_WEB + "/mvc/AggSite/PostList.aspx";
       #endregion

        #region 新闻
        /// <summary>
        /// 本周热门
        /// </summary>
        public const string NEWS_HOT_CURRENT_WEEK_LIST = Host + "/api/newsitems/@hot-week?pageIndex={0}&pageSize={1}";
        /// <summary>
        /// 推荐新闻
        /// </summary>
        public const string NEWS_DIGG_LIST = Host + "/api/newsitems/@recommended?pageIndex={0}&pageSize={1}";
        /// <summary>
        /// 最新新闻
        /// </summary>
        public const string NEWS_NEWEST_LIST = Host + "/api/newsitems?pageIndex={0}&pageSize={1}";
        #endregion

        #region 知识库
        public const string KBARTICLE_LIST = Host + "/api/KbArticles?pageIndex={0}&pageSize={1}";
        #endregion

        #region  闪存
        /// <summary>
        /// 全站所有
        /// </summary>
        public const string TALK_ALL_LIST = Host + "/api/statuses/@{0}?pageIndex={1}&pageSize={2}&tag=";
        /// <summary>
        /// 最新回复 从网页上抓取
        /// </summary>
        public const string TAKL_RECENT_LIST = "https://ing.cnblogs.com/ajax/ing/GetIngList?IngListType={0}&PageIndex={1}&PageSize={2}";
        #endregion

        #region 问题
        public const string Question_List= "https://api.cnblogs.com/api/questions/@{0}?pageIndex={1}&pageSize={2}";
        #endregion

    }
}