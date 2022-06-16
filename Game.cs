using System;
using System.IO;
using System.Text;

namespace HangManWithGameClass
{
    public class Game
    {
        private enum GameState { Lost, Winnery, Play }
        private GameState state;
        private bool IsLost;
        private string _word;
        private StringBuilder _current;
        private char[] _totalguessedCharecters = new char[EnglishCharecters];
        private char[] _correctGuessedCharecters;
        private int _failsCount;
        private int _totalGuessCount;

        private const int EnglishCharecters = 26;
        private const int MAXFAILURES = 10;
        private static Random _random;
        private static readonly string[] _vocabulary;

        public string Word
        {
            get { return _word; }
            private set { _word = value; }
        }

        public int WordLength
        {
            get { return _word.Length; }
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

        public char[] TotalGuessedCharecters
        {
            get { return _totalguessedCharecters; }
            private set { _correctGuessedCharecters = value; }
        }

        public char[] CorrectGuessedCharecters
        {
            get { return _correctGuessedCharecters; }
            private set { _correctGuessedCharecters = value; }
        }

        static Game()
        {
            _random = new Random();
            _vocabulary = new string[] {
            "doctor", "solider", "Policeman", "bartender", "dentist", "pharmacist", "physician",
                "veterinarian","teacher", "lawyer", "accountant", "paramedic", "Nurse ,architect"
                ,"waiter","painter","photographer"};
        }
        public Game()
        {
            Current = new StringBuilder();
            NewGme();
        }

        private void NewGme()
        {
            IsLost = false;
            GenarateWord();
            CorrectGuessedCharecters = new char[WordLength];
            FailsCount = 0;
            TotalGuessCount = 0;
        }

        public void GuessLetter(char g)
        {
            if (char.IsLetter(g) && state == GameState.Play)
            {
                g = char.ToLower(g);
                /*TODO Non repetative addidng*/
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
                    FailsCount++;
                }
            }
            CheckGame();
        }

        public void GenarateWord()
        /* Genarate a random word in lower case*/
        {
            Word = _vocabulary[_random.Next(0, _vocabulary.Length)].ToLower();
            Current.Append('_', WordLength);

        }
        public void CheckGame()
        {
            if (_failsCount >= MAXFAILURES)
            {
                state = GameState.Lost;
            }
            else if (Word == Current.ToString())
            {
                state = GameState.Winnery;
            }
        }

        public string GetCurrent()
        {
            return Current.ToString();
        }
    }
}






/*        private static string[] readWordsFromFile()

        //Reads the names text file for the vocalulary
        //the seperator ", "
        //Origanly from https://github.com/aruljohn/popular-baby-names/blob/master/2020/boy_names_2020.json
        //I've saved this data as txt in the Debug/.Net6 directory in order to play enven when 
        // disconected to the internet.
        {
            string st = File.ReadAllText(@"\NamesText.txt");
            string separator = @""", """;
            string[] wordsToReturn = st.Split(separator, StringSplitOptions.None);
            return wordsToReturn;
        }*/
