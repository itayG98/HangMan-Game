using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HangManWithGameClass
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    // Color template https://colorhunt.co/palette/9eb23bc7d36ffcf9c6e0deca 

    public sealed partial class MainPage : Page
    {
        private Game hangman;
        private TextBlock tx;
        public MainPage()
        {
            this.InitializeComponent();
            /*Sets the colours*/
            HangManGrid.Background = new SolidColorBrush(Color.FromArgb(200, 252, 249, 198));
            KeysGrid.Background = new SolidColorBrush(Color.FromArgb(200, 158, 178, 59));

            hangman = new Game();
            CreateEventForBtn();
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
                if (key == "Change Level")
                {
                    hangman.ChangeLevel();
                }
                else if (key == "Replay")
                {
                    hangman.NewGme();
                }
            }
            UpdateGame();
        }

        public void UpdateGame()
        {
            CurretWord.Text = hangman.GetCurrentString();
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
        }

        public void Lose()
        {
            CurretWord.Text += "\nUnfortunately you lost";
        }

        public void Win()
        {
            CurretWord.Text += "\nWell Done";
        }
    }
}
