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

namespace cnblogapp.xamarinandroid.Shared
{
    public sealed class UserInfoManager
    {
        public bool IsWebLogin { get; set; }
        private Token tokenModel;
        private static UserInfoManager userInfoManager;
        private Context context;
        public bool IsAuthLogin()
        {
            var  userToken = UserTokenShared.GetAccessToken(context);
            if (userToken != null && userToken.IsExpire == false && string.IsNullOrWhiteSpace(userToken.access_token))
            {
                return true;
            }
            return false;
        }
        public static UserInfoManager GetInstance()
        {
            if (userInfoManager == null)
            {
                throw new NullReferenceException("UserManager 没有初始化");
            }
            return userInfoManager;
        }
        public static void Init(Context applicationContext)
        {
            if (userInfoManager == null)
            {
                userInfoManager = new UserInfoManager(applicationContext);
            }
        }
        private UserInfoManager(Context context)
        {
            this.context = context;
        }
    }
}