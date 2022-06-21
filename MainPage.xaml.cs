using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HangManWithGameClass
{
    /// 
    /// I made a simple UWP Desktop App using one C# class for the game A text file from the internet with 500 names and construct XAML page
    /// divided into three main parts : Hangm draw , the game ststus and a virtual keyboard .
    /// My game response also with keyboard pressing.
    /// 
    // Color from https://colorhunt.co/palette/9eb23bc7d36ffcf9c6e0deca 

    public sealed partial class MainPage : Page
    {
        private Game hangman;
        private List<UIElement> hangManParts;
        private Random r;


        public MainPage()
        {
            this.InitializeComponent();
            HideHangedMan();
            CreateEventForBtn();
            hangman = new Game();
            Window.Current.CoreWindow.KeyDown += KeyDownMethod;
            setUI();
            UpdateGame();
            hangManParts = new List<UIElement> {
               _1,_2,_3,_4,_5,_6,_7,_8,_9,_10
            };
        }

        private void KeyDownMethod(CoreWindow sender, KeyEventArgs args)
        /*Calls keyResponse if game is on with apropriate charecter*/
        {
            if (hangman.State == GameState.Play)
            {
                if (char.TryParse(args.VirtualKey.ToString(), out char key))
                {
                    KeyResponse(key.ToString());
                }
            }
            else
            {
                hangman.NewGme();
                HideHangedMan();
                UpdateGame();
            }
        }

        private void setUI()
        /*Sets the colours*/
        {
            HangManGrid.Background = new SolidColorBrush(Color.FromArgb(200, 252, 249, 198));
            KeysGrid.Background = new SolidColorBrush(Color.FromArgb(200, 158, 178, 59));
            RotateTransform myRotateTransform;
            r = new Random();

            foreach (Button btn in KeysGrid.Children)
            {
                myRotateTransform = new RotateTransform
                {
                    Angle = r.Next(-7, 7)
                };
                btn.RenderTransform = myRotateTransform;
                btn.FontFamily = new FontFamily("Segoe Script");
            }
        }

        public void CreateEventForBtn()
        /*Create event handler to each btn*/
        {
            foreach (Button btn in KeysGrid.Children)
            {
                btn.Click += Btn_Click;
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        /*Calls apropriate method if game is on or replays if game is not on */
        {
            if (hangman.State == GameState.Play)
            {
                Button btn = (Button)sender;
                KeyResponse(btn.Name);
            }
            else
            {
                hangman.NewGme();
                HideHangedMan();
                UpdateGame();
            }
        }

        private void KeyResponse(string key)
        {
            bool isLetter = char.TryParse(key, out char letter);
            if (isLetter)
            {
                hangman.GuessLetter(letter);
            }
            else
            {
                if (key == "Level")
                {
                    hangman.ChangeLevel();
                }
                else if (key == "Replay")
                {
                    HideHangedMan();
                    hangman.NewGme();

                }
            }
            UpdateGame(); ;
        }

        public void UpdateGame()
        /*Update texts and calss show hangman*/
        {
            CurretWord.Text = hangman.GetCurrentString();
            LevelHeadLine.Text = $"Level : {hangman.Level}\n{hangman.WordLength} letrers";
            GueesedLetters.Text = hangman.GetTotalCharectersGueesed();
            switch (hangman.State)
            {
                case GameState.Winnery:
                    {
                        Win();
                        break;
                    }
                case GameState.Lost:
                    {
                        Lose();
                        break;
                    }
            }
            showHangedMan(hangman.FailsCount);
        }

        public void showHangedMan(int failCount)
        /*Shows hangman aproproate parts 
         works for every level*/
        {
            for (int i = failCount - 2; i < hangman.MaxFailures && i < failCount; i++)
            {
                if (i < 0)
                    continue;
                else
                    hangManParts[i].Visibility = Visibility.Visible;
            }
        }

        private void HideHangedMan()
        /*Hides hangman*/
        {
            _1.Visibility = Visibility.Collapsed;
            _2.Visibility = Visibility.Collapsed;
            _3.Visibility = Visibility.Collapsed;
            _4.Visibility = Visibility.Collapsed;
            _5.Visibility = Visibility.Collapsed;
            _6.Visibility = Visibility.Collapsed;
            _7.Visibility = Visibility.Collapsed;
            _8.Visibility = Visibility.Collapsed;
            _9.Visibility = Visibility.Collapsed;
            _10.Visibility = Visibility.Collapsed;
        }

        private void Lose()
        {
            CurretWord.Text += "\nUnfortunately you lost";
        }

        private void Win()
        {
            CurretWord.Text += "\nWell Done";
        }
    }
}
