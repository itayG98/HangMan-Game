using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HangManWithGameClass
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
    public sealed partial class MainPage : Page
    {
        private Game hangman;
        private Rect win;
        private TextBlock tx;
        public MainPage()
        {
            this.InitializeComponent();

            hangman = new Game();
            win = ApplicationView.GetForCurrentView().VisibleBounds;
            ExampleGuess();
            tx = GenerateExmpTextBox();
            Can.Children.Add(tx);
            tx.Text += hangman.TotalGuessCount;

        }

        public void ExampleGuess() 
        {
            hangman.GuessLetter('e');
            hangman.GuessLetter('a');
            hangman.GuessLetter('e');
            hangman.GuessLetter('i');
            hangman.GuessLetter('e');
            hangman.GuessLetter('a');
            hangman.GuessLetter('e');
            hangman.GuessLetter('i');
            hangman.GuessLetter('e');
            hangman.GuessLetter('a');
            hangman.GuessLetter('e');
            hangman.GuessLetter('i');
        }

        public TextBlock GenerateExmpTextBox ()
        {
            TextBlock txb = new TextBlock();
            Canvas.SetLeft(txb, win.Width / 2);
            Canvas.SetTop(txb, win.Height / 2);
            txb.Text = hangman.GetCurrent();
            return txb;
        }
    }
}
