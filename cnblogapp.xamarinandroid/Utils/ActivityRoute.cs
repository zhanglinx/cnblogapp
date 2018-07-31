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
using cnblogapp.xamarinandroid.Activities;

namespace cnblogapp.xamarinandroid.Utils
{
    public class ActivityRoute
    {
        public static void jumpToLogin(Context context)
        {
            Intent intent = new Intent(context, typeof(LoginActivity));
            context.StartActivity(intent);
        }
    }
}