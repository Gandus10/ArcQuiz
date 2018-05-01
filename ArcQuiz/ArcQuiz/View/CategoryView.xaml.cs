using ArcQuiz.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcQuiz.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryView : ContentPage
	{
		public CategoryView (bool isNetwork)
		{
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new CategoryViewModel(isNetwork);
        }
    }
}