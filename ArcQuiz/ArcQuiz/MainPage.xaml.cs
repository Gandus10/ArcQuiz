using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcQuiz
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        async void OpenEngineerPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EngineerPage());
        }

        async void OpenNursePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NursePage());
        }
    }
}
