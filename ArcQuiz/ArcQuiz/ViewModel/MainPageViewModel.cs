using ArcQuiz.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ArcQuiz.ViewModel
{
    class MainPageViewModel
    {
        private Command openCategoryView;
        public Command OpenCategoryView
        {
            get
            {
                return openCategoryView ?? (openCategoryView = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new CategoryView())));
            }
        }

        private Command openScoreView;
        public Command OpenScoreView
        {
            get
            {
                return openScoreView ?? (openScoreView = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new ScoreView())));
            }
        }
    }
}
