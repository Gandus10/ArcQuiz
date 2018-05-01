using ArcQuiz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcQuiz.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameResultView : ContentPage
	{
		public GameResultView (double score, string fileName)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new GameResultViewModel(score, fileName);
        }
	}
}