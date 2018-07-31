using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Activities;
using cnblogapp.xamarinandroid.Fragments;
using cnblogapp.xamarinandroid.Shared;
using cnblogapp.xamarinandroid.Utils;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
namespace cnblogapp.xamarinandroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : BaseActivity, BottomNavigationView.IOnNavigationItemSelectedListener,View.IOnClickListener
    {
        private TextView _textMessage;
        private FragmentManager fragmentManager;
        private NewsFragment newsFragment;
        private IndexFragment indexFragment;
        private TalkFragment TalkFragment;
        private QuestionFragment questionFragment;
        private MineFragment mineFragment;
        Handler getTokenHandler;
        protected override int LayoutResourceId => Resource.Layout.activity_main;

        protected override string ToolbarTitle =>Resources.GetString(Resource.String.toolbar_title_index);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            BottomNavigationViewHelper.DisableShiftMode(navigation);
            fragmentManager = SupportFragmentManager;
            getTokenHandler = new Handler();
            if (HttpClientUtil.accessToken == null || HttpClientUtil.accessToken.IsExpire || string.IsNullOrWhiteSpace(HttpClientUtil.accessToken.access_token))
            {
                getTokenHandler.Post(async () =>
                {
                    await HttpClientUtil.GetCredentials(token =>
                    {
                        AccessTokenShared.SaveAccessToken(this, token);
                        HttpClientUtil.accessToken = token;
                        SwicthFragment(Resource.Id.navigation_home);
                    },
                    (error) =>
                    {
                        ToastUtil.ToastShort(this, error);
                    });
                });
            }
            else
            {
                SwicthFragment(Resource.Id.navigation_home);
            }
        }
        public void OnClick(View v)
        {
            throw new System.NotImplementedException();
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            SwicthFragment(item.ItemId);
            return true;
        }
        void HideAllFragment(FragmentTransaction  fragmentTransaction)
        {
            if (newsFragment != null)
                fragmentTransaction.Hide(newsFragment);
            if (TalkFragment != null)
                fragmentTransaction.Hide(TalkFragment);
            if (indexFragment != null)
                fragmentTransaction.Hide(indexFragment);
            if (questionFragment != null)
                fragmentTransaction.Hide(questionFragment);
            if (mineFragment != null)
                fragmentTransaction.Hide(mineFragment);
        }
        void SwicthFragment(int id)
        {
            FragmentTransaction ft = fragmentManager.BeginTransaction();
            HideAllFragment(ft);
            switch (id)
            {
                case Resource.Id.navigation_home:
                    if (indexFragment == null)
                    {
                        indexFragment = new IndexFragment();
                        ft.Add(Resource.Id.flyout_, indexFragment);
                    }
                    else
                        ft.Show(indexFragment);
                    SetToolbarTitle(Resources.GetString(Resource.String.toolbar_title_index));
                    break;
                case Resource.Id.navigation_dashboard:
                    if (newsFragment == null)
                    {
                        newsFragment = new NewsFragment();
                        ft.Add(Resource.Id.flyout_, newsFragment);
                    }
                    else
                        ft.Show(newsFragment);
                    SetToolbarTitle(Resources.GetString(Resource.String.toolbar_title_news));
                    break;
                case Resource.Id.navigation_notifications:
                    if (TalkFragment == null)
                    {
                        TalkFragment = new TalkFragment();
                        ft.Add(Resource.Id.flyout_, TalkFragment);
                    }
                    else
                        ft.Show(TalkFragment);
                    SetToolbarTitle(Resources.GetString(Resource.String.toolbar_title_question));
                    break;
                case Resource.Id.navigation_question:
                    if (questionFragment == null)
                    {
                        questionFragment = new QuestionFragment(false);
                        ft.Add(Resource.Id.flyout_, questionFragment);
                    }
                    else
                    {
                        ft.Show(questionFragment);
                    }
                    SetToolbarTitle(Resources.GetString(Resource.String.title_question));
                    break;
                case Resource.Id.navigation_mine:
                    if (mineFragment == null)
                    {
                        mineFragment = new MineFragment();
                        ft.Add(Resource.Id.flyout_,mineFragment);
                    }
                    else
                    {
                        ft.Show(mineFragment);
                    }
                    break;
            }
            ft.Commit();
        }
    }
}

