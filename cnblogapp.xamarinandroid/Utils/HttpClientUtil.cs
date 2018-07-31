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
using cnblogapp.xamarinandroid.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace cnblogapp.xamarinandroid.Utils
{
    public class HttpClientUtil
    {
        public static Token accessToken;
        internal static RestClient GetRestClient(string  url)
        {
            var restClient = new RestClient(url)
            {
                Timeout = 5000,
                ReadWriteTimeout = 5000
            };
            return restClient;
        }
        /// <summary>
        /// 获取授权
        /// </summary>
        /// <returns></returns>
        public static async Task GetCredentials(Action<Token> successAction,Action<string> errorAction)
        {
            try
            {
                var client = GetRestClient(Constant.CONNECT_TOKEN);
                RestRequest request = new RestRequest();
                request.AddParameter("client_id", Constant.client_id);
                request.AddParameter("client_secret", Constant.client_secret);
                request.AddParameter("grant_type", "client_credentials");
                var response = await client.ExecutePostTaskAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    errorAction("网络请求失败" + response.StatusCode);
                    return;
                }
                if (string.IsNullOrEmpty(response.Content))
                {
                    errorAction("返回数据有误");
                    return;
                }
                var token = JsonConvert.DeserializeObject<Token>(response.Content);
                successAction(token);
            }
            catch (Exception ex)
            {
                errorAction(ex.StackTrace.ToString());
                BuglyUtil.PostException(ex);
            }
        }

        public static async Task GetAsync<T>(string  url ,Dictionary<string,string> _params,Action<T> success,Action<string> error)
        {
            try
            {
                var client = GetRestClient(url);
                RestRequest request = new RestRequest();
                if (_params != null)
                {
                    foreach (var kv in _params)
                    {
                        request.AddParameter(kv.Key, kv.Value);
                    }
                }
                request.AddHeader("Authorization", accessToken.token_type + " " + accessToken.access_token);
                var response = await client.ExecuteGetTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content;
                    var result = JsonConvert.DeserializeObject<T>(content);
                    success(result);
                }
                else
                {
                    error(response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                BuglyUtil.PostException(ex);
            }
        }
        public static async Task PostWebHttpAsync<T, M>(string url, M model, List<HttpHeader> restHeaders, List<HttpCookie> restCookies, Action<T> success, Action<string> error)
        {
            try
            {
                var client = GetRestClient(url);
                RestRequest request = new RestRequest();
                request.AddJsonBody(model);
                restHeaders.ForEach(f => { request.AddHeader(f.Name, f.Value); });
                restCookies.ForEach(f => { request.AddCookie(f.Name, f.Value); });
                var response = await client.ExecutePostTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content;
                    var result = JsonConvert.DeserializeObject<T>(content);
                    success(result);
                }
                else
                {
                    error(response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                BuglyUtil.PostException(ex);
            }
        }

        public static  void PostWebHttp<T, M>(string url, M model, List<HttpHeader> restHeaders, List<HttpCookie> restCookies, Action<T> success, Action<string> error)
        {
            try
            {
                var client = GetRestClient(url);
                RestRequest request = new RestRequest();
                request.AddJsonBody(model);
                restHeaders.ForEach(f => { request.AddHeader(f.Name, f.Value); });
                restCookies.ForEach(f => { request.AddCookie(f.Name, f.Value); });
                var response =  client.ExecuteAsPost(request,"post");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content;
                    var result = JsonConvert.DeserializeObject<T>(content);
                    success(result);
                }
                else
                {
                    error(response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                BuglyUtil.PostException(ex);
            }
        }

        public static async Task PostBlogCategoryAsync(string url,BlogCategoryModel model, Action<List<BlogModel>> success, Action<string> error)
        {
            try
            {
                var client = GetRestClient(url);
                RestRequest request = new RestRequest();
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
                request.AddHeader("Origin", "https://www.cnblogs.com");
                //request.AddHeader("");
                request.AddJsonBody(model);
                var response = await client.ExecutePostTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content;
                    var result = HtmlParseUtil.ListBlog(content);
                    if (result != null)
                    {
                        success(result);
                    }
                    else
                    {
                        error("html parse error");
                    }
                }
                else
                {
                    error(response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                BuglyUtil.PostException(ex);
                error(ex.Message);
            }
        }


        public static async Task GetTalkRecentAsync(string url, Action<List<TalkModel>> success, Action<string> error)
        {
            try
            {
                var client = GetRestClient(url);
                RestRequest request = new RestRequest();
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
                request.AddHeader("Origin", "https://www.cnblogs.com");
                request.AddHeader("referer", "https://ing.cnblogs.com/");
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                //request.AddHeader("");
                var response = await client.ExecuteGetTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content;
                    var result = HtmlParseUtil.ListTalkRecent(content);
                    if (result != null)  
                    {
                        success(result);
                    }
                    else
                    {
                        error("html parse error");
                    }
                }
                else
                {
                    error(response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                BuglyUtil.PostException(ex);
                error(ex.Message);
            }
        }
    }
}