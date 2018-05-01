using ArcQuiz.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcQuiz.Data
{
    public class ScoreDataBase
    {
        readonly SQLiteAsyncConnection database;

        public ScoreDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ScoreModel>().Wait();
        }

        public Task<List<ScoreModel>> GetScoresAsync()
        {
            return database.Table<ScoreModel>().Take(15).OrderByDescending(t=>t.ID).ToListAsync();
        }

        public Task<ScoreModel> GetScoreAsync(int id)
        {
            return database.Table<ScoreModel>().Where( i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveScoreAsync(ScoreModel score)
        {
            if(score.ID != 0)
            {
                return database.UpdateAsync(score);
            }
            else
            {
                return database.InsertAsync(score);
            }
        }

        public Task<int> DeleteScoreAsync(ScoreModel score)
        {
            return database.DeleteAsync(score);
        }
    }
}
