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
using SQLite;

namespace cnblogapp.xamarinandroid.ViewModels
{
    /// <summary>
    /// 闪存
    /// </summary>
    public class TalkModel
    {
        [PrimaryKey, Indexed]
        public int Id { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否私有
        /// </summary>
        public bool IsPrivate { get; set; }
        public bool IsLucky { get; set; }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommentCount { get; set; }
        public string UserAlias { get; set; }
        /// <summary>
        /// 用户显示名
        ///</summary>
        public string UserDisplayName { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime DateAdded { get; set; }
        /// <summary>
        /// 用户头像url
        /// </summary>
        public string UserIconUrl { get; set; }
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        [Ignore]
        public int ParentCommentId { get; set; }
        [Ignore]
        public string ParentCommentContent { get; set; }
        [Ignore]
        public int StatusId { get; set; }
        [Ignore]
        public string StatusUserAlias { get; set; }
        [Ignore]
        public string StatusContent { get; set; }
        [Ignore]
        public List<TalkCommentModel> Comments { get; set; }
        /// <summary>
        /// 闪存类型
        /// </summary>
        public TalkType  TalkType { get; set; }
    }
    /// <summary>
    /// 闪存评论
    /// </summary>
    public class TalkCommentModel
    {
        [PrimaryKey, Indexed]
        public int Id { get; set; }
        public string Content { get; set; }
        public int TalkId { get; set; }
        public string UserAlias { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserIconUrl { get; set; }
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
    }
    public enum TalkType{
        All,RecentComment,Following,Me
    }
}