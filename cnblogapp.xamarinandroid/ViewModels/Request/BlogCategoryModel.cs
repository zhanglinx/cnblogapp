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

namespace cnblogapp.xamarinandroid.ViewModels
{
    public class BlogCategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryType { get; set; }

        public string ItemListActionName { get; set; }
        public int PageIndex { get; set; }
        public int ParentCategoryId { get; set; }

        public int Position { get; set; }
        public string CategoryName { get; set; }
        public BlogCategoryModel()
        {
            
        }
        public BlogCategoryModel(string  categoryType,string  ItemListActionName,int pageIndex,int parentCategoryId,int categoryId)
        {
            this.CategoryId = categoryId;
            this.CategoryType = categoryType;
            this.ItemListActionName = ItemListActionName;
            this.PageIndex = pageIndex;
            this.ParentCategoryId = parentCategoryId;
        }
    }
}