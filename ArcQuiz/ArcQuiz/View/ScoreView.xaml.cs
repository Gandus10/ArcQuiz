using ArcQuiz.Model;
using ArcQuiz.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcQuiz.View
{
	public partial class ScoreView : ContentPage
	{
        //public ObservableCollection<ScoreModel> Scores { get; set; } = new ObservableCollection<ScoreModel>();

        public ScoreView()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new ScoreViewModel();
        }
    }
}
