using ArcQuiz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcQuiz
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NursePage : ContentPage
	{
		public NursePage ()
		{
			InitializeComponent ();

            BindingContext = new QuestionViewModel();
        }
	}
}