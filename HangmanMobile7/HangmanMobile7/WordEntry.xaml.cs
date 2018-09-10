using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HangmanMobile7
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordEntry : ContentPage
	{
		public WordEntry ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            homeImage2.Source = ImageSource.FromFile("homeG4.png");
        }

        async void SwitchToPlayerOne_Clicked(object sender, EventArgs e)
        {
            string word = wordEntryBox.Text;
            await Navigation.PushAsync(new Guessing(word));
        }

        async void GoHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}