﻿using ArcQuiz.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ArcQuiz.ViewModel
{
    class CategoryViewModel
    {
       
        private bool isNetwork;
        public CategoryViewModel(bool isNetwork)
        {
            this.isNetwork = isNetwork;
        }

        private Command openNurseView;
        public Command OpenNurseView
        {
            get
            {
                return openNurseView ?? (openNurseView = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new GameView("arc_quiz_nurse.json", isNetwork))));
            }
        }



        private Command openEngineerView;
        public Command OpenEngineerView
        {
            get
            {
                return openEngineerView ?? (openEngineerView = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new GameView("arc_quiz_ingeneer.json", isNetwork))));
            }
        }


    }
}
