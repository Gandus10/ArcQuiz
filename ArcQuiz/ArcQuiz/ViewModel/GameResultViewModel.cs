using ArcQuiz.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ArcQuiz.ViewModel
{
    class GameResultViewModel
    {
        string FileName { get; set; }
        public double Score { get; set; }
        public String ScoreLabel { get => ((int)(Score * 100))+ "% de bonnes réponses";
        }

        private Command goToMainView;
        public Command GoToMainView
        {
            get
            {
                return goToMainView ?? (goToMainView = new Command(async () => await Application.Current.MainPage.Navigation.PopToRootAsync()));
            }
        }

        private Command replay;
        public Command Replay
        {
            get
            {
                return replay ?? (replay = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new GameView(FileName))));
            }
        }

        public GameResultViewModel(double score, string fileName)
        { 
            Score = score;
            FileName = fileName;
        }
    }
}
