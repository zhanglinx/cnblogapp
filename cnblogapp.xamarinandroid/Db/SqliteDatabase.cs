using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using cnblogapp.xamarinandroid.ViewModels;
using SQLite;

namespace cnblogapp.xamarinandroid.Db
{
     public class SqliteDatabase
    {
        private static readonly object _lock = new object();
        private static Database instance;
        private const string SQLITE_DB_NAME = "cnblog.db";
        public static Database Instance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), SQLITE_DB_NAME);
                        instance = new Database(dbPath);
                    }
                }
            }
            return instance;
        }
    }
    public class Database : SQLiteAsyncConnection
    {
        public Database(string path) : base(path)
        {
            CreateTable();
        }
        public async void CreateTable()
        {
            await CreateTableAsync<BlogModel>().ContinueWith((results) =>
            {
                Log.Error("CreateTable", "create BlogModel success");
            });
            await CreateTableAsync<NewsModel>().ContinueWith((results) =>
            {
                Log.Error("CreateTable", "create NewsModel success");
            });
            await CreateTableAsync<KbArticleModel>().ContinueWith((results) =>
            {
                Log.Error("CreateTable", "create KbArticleModel success");
            });
            await CreateTableAsync<TalkModel>().ContinueWith((results) =>
            {
                Log.Error("CreateTable", "create TalkModel success");
            });
            await CreateTableAsync<QuestionModel>().ContinueWith((results) =>
            {
                Log.Error("CreateTable", "create QuestionModel success");
            });
        }
        #region  博客

        public async Task<List<BlogModel>> GetBlogList(int pageSize, int categoryId)
        {
            return await Table<BlogModel>().Where(s => s.CategoryId == categoryId).OrderByDescending(a => a.PostDate).Skip(0).Take(pageSize).ToListAsync();
        }
        public async Task<BlogModel> GetBlog(int id)
        {
            return await Table<BlogModel>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateBlog(BlogModel model)
        {
            await UpdateAsync(model);
        }
        public async Task UpdateBlogList(List<BlogModel> list)
        {
            foreach (var item in list)
            {
                if (await GetBlog(item.Id) == null)
                {
                    await InsertAsync(item);
                }
                else
                {
                    await UpdateAsync(item);
                }
            }
        }
        #endregion 新闻

        #region 新闻
        public async Task<List<NewsModel>> GetNewsList(int pageSize)
        {
            return await Table<NewsModel>().Where(s => s.IsDigg == false && s.IsHot == false).OrderByDescending(a => a.DateAdded).Skip(0).Take(pageSize).ToListAsync();
        }

        public async Task<List<NewsModel>> GetNewsHotList(int pageSize)
        {
            return await Table<NewsModel>().Where(s => s.IsDigg == false && s.IsHot == true).OrderByDescending(a => a.DateAdded).Skip(0).Take(pageSize).ToListAsync();
        }

        public async Task<List<NewsModel>> GetNewsDiggList(int pageSize)
        {
            return await Table<NewsModel>().Where(s => s.IsDigg == true && s.IsHot == false).OrderByDescending(a => a.DateAdded).Skip(0).Take(pageSize).ToListAsync();
        }

        public async Task<NewsModel> GetNews(int id)
        {
            return await Table<NewsModel>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateNewsList(List<NewsModel> list)
        {
            foreach (var item in list)
            {
                if (await GetNews(item.Id) == null)
                {
                    await InsertAsync(item);
                }
                else
                {
                    await UpdateAsync(item);
                }
            }
        }
        #endregion
        #region 知识库
        public async Task<List<KbArticleModel>> GetKbArticleList(int pageSize)
        {
            return await Table<KbArticleModel>().OrderByDescending(a => a.DateAdded).Skip(0).Take(pageSize).ToListAsync();
        }

        public async Task<KbArticleModel> GetKbArticle(int id)
        {
            return await Table<KbArticleModel>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateKbArticleList(List<KbArticleModel> list)
        {
            foreach (var item in list)
            {
                if (await GetKbArticle(item.Id) == null)
                {
                    await InsertAsync(item);
                }
                else
                {
                    await UpdateAsync(item);
                }
            }
        }
        #endregion

        #region  闪存
        public async Task<List<TalkModel>> GetTalkList(int pageSize, TalkType type)
        {
            return await Table<TalkModel>().Where(s => s.TalkType == type).OrderByDescending(a => a.DateAdded).Skip(0).Take(pageSize).ToListAsync();
        }

        public async Task<TalkModel> GetTalk(int id)
        {
            return await Table<TalkModel>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateTalkList(List<TalkModel> list)
        {
            foreach (var item in list)
            {
                if (await GetTalk(item.Id) == null)
                {
                    await InsertAsync(item);
                }
                else
                {
                    await UpdateAsync(item);
                }
            }
        }
        #endregion

        #region 博问
        public async Task<List<QuestionModel>> ListQuestionAsync(int pageSize, QuestionType type)
        {
            return await Table<QuestionModel>().Where(s => s.QuestionType == type).OrderByDescending(s => s.DateAdded).Skip(0).Take(pageSize).ToListAsync();
        }
        public async Task<QuestionModel> GetQuestionAsync(int id)
        {
            return await Table<QuestionModel>().Where(s => s.Qid == id).FirstOrDefaultAsync();
        }
        public async Task UpdateQuestionList(List<QuestionModel> list)
        {
            foreach (var item in list)
            {
                if (await GetQuestionAsync(item.Qid) == null)
                {
                    await InsertAsync(item);
                }
                else
                {
                    await UpdateAsync(item);
                }
            }
        }
        #endregion

    }


}