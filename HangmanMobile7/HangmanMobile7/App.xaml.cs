using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace HangmanMobile7
{
	public partial class App : Application
    { 

		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage (new MainPage());
		}
  

        protected override void OnStart ()
		{
            // Handle when your app starts

            AppCenter.Start("android=8251e97f-bb5b-43b6-a400-6619fbf40546;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
