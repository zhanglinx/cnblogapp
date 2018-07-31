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

namespace cnblogapp.xamarinandroid.Adapter.RecyclerViewBaseAdapter.LoadMore
{
    public abstract class LoadMoreView
    {
        public const int STATUS_DEFAULT = 1;
        public const int STATUS_LOADING = 2;
        public const int STATUS_FAIL = 3;
        public const int STATUS_END = 4;
        public const int STATUS_CLICK = 5;
        private int loadMoreStatus = STATUS_DEFAULT;
        private bool loadMoreEndGone = false;

        public void setLoadMoreStatus(int loadMoreStatus)
        {
            this.loadMoreStatus = loadMoreStatus;
        }
        public int getLoadMoreStatus()
        {
            return loadMoreStatus;
        }

        [Obsolete]
        public bool isLoadEndMore()
        {
            return loadMoreEndGone;
        }

        public void Convert(BaseViewHolder holder)
        {
            switch (loadMoreStatus)
            {
                case STATUS_LOADING:
                    SetVisibleLoading(holder, true);
                    SetVisibleLoadFail(holder, false);
                    SetVisibleLoadEnd(holder, false);
                    SetVisibleLoadClick(holder, false);
                    break;
                case STATUS_FAIL:
                    SetVisibleLoading(holder, false);
                    SetVisibleLoadFail(holder, true);
                    SetVisibleLoadEnd(holder, false);
                    SetVisibleLoadClick(holder, false);
                    break;
                case STATUS_END:
                    SetVisibleLoading(holder, false);
                    SetVisibleLoadFail(holder, false);
                    SetVisibleLoadEnd(holder, true);
                    SetVisibleLoadClick(holder, false);
                    break;
                case STATUS_CLICK:
                    SetVisibleLoading(holder, false);
                    SetVisibleLoadFail(holder, false);
                    SetVisibleLoadEnd(holder, false);
                    SetVisibleLoadClick(holder, true);
                    break;
                case STATUS_DEFAULT:
                    SetVisibleLoading(holder, false);
                    SetVisibleLoadFail(holder, false);
                    SetVisibleLoadEnd(holder, false);
                    SetVisibleLoadClick(holder, false);
                    break;
            }
        }

        private void SetVisibleLoading(BaseViewHolder holder, bool visible)
        {
            holder.SetVisible(getLoadingViewId(), visible);
        }

        private void SetVisibleLoadFail(BaseViewHolder holder, bool visible)
        {
            holder.SetVisible(getLoadFailViewId(), visible);
        }

        private void SetVisibleLoadEnd(BaseViewHolder holder, bool visible)
        {
            int loadEndViewId = getLoadEndViewId();
            if (loadEndViewId != 0)
            {
                holder.SetVisible(loadEndViewId, visible);
            }
        }

        private void SetVisibleLoadClick(BaseViewHolder holder, bool visible)
        {
            holder.SetVisible(getLoadClickViewId(), visible);
        }

        public void setLoadMoreEndGone(bool loadMoreEndGone)
        {
            this.loadMoreEndGone = loadMoreEndGone;
        }
        public bool IsLoadEndMoreGone()
        {
            if (getLoadEndViewId() == 0)
            {
                return true;
            }
            return loadMoreEndGone;
        }
        /// <summary>
        /// load more layout
        /// </summary>
        /// <returns></returns>
        public abstract int getLayoutId();
        /// <summary>
        /// loading view
        /// </summary>
        /// <returns></returns>
        protected abstract int getLoadingViewId();
        /// <summary>
        /// load fail view
        /// </summary>
        /// <returns></returns>
        protected abstract int getLoadFailViewId();
        /// <summary>
        /// load end view
        /// </summary>
        /// <returns></returns>
        protected abstract int getLoadEndViewId();
        protected abstract int getLoadClickViewId();
    }
}