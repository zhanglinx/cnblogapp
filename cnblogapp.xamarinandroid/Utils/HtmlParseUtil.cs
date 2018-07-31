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
using cnblogapp.xamarinandroid.ViewModels;
using HtmlAgilityPack;

namespace cnblogapp.xamarinandroid.Utils
{
    public class HtmlParseUtil
    {
        public static List<BlogModel>  ListBlog(string  htmlContent)
        {
            List<BlogModel> result = new List<BlogModel> ();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            try
            {
                //var nodes = doc.DocumentNode.SelectNodes("./");
                var postItemNodes = doc.DocumentNode.SelectNodes("//div[@class='post_item']");
                foreach (var item in postItemNodes)
                {
                    BlogModel model = new BlogModel();
                    var diggNumNode = item.SelectSingleNode(".//span[@class='diggnum']");
                    model.Diggcount = int.Parse(diggNumNode.InnerText);
                    string idStr = diggNumNode.GetAttributeValue("id", "");
                    model.Id = int.Parse(idStr.Substring(idStr.LastIndexOf('_')+1,idStr.Length-idStr.LastIndexOf('_')-1));
                    var commentCountTxt = item.SelectSingleNode(".//span[@class='article_comment']").InnerText.Trim();
                    model.CommentCount = int.Parse(commentCountTxt.Substring(3, commentCountTxt.Length - 4));
                    model.Author = item.SelectSingleNode(".//a[@class='lightblue']").InnerText;
                    model.Title = item.SelectSingleNode(".//a[@class='titlelnk']").InnerText;
                    model.Description = item.SelectSingleNode(".//p[@class='post_item_summary']").InnerText.Trim();
                    model.Avatar = "https:" + item.SelectSingleNode(".//img")?.GetAttributeValue("src", "");
                    model.Url = item.SelectSingleNode(".//a[@class='titlelnk']").GetAttributeValue("href", "");
                    model.PostDate = DateTime.Now;
                    result.Add(model);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static List<TalkModel> ListTalkRecent(string htmlContent)
        {
            List<TalkModel> result = new List<TalkModel>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            try
            {
                //var nodes = doc.DocumentNode.SelectNodes("./");
                var postItemNodes = doc.DocumentNode.SelectNodes("//li[@class='entry_b']");
                foreach (var item in postItemNodes)
                {
                    TalkModel model = new TalkModel();
                    model.UserIconUrl = item.SelectSingleNode("./div[@class='ing-item']/div[@class='feed_avatar']//img")?.GetAttributeValue("src", "");
                    model.UserDisplayName = item.SelectSingleNode(".//a[@class='ing-author']").InnerText;
                    model.Content = item.SelectSingleNode(".//span[@class='ing_body']").InnerText;

                    var commentCountText = item.SelectSingleNode(".//a[@class='ing_reply gray']").InnerText;
                    model.CommentCount = 0;
                    if (commentCountText.Length > 2)
                    {
                        model.CommentCount = int.Parse(commentCountText.Substring(0, commentCountText.Length - 2));
                    }

                    var idText = item.SelectSingleNode(".//div[@class='feed_body']").GetAttributeValue("id","");
                    model.Id =int.Parse(idText.Substring(idText.LastIndexOf('_')+1,idText.Length-idText.LastIndexOf('_')-1));

                    var publishTimeText = item.SelectSingleNode(".//a[@class='ing_time gray']").InnerText;
                    model.DateAdded = DatetimeUtil.StringToDateTime(publishTimeText);
                    result.Add(model);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}