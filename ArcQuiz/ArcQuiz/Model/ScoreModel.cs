using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcQuiz.Model
{
    public class ScoreModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string GameMode { get; set; }
        public int TotalNumberOfPoints { get; set; }
        public int TotalOfCorrectedAnswers { get; set; }
        public string Date { get; set; }
        public int Score { get; set; }
    }
}
