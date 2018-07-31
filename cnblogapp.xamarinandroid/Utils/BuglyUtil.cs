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
using Com.Chteam.Agent;
using Newtonsoft.Json;

namespace cnblogapp.xamarinandroid.Utils
{
    public class BuglyUtil
    {
        public static void PostException(System.Exception ex) => BuglyAgentHelper.PostCatchedException(new Java.Lang.Throwable(ex.ToString()));

        public static void PostException(object obj)
        {
            try
            {
                BuglyAgentHelper.PostCatchedException(new Java.Lang.Throwable(JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings() {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })));
            }
            catch (System.Exception ex)
            {
                PostException(ex);
            }
        }

        public static void CheckUpdate(Activity activity)
        {
            try {
                BuglyAgentHelper.CheckUpgrade();
            }
            catch (System.Exception ex)
            {
                PostException(ex);
            }
        }
    }
}