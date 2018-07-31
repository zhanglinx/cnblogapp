using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;


namespace cnblogapp.xamarinandroid.Utils
{
    public class BottomNavigationViewHelper
    {
        public static void DisableShiftMode(BottomNavigationView view)
        {
            BottomNavigationMenuView menuView =  (BottomNavigationMenuView)view.GetChildAt(0);
            try
            {
                Java.Lang.Reflect.Field shiftingMode = menuView.Class.GetDeclaredField("mShiftingMode");
                shiftingMode.Accessible = true;
                shiftingMode.SetBoolean(menuView,false);
                shiftingMode.Accessible = false;
                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    BottomNavigationItemView itemView = (BottomNavigationItemView)menuView.GetChildAt(i);
                    itemView.SetShiftingMode(false);
                    itemView.SetChecked(itemView.ItemData.IsChecked);
                }
            }
            catch (Exception ex)
            {
                BuglyUtil.PostException("unable to get shift mode field"+ex);
            }
        }
    }
}