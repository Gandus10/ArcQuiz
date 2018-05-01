using ArcQuiz.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcQuiz.ViewModel
{
    class ScoreViewModel : INotifyPropertyChanged
    {
        private string averageLast15ScoreLabel;
        public string AverageLast15ScoreLabel
        {
            get
            {
                int scoreLabel = (int)(AverageLast15Score * 100);
                return scoreLabel.ToString() + "%";
            }
            set
            {
                averageLast15ScoreLabel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageLast15ScoreLabel"));
            }
        }

        public double averageLast15Score;
        public double AverageLast15Score{
            get => averageLast15Score;
            set {
                averageLast15Score = value;
                AverageLast15ScoreLabel = value * 100 + "%";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageLast15Score"));
            }
        }
        public ObservableCollection<ScoreModel> Scores { get; set; } = new ObservableCollection<ScoreModel>();
        public ICommand ItemSelectedCommand { get; private set; }

        private string selectedItemText;
        public string SelectedItemText
        {
            get => selectedItemText;
            set
            {
                selectedItemText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItemText"));
            }
        }

        public ScoreViewModel()
        {
            PopulateScore();
            ItemSelectedCommand = new Command<ScoreModel>(HandleItemSelected);
        }

        private double CalculateAverageScore()
        {
            int somme = 0;
            foreach (ScoreModel score in Scores)
            {
                somme += score.Score;
            }
            try {
                double resultInPercentage = somme / Scores.Count;
                double finalResult = resultInPercentage / 100;
                return finalResult;
            }
            catch
            {
                return 0;
            }
            
        }

        private void HandleItemSelected(ScoreModel scoreModel)
        {
            SelectedItemText = $"{scoreModel.Date} {scoreModel.GameMode} {scoreModel.TotalNumberOfPoints}";
        }


        private async void PopulateScore()
        {
            List<ScoreModel> scores = await App.Database.GetScoresAsync();
            foreach(ScoreModel score in scores)
            {
                Scores.Add(score);
            }

            AverageLast15Score = CalculateAverageScore();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
