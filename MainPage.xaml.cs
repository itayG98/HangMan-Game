using System;
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
        public MainPage()
        {
            this.InitializeComponent();
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
            if (hangman.State == Game.GameState.Play)
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
                case Game.GameState.Winnery:
                    {
                        Win();
                        break;
                    }
                case Game.GameState.Lost:
                    {
                        Lose();
                        break;
                    }
            }
            showHangedMan(hangman.FailsCount);
        }

        private void showHangedMan(int failsCount)
        {
            switch (failsCount)
            {
                case 0:
                    {
                        _0.Visibility = Visibility.Visible;
                        break;
                    }
                case 1:
                    {
                        _0.Visibility = Visibility.Visible;
                        _1.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        _0.Visibility = Visibility.Visible;
                        _1.Visibility = Visibility.Visible;
                        _2.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        _1.Visibility = Visibility.Visible;
                        _2.Visibility = Visibility.Visible;
                        _3.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        _2.Visibility = Visibility.Visible;
                        _3.Visibility = Visibility.Visible;
                        _4.Visibility = Visibility.Visible;
                        break;
                    }
                case 5:
                    {
                        _3.Visibility = Visibility.Visible;
                        _4.Visibility = Visibility.Visible;
                        _5.Visibility = Visibility.Visible;
                        break;
                    }
                case 6:
                    {
                        _4.Visibility = Visibility.Visible;
                        _5.Visibility = Visibility.Visible;
                        _6.Visibility = Visibility.Visible;
                        break;
                    }
                case 7:
                    {
                        _5.Visibility = Visibility.Visible;
                        _6.Visibility = Visibility.Visible;
                        _7.Visibility = Visibility.Visible;
                        break;
                    }
                case 8:
                    {
                        _6.Visibility = Visibility.Visible;
                        _7.Visibility = Visibility.Visible;
                        _8.Visibility = Visibility.Visible;
                        break;
                    }
                case 9:
                    {
                        _7.Visibility = Visibility.Visible;
                        _8.Visibility = Visibility.Visible;
                        _9.Visibility = Visibility.Visible;
                        break;
                    }
                case 10:
                    {
                        _8.Visibility = Visibility.Visible;
                        _9.Visibility = Visibility.Visible;
                        _10.Visibility = Visibility.Visible;
                        break;
                    }
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
