using ArcQuiz.Data;
using ArcQuiz.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcQuiz
{
	public partial class App : Application
	{
        static ScoreDataBase database;
		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new MainPageView());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static ScoreDataBase Database
        {
            get
            {
                if(database == null)
                {
                    database = new ScoreDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ScoreSQLite.db3"));
                }
                return database;
            }
        }
	}
}
