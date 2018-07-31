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
    public class AccessTokenShared
    {
        private const string ACCESS_TOKEN_NAME = "access_token_shared";
        public static void SaveAccessToken(Context context, Token token)
        {
            BaseShared.Instance(context, ACCESS_TOKEN_NAME).SetString("access_token", token.access_token);
            BaseShared.Instance(context, ACCESS_TOKEN_NAME).SetString("token_type", token.token_type);
            BaseShared.Instance(context, ACCESS_TOKEN_NAME).SetInt("expires_in", token.expires_in);
            BaseShared.Instance(context, ACCESS_TOKEN_NAME).SetDateTime("refresh_time", token.RefreshTime);
        }
        public static Token GetAccessToken(Context context)
        {
            Token token = new Token();
            token.access_token = BaseShared.Instance(context, ACCESS_TOKEN_NAME).GetString("access_token", "");
            token.token_type = BaseShared.Instance(context, ACCESS_TOKEN_NAME).GetString("token_type", "");
            token.expires_in = BaseShared.Instance(context, ACCESS_TOKEN_NAME).GetInt("expires_in", 0);
            token.RefreshTime = BaseShared.Instance(context, ACCESS_TOKEN_NAME).GetDateTime("refresh_time");
            return token;
        }
    }
}