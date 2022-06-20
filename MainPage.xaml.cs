using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HangManWithGameClass
{
    /// <summary>
    /// HangMan UWP game X86 runs it better 
    /// </summary>
    /// 
    // Color template https://colorhunt.co/palette/9eb23bc7d36ffcf9c6e0deca 

    public sealed partial class MainPage : Page
    {
        private Game hangman;
        private List<UIElement> hangManParts;


        public MainPage()
        {
            this.InitializeComponent();
            hangManParts = new List<UIElement> {
                _0,_1,_2,_3,_4,_5,_6,_7,_8,_9,_10
            };
            HideHangedMan();
            CreateEventForBtn();
            /*Sets the colours*/
            HangManGrid.Background = new SolidColorBrush(Color.FromArgb(200, 252, 249, 198));
            KeysGrid.Background = new SolidColorBrush(Color.FromArgb(200, 158, 178, 59));
            hangman = new Game();
            UpdateGame();
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
        /*play keyboard's apropriate action if game is on or replays if game is not on */
        {
            if (hangman.State == GameState.Play)
            {
                Button b = (Button)sender;
                bool isLetter = char.TryParse(b.Name, out char letter);

                if (isLetter)
                {
                    hangman.GuessLetter(letter);
                }
                else
                {
                    string key = b.Name;
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
                UpdateGame();
            }
            else
            {
                hangman.NewGme();
                UpdateGame();
            }
        }

        public void UpdateGame()
        {
            CurretWord.Text = hangman.GetCurrentString();
            LevelHeadLine.Text = $"Level : {hangman.Level}\n{hangman.WordLength} letrers";
            GueesedLetters.Text = hangman.TotalGuessedCharectersString();
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
        {
            for (int i = failCount - 1; i < hangman.MaxFailures && i < failCount; i++)
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
            _0.Visibility = Visibility.Collapsed;
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
