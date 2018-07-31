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
    public class KbArticleAdapter : BaseQuickAdapter<KbArticleModel>, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            if (v.Tag != null && v.Id == Resource.Id.cardview_item)
            {
               //ToastUtil.ToastShort(context,v.Tag.ToString());
            }
        }
        private DisplayImageOptions options;
        public KbArticleAdapter() : base(Resource.Layout.item_kbarticle)
        {
            options = new DisplayImageOptions.Builder()
              .ShowImageForEmptyUri(Resource.Mipmap.ic_avatar_default)
              .ShowImageOnFail(Resource.Mipmap.ic_avatar_default)
               .ShowImageOnLoading(Resource.Mipmap.ic_avatar_default)
         .CacheInMemory(true)
         .BitmapConfig(Bitmap.Config.Rgb565)
         .CacheOnDisk(true).Build();
        }
        protected override void ConvertAsync(RecyclerView.ViewHolder helper, KbArticleModel item)
        {
            BaseViewHolder holder = helper as BaseViewHolder;
            ((holder.GetView(Resource.Id.tv_dateAdded)) as TextView).Text = item.DateAdded.ToShortTimeString();
            ((holder.GetView(Resource.Id.tv_viewCount)) as TextView).Text ="浏览："+ item.ViewCount.ToString();
            ((holder.GetView(Resource.Id.tv_summary)) as TextView).Text = item.Summary;
            ((holder.GetView(Resource.Id.tv_diggCount)) as TextView).Text = item.Diggcount.ToString();
            ((holder.GetView(Resource.Id.tv_title)) as TextView).Text = item.Title;
            ((holder.GetView(Resource.Id.tv_author)) as TextView).Text = item.Author;
            ((holder.GetView(Resource.Id.cardview_item)) as CardView).Tag = item.Id.ToString();
            
        }
    }
}