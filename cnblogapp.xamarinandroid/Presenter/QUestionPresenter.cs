using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.Db;
using cnblogapp.xamarinandroid.Utils;
using cnblogapp.xamarinandroid.ViewModels;
using cnblogapp.xamarinandroid.Views;

namespace cnblogapp.xamarinandroid.Presenter
{
    public class QuestionPresenter
    {
        private IQuestionView  questionView;
        public QuestionPresenter(IQuestionView view)
        {
            this.questionView = view;
        }
        public async Task GetServiceQuestionListAsync(int pageIndex, int pageSize,QuestionType type)
        {
            string _questionType = string.Empty;
            switch (type)
            {
                case QuestionType.最新:
                    _questionType = "unsolved";
                    break;
                case QuestionType.高分:
                    _questionType = "highscore";
                    break;
                case QuestionType.待解决:
                    _questionType = "noanswer";
                    break;
                case QuestionType.已解决:
                    _questionType = "solved";
                    break;
                case QuestionType.我的提问:
                    _questionType = "myquestion";
                    break;
                case QuestionType.被采纳:
                    _questionType = "mybestanswer";
                    break;
                case QuestionType.我的回答:
                    _questionType = "myanswer";
                    break;
            }
            string url =string.Format(Constant.Question_List,_questionType,pageIndex,pageSize);
            await HttpClientUtil.GetAsync<List<QuestionModel>>(url, null,
                async (list) =>
                {
                    list.ForEach(f =>
                    {
                        if (f.QuestionUserInfo != null)
                        {
                            f.QuestionUserInfo.IconName = Constant.CNBLOG_PIC+ f.QuestionUserInfo.IconName;
                        }
                        f.QuestionType = type;
                     });
                    questionView.GetServiceQuestionSuccess(list);
                    await SqliteDatabase.Instance().UpdateQuestionList(list);
                },
                (error) =>
                {
                    questionView.GetServiceQuestionFail(error);
                });
        }

        public async Task GetLocalQuestionListAsync(int pageSize,QuestionType type)
        {
            var  list =  await SqliteDatabase.Instance().ListQuestionAsync(pageSize,type);
            questionView.GetLocalQuestionSuccess(list);
        }

    }
}