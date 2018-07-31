using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using cnblogapp.xamarinandroid.Utils;
using cnblogapp.xamarinandroid.ViewModels;
using RestSharp;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace cnblogapp.xamarinandroid.Activities
{
    [Activity(Label = "LoginActivity",Theme ="@style/AppTheme")]
    public class LoginActivity : BaseActivity
    {
        protected override int LayoutResourceId => Resource.Layout.activity_login;

        protected override string ToolbarTitle => Resources.GetString(Resource.String.login);

        private ProgressBar  progressBar;
        private WebView loginview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressbar_login);
            progressBar.Max = 100;
            loginview = FindViewById<WebView>(Resource.Id.webview_login);
            loginview.Settings.SetSupportZoom(true);
            loginview.Settings.JavaScriptEnabled = true;
            loginview.Settings.BuiltInZoomControls = true;
            loginview.Settings.CacheMode = CacheModes.NoCache;
            WebViewClient webClient = new WebViewClient();
            loginview.SetWebChromeClient(new LoginWebChromeClient(progressBar));
            loginview.SetWebViewClient(new LoginGetCookieClient(this));
            loginview.LoadUrl(Constant.CNBLOG_WEB_LOGIN);
            string url = string.Format(Constant.GetAuthrize,Constant.client_id);
            // Create your application here
            new Handler((message) =>
            {

            });
        }
        public class LoginWebChromeClient : WebChromeClient
        {
            private ProgressBar progressbar;
            public LoginWebChromeClient(ProgressBar _progress)
            {
                progressbar = _progress;
            }
            public override void OnProgressChanged(WebView view, int newprogress)
            {
                progressbar.Progress = newprogress;
                if (newprogress < 100)
                {
                    if (progressbar.Visibility == ViewStates.Gone) progressbar.Visibility = ViewStates.Visible;
                }
                else progressbar.Visibility = ViewStates.Gone;
                base.OnProgressChanged(view, newprogress);
            }
        };

        public class LoginGetCookieClient : WebViewClient
        {
            private Context context;
            public LoginGetCookieClient(Context context)
            {
                this.context = context;
            }
            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                view.LoadUrl(Constant.CNBLOG_WEB_LOGIN);
                return true;
            }
            public override void OnPageFinished(WebView view, string url)
            {
                if (Build.VERSION.SdkInt < Build.VERSION_CODES.Lollipop)
                {
                    CookieSyncManager.CreateInstance(context);
                }
                Android.Webkit.CookieManager cookieManager = CookieManager.Instance;
                string cookieStr = cookieManager.GetCookie("https://www.cnblogs.com/");
                if (string.IsNullOrWhiteSpace(cookieStr))
                    return;
                string[] cookieArray = cookieStr.Split(';');
                List<RestSharp.HttpCookie> cookieList = new List<RestSharp.HttpCookie>();
                foreach (var item in cookieArray)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        string cookieItemText = item.Trim();
                        RestSharp.HttpCookie cookie = new RestSharp.HttpCookie();
                        int nameIndex = cookieItemText.IndexOf('=');
                        cookie.Name = cookieItemText.Substring(0, nameIndex);
                        cookie.Value = cookieItemText.Substring(nameIndex + 1, cookieItemText.Length - nameIndex - 1);
                        cookieList.Add(cookie);
                    }
                }
                CookieSyncManager.Instance.Sync();
                List<HttpHeader> headers = new List<HttpHeader>();
                headers.Add(new HttpHeader() { Name = "Host", Value = "www.cnblogs.com" });
                headers.Add(new HttpHeader() { Name = "Origin", Value = "https://www.cnblogs.com" });
                headers.Add(new HttpHeader() { Name = "X-Requested-With", Value = "XMLHttpRequest" });
                headers.Add(new HttpHeader() { Name = "Referer", Value = "https://www.cnblogs.com/" });
                HttpClientUtil.PostWebHttp<WebResponseMessage, DiggBuryModel>("https://www.cnblogs.com/mvc/vote/VoteBlogPost.aspx", new DiggBuryModel("ansang", 9371764), headers, cookieList,
                                 (model) => {
                                     System.Diagnostics.Debug.Write(model.Message);
                                 },
                                 (error) =>
                                 {
                                     System.Diagnostics.Debug.Write(error);
                                 });
                base.OnPageFinished(view, url);
            }
        }

        public class LoginWebViewClient : WebViewClient
        {
            public override WebResourceResponse ShouldInterceptRequest(WebView view, IWebResourceRequest request)
            {
                return base.ShouldInterceptRequest(view, request);

            }
            private Context context;
            // private static Token token = AccessTokenUtil.GetToken(context);
            public LoginWebViewClient(Context _context)
            {
                context = _context;
            }
            [Obsolete]
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                if (url.IndexOf(Constant.Callback) > -1)
                {
                    Uri uri = new Uri(url.Replace("#", "?"));
                    var query = uri.Query.TrimStart('?').Split('&');
                    foreach (var item in query)
                    {
                        var q = item.Split('=');
                        if (q[0] == "code")
                        {
                            //var code = q[1];
                            //System.Diagnostics.Debug.Write(code);
                            //Token token = AccessTokenUtil.GetToken(context);
                            //AuthorizationRequest.Authorization_Code(token, code, (userToken) =>
                            //{
                            //    System.Diagnostics.Debug.Write(userToken.access_token);
                            //    UserTokenUtil.SaveToken(userToken, context);
                            //    // ActivityCompat.FinishAfterTransition(context);
                            //    context.StartActivity(new Intent(context, typeof(MainActivity)));
                            //    //MobclickAgent.OnProfileSignIn(code);
                            //},
                            //error =>
                            //{
                            //    //MobclickAgent.ReportError(context, "登录失败" + error);
                            //    System.Diagnostics.Debug.Write(error);
                            //});
                        }
                    }
                    // view.stoploading();
                }
                return base.ShouldOverrideUrlLoading(view, url);
            }
        }
    }
}