using System;
using System.IO;
using System.Text;

namespace HangManWithGameClass
{

    /// <summary>
    /// Itay getahun 1070
    /// /The game class load a vacabulary of 500 names in english and choose random name for the game
    /// It manage the guesses, failours count and guessed array 
    /// </summary>
    public enum GameState { Lost, Winnery, Play }
    public class Game
    {
        private GameState _state;
        private string _word;
        private StringBuilder _current;
        private char[] _totalguessedCharecters;
        private char[] _correctGuessedCharecters;
        private int _failsCount;
        private int _totalGuessCount;
        private int _level;

        private const int _ENGLISH_CHARECTERS = 26;
        private const int _MAXFAILURES = 10;

        private static Random _random;
        private static readonly string[] _vocabulary;

        private string Word
        {
            get { return _word; }
            set { _word = value; }
        }

        public int WordLength
        {
            get { return Word.Length; }
        }

        private StringBuilder Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public int FailsCount
        {
            get { return _failsCount; }
            private set { _failsCount = value; }
        }

        public int TotalGuessCount
        {
            get { return _totalGuessCount; }
            private set { _totalGuessCount = value; }
        }

        public int Level
        {
            get { return _level; }
            private set { _level = value <= 0 || value > 3 ? 1 : value; }
        }

        public GameState State
        {
            get { return _state; }
        }

        public char[] TotalGuessedCharecters
        {
            get { return _totalguessedCharecters; }
            private set { _totalguessedCharecters = value; }
        }

        public char[] CorrectGuessedCharecters
        {
            get { return _correctGuessedCharecters; }
            private set { _correctGuessedCharecters = value; }
        }

        public int MaxFailures
        {
            get { return _MAXFAILURES; }
        }

        static Game()
        /*One random obj and one vacabulary for all instances */
        {
            _random = new Random();
            _vocabulary = LoadWordVacabulary();
        }

        public string GetTotalCharectersGueesed()
        {
            StringBuilder sb = new StringBuilder("");
            foreach (char letter in TotalGuessedCharecters)
            {
                sb.Append(letter + " ");
            }
            return sb.ToString();
        }

        public void ChangeLevel()
        {
            Level++;
        }

        public Game()
        {
            Level = 1;
            NewGme();
        }

        public void NewGme()
        /*Initilize the new game state or replay it*/
        {
            TotalGuessedCharecters = new char[_ENGLISH_CHARECTERS];
            FailsCount = 0;
            TotalGuessCount = 0;
            _state = GameState.Play;
            TotalGuessedCharecters = new char[_ENGLISH_CHARECTERS];
            GenarateWord();
            CorrectGuessedCharecters = new char[WordLength];
        }

        public void GuessLetter(char guessedChar)
        /*Check letter IFF the game is playing , its a letter and didnt guessed it already
         Then update game state if needed calling CheckGame methods */
        {
            if (char.IsLetter(guessedChar) && !IsGuessed(char.ToLower(guessedChar)) && State == GameState.Play)
            {
                guessedChar = char.ToLower(guessedChar);
                TotalGuessedCharecters[TotalGuessCount] = guessedChar;
                TotalGuessCount++;
                if (Word.IndexOf(guessedChar) != -1)
                {
                    CorrectGuessedCharecters[TotalGuessCount - FailsCount - 1] = guessedChar;
                    for (int i = 0; i < WordLength; i++)
                    {
                        if (Word[i] == guessedChar)
                        {
                            Current[i] = guessedChar;
                        }
                    }
                }
                else
                {
                    FailsCount += Level;
                }
            }
            CheckGame();
        }

        private bool IsGuessed(char guessedChar)
        /*Return whether the letter already guessed*/
        {
            char.ToLower(guessedChar);
            foreach (char letter in TotalGuessedCharecters)
            {
                if (letter == guessedChar)
                {
                    return true;
                }
            }
            return false;
        }

        private void GenarateWord()
        /* Genarate a random word in lower case
         initialize an apropriate Current var*/
        {
            Word = _vocabulary[_random.Next(0, _vocabulary.Length)].ToLower();
            Current = new StringBuilder("");
            Current.Append('_', WordLength);
            for (int i = 0; i < WordLength; i++)
            {
                if (Word[i] == ' ')
                    Current[i] = ' ';
                else if (Word[i] == '-')
                {
                    Current[i] = '-';
                }
            }
        }
        private void CheckGame()
        /*Check whether the game state need to change*/
        {
            if (_failsCount >= _MAXFAILURES)
            {
                _state = GameState.Lost;
            }
            else if (Word == Current.ToString())
            {
                _state = GameState.Winnery;
            }
        }

        public string GetCurrentString()
        /*Convert the StringBuilder Current to string and return it*/
        {
            return Current.ToString();
        }

        public static string[] LoadWordVacabulary()
        /*Read local text file with names*/
        {
            string st = File.ReadAllText(@"Assets\Name.txt");
            string separator = @""", """;
            string[] wordsToReturn = st.Split(separator, StringSplitOptions.None);
            return wordsToReturn;
        }
    }
}


