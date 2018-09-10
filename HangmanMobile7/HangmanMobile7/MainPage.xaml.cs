using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HangmanMobile7
{
	public partial class MainPage : ContentPage
	{

        /*todo: test ios functionality
         *    - refinish UI
         *          >red color is: #e74c3cff
         *          >turquise color is: #1abc9cff
         *          >grey color is: #b7b7b7
         */


        public MainPage()
		{
			InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
		}


        async void SinglePlayerButton_Clicked(object sender, EventArgs e)
        {
            string word = "garlic";
            await Navigation.PushAsync(new Guessing(word), true);
        }

        async void TwoPlayerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WordEntry(), true);
        }
    }
}
