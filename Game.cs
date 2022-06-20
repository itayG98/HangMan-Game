using System;
using System.IO;
using System.Text;

namespace HangManWithGameClass
{
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

        private const int EnglishCharecters = 26;
        private const int MAXFAILURES = 10;
        private static Random _random;
        private static readonly string[] _vocabulary;
        private static string[] wordBank;

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
            get { return MAXFAILURES; }
        }

        static Game()
        {
            _random = new Random();
            /*            _vocabulary = new string[] {
                        "doctor", "solider", "Policeman", "bartender", "dentist", "pharmacist", "physician",
                            "veterinarian","teacher", "lawyer", "accountant", "paramedic", "Nurse ,architect"
                            ,"waiter","painter","photographer"};*/
            _vocabulary = LoadWordVacabulary();
        }

        public string TotalGuessedCharectersString()
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
            NewGme();
        }

        public void NewGme()
        /*Initilize the new game state or replay it*/
        {
            TotalGuessedCharecters = new char[EnglishCharecters];
            Level = 1;
            FailsCount = 0;
            TotalGuessCount = 0;
            _state = GameState.Play;
            TotalGuessedCharecters = new char[EnglishCharecters];
            GenarateWord();
            CorrectGuessedCharecters = new char[WordLength];
        }

        public void GuessLetter(char g)
        /*Check letter IFF the game is playing , its a letter and didnt guessed it already
         Then update game state if needed calling CheckGame methods */
        {
            if (char.IsLetter(g) && !IsGuessed(char.ToLower(g)) && State == GameState.Play)
            {
                g = char.ToLower(g);
                TotalGuessedCharecters[TotalGuessCount] = g;
                TotalGuessCount++;
                if (Word.IndexOf(g) != -1)
                {
                    CorrectGuessedCharecters[TotalGuessCount - FailsCount - 1] = g;
                    for (int i = 0; i < WordLength; i++)
                    {
                        if (Word[i] == g)
                        {
                            Current[i] = g;
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

        private bool IsGuessed(char g)
        /*Return whether the letter already guessed*/
        {
            char.ToLower(g);
            foreach (char letter in TotalGuessedCharecters)
            {
                if (letter == g)
                {
                    return true;
                }
            }
            return false;
        }

        private void GenarateWord()
        /* Genarate a random word in lower case*/
        {
            Word = _vocabulary[_random.Next(0, _vocabulary.Length)].ToLower();
            Current = new StringBuilder("");
            Current.Append('_', WordLength);

        }
        private void CheckGame()
        /*Check whether the game state need to change*/
        {
            if (_failsCount >= MAXFAILURES)
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
        {
            string st = File.ReadAllText(@"Assets\Name.txt");
            string separator = @""", """;
            string[] wordsToReturn = st.Split(separator, StringSplitOptions.None);
            return wordsToReturn;
        }
    }
}


