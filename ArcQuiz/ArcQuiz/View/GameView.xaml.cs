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
	public partial class GameView : ContentPage
	{
		public GameView (String fileName, bool isNetwork = false)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            if (!isNetwork)
                BindingContext = new QuestionViewModel(fileName);
            else
                BindingContext = new QuestionNetworkViewModel(fileName);

		}
	}
}