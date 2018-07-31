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
    public class TalkAdapter : BaseQuickAdapter<TalkModel>, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            if (v.Tag != null && v.Id == Resource.Id.cardview_item)
            {
               //ToastUtil.ToastShort(context,v.Tag.ToString());
            }
        }
        private DisplayImageOptions options;
        public TalkAdapter() : base(Resource.Layout.item_talk)
        {
            options = new DisplayImageOptions.Builder()
              .ShowImageForEmptyUri(Resource.Mipmap.ic_avatar_default)
              .ShowImageOnFail(Resource.Mipmap.ic_avatar_default)
               .ShowImageOnLoading(Resource.Mipmap.ic_avatar_default)
      .CacheInMemory(true)
      .BitmapConfig(Bitmap.Config.Rgb565)
      .CacheOnDisk(true).Build();
        }
        protected override void ConvertAsync(RecyclerView.ViewHolder helper, TalkModel item)
        {
            BaseViewHolder holder = helper as BaseViewHolder;
            ((holder.GetView(Resource.Id.tv_dateAdded)) as TextView).Text = item.DateAdded.ToString("yyyy-MM-dd HH:mm");
            ((holder.GetView(Resource.Id.tv_userDisplayName)) as TextView).Text = item.UserDisplayName;
            ((holder.GetView(Resource.Id.tv_content)) as TextView).Text=item.Content;
            ((holder.GetView(Resource.Id.ly_item)) as LinearLayout).Tag = item.Id.ToString();
            var tv_comment = ((holder.GetView(Resource.Id.tv_commentCount)) as TextView);
            if (item.CommentCount == 0)
            {
                tv_comment.Visibility = ViewStates.Gone;
            }
            else
            {
                tv_comment.Visibility = ViewStates.Visible;
                tv_comment.Text = item.CommentCount.ToString();
            }
            var iv_avatar = (holder.GetView(Resource.Id.iv_avatar)) as XCircleImageView;
            ImageLoader.Instance.DisplayImage(item.UserIconUrl, iv_avatar, options);
        }
    }
}