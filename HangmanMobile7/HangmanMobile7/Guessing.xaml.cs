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
	public partial class Guessing : ContentPage
	{

        //the answer will always be "garlic" if the person selects single player

        string answer;
        List<char> letters;
        List<char> garbage;
        int lives;

        //for xaml testing:
        public Guessing()
        {
            InitializeComponent();
            pictura.Source = ImageSource.FromFile("tpp.png");
            homeImage.Source = ImageSource.FromFile("homeG4.png");
            heartImage.Source = ImageSource.FromFile("Heart1.png");
            SetUpGraphic();
        }



        public Guessing (string word)
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);    //removes navigastion bar at top

            lives = 7;

            homeImage.Source = ImageSource.FromFile("homeG4.png");   //sets up images
            pictura.Source = ImageSource.FromFile("tpp.png");
            heartImage.Source = ImageSource.FromFile("Heart1.png");
            SetUpGraphic();

            answer = word;
            //SetAnswer();
            letters = new List<char>();
            garbage = new List<char>();

            for (int i = 0; i < answer.Length; i++)
            {
                letters.Add('_');
            }

            ShowSolution();                         // makes the answer label the correct number of letters _ _ ...

            livesLabel.Text = lives.ToString();
        }



        private void Guessed(object sender, EventArgs e)
        {

            if(LetterEntry.Text != null)
            {
                string l = LetterEntry.Text;

                if (l.Length == 1)
                {
                    char let = char.Parse(l);

                    let = ConvertToLowercase(let);

                    if (!garbage.Contains(let) && !letters.Contains(let))
                    {
                        ContainsAlg(let);                           //these are the steps after go is pressed: (1)
                    }
                    else
                    {
                        DisplayAlert("Invalid", "You have already used that letter", "ok");
                        LetterEntry.Text = "";
                    }

                }
                else
                {
                    LetterEntry.Text = "";
                    DisplayAlert("Invalid", "What you entered is not processable", "ok");
                }
            }
        }


        private void ContainsAlg(char let)          // checks to see if it contains (2)
        {
            if (answer.Contains(let))
            {
                MAlg(let);
            }
            else
            {
                garbage.Add(let);
                string guessedLetters = garbagePile.Text;       //puts used letters at the bottom of the screen
                guessedLetters += " " + let;
                garbagePile.Text = guessedLetters;

                lives -= 1;
                livesLabel.Text = lives.ToString();
                SetUpGraphic();


                if(lives == 0)
                {
                    //DisplayAlert("You Lost!", "Rate the app", "done");
                    BackToHome();
                }
            }

            LetterEntry.Text = "";
        }


        private void MAlg(char let)                 // main algorithm           (3) 
        {                                           //once it gets here, the letter is 100% certain in the word
            IndexAlg(let);

            CheckIfSolved();
        }


        private void IndexAlg(char let)             // gets the indexes of the matching letters in the answer (4) 
        {
            List<int> indexes = new List<int>();

            for (int i = 0; i < answer.Length; i++)
            {
                if ((answer.ElementAt(i).ToString()).Equals(let.ToString()))
                {
                    indexes.Add(i);
                }
            }

            ChangeLetterList(indexes, let);
        }


        private void ChangeLetterList(List<int> indexes1, char let)         // (5)
        {
            for (int i = 0; i < indexes1.Count; i++)
            {
                int dexOfWord = indexes1.ElementAt(i);                      // gets the int of each index of indexes1
                letters[dexOfWord] = let;                                   //replaces the correctly guessed letters with the letter instead of "_"
            }

            ShowSolution();

        }


        private void ShowSolution()                                         // (6)
        {                                                                   //changes the answer label appropriately
            string visible = "";
            for (int i = 0; i < letters.Count; i++)
            {
                visible += letters.ElementAt(i).ToString();
                visible += " ";
            }

            answerLabel.Text = visible;
        }


        async void CheckIfSolved()                                        // (7)  checks if you won!
        {
            string current = "";
            for (int i = 0; i < letters.Count; i++)
            {
                current += letters.ElementAt(i).ToString();
            }

            if (current.Equals(answer))
            {
                await DisplayAlert("You Win", "Rate the app", "done");
                BackToHome();
            }
        }
        
        async void BackToHome()
        {
            await Navigation.PushAsync(new MainPage());
        }


        private void SetUpGraphic()                 //changes the graphic, depending on how many lives
        {
            string[] fileNames = { "MissingHead", "MissingBody", "Missing2Arms",
                                        "MissingArm", "Missing2Legs", "MissingLeg", "full" };

            int pictureNumber = 7 - lives;

            string sourceName = "full.png";

            if(pictureNumber >= 0 && pictureNumber<7)
            {
                sourceName = fileNames[pictureNumber] + ".png";
            }

            mainGraphic.Source = ImageSource.FromFile(sourceName);
        }

        private char ConvertToLowercase(char c)     //if the entered letter is capitalized, this will make it lowercase
        {
            char newChar = c;

            if (c < 97)
            {
                int val = c - 0;
                val += 32;
                char r = (char)val;
                newChar = r;
            }

            return newChar;
        }

        async void GoHome_Clicked(object sender, EventArgs e)
        {
            bool x = await DisplayAlert("Go Home \n", "Are you sure you want to go home?" +
                "\n" +
                "\n" +
                "The current game will be lost." +
                "\n", "yes", "cancel");
            
            if (x)
            {
                BackToHome();
            }
        }
    }
}