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

namespace cnblogapp.xamarinandroid.Views
{
    public interface IQuestionView
    {
        void GetServiceQuestionSuccess(List<QuestionModel> list);
        void GetLocalQuestionSuccess(List<QuestionModel> list);

        void GetServiceQuestionFail(string  error);
    }
}