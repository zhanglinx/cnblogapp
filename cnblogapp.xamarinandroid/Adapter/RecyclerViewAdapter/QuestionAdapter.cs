using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter;
using cnblogapp.xamarinandroid.Utils;
using cnblogapp.xamarinandroid.ViewModels;
using cnblogapp.xamarinandroid.Widgets;
using Com.Nostra13.Universalimageloader.Core;
using Java.Lang;

namespace cnblogapp.xamarinandroid.Adapter.RecyclerViewAdapter
{
    public class QuestionAdapter : BaseQuickAdapter<QuestionModel>, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            if (v.Tag != null && v.Id == Resource.Id.cardview_item)
            {
               //ToastUtil.ToastShort(context,v.Tag.ToString());
            }
        }
        private DisplayImageOptions options;
        public QuestionAdapter() : base(Resource.Layout.item_question)
        {
            options = new DisplayImageOptions.Builder()
              .ShowImageForEmptyUri(Resource.Mipmap.ic_avatar_default)
              .ShowImageOnFail(Resource.Mipmap.ic_avatar_default)
               .ShowImageOnLoading(Resource.Mipmap.ic_avatar_default)
      .CacheInMemory(true)
      .BitmapConfig(Bitmap.Config.Rgb565)
      .CacheOnDisk(true).Build();
        }
        protected override void ConvertAsync(RecyclerView.ViewHolder helper, QuestionModel item)
        {
            BaseViewHolder holder = helper as BaseViewHolder;
            ((holder.GetView(Resource.Id.tv_dateAdded)) as TextView).Text = item.DateAdded.ToCommonString();
            ((holder.GetView(Resource.Id.tv_title)) as TextView).Text = item.Title;
            ((holder.GetView(Resource.Id.tv_viewCount)) as TextView).Text="浏览"+item.ViewCount;
            ((holder.GetView(Resource.Id.tv_answerCount)) as TextView).Text = item.AnswerCount.ToString();
            ((holder.GetView(Resource.Id.tv_awardCount)) as TextView).Text = item.Award.ToString();
            ((holder.GetView(Resource.Id.cardview_item)) as CardView).Tag = item.Qid.ToString();

            //var tv_comment = ((holder.GetView(Resource.Id.tv_commentCount)) as TextView);
            //if (item.CommentCount == 0)
            //{
            //    tv_comment.Visibility = ViewStates.Gone;
            //}
            //else
            //{
            //    tv_comment.Visibility = ViewStates.Visible;
            //    tv_comment.Text = item.CommentCount.ToString();
            //}
            var iv_avatar = (holder.GetView(Resource.Id.iv_avatar)) as XCircleImageView;
            if (item.QuestionUserInfo != null && item.QuestionUserInfo.UserID > 0)
            {
               ImageLoader.Instance.DisplayImage(item.QuestionUserInfo.IconName, iv_avatar, options);
                ((holder.GetView(Resource.Id.tv_userName)) as TextView).Text = item.QuestionUserInfo.UserName;
            }
        }
    }
}