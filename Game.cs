using System;
using System.IO;

namespace HangManWithGameClass
{
    public class Game
    {
        private string _currentWord;
        private char[] _TotalguessedCharecters = new char[EnglishCharecters];
        private char[] _correctGuessedCharecters;
        private int _failsCount;
        private int _totalGuessCount;
        private static Random _random;
        private const int EnglishCharecters = 26;
        private const int MAXFAILURES = 10;
        private static readonly string[] _vocabulary;

        public string CurrentWord
        {
            get { return _currentWord; }
            private set { _currentWord = value; }
        }

        public int CurrentWordLength
        {
            get { return _currentWord.Length; }
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
            get { return _TotalguessedCharecters; }
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
                "veterinarian","teacher", "lawyer", "accountant", "paramedic", "Nurse ,architect"};
        }
        public Game()
        {
            GenarateWord();
            FailsCount = 0;
            TotalGuessCount = 0;
        }
        public bool GuessLetter(char g)
        {
            if (!Char.IsLetter(g) || IsLost())
            {
                return false;
            }
            g = Char.ToLower(g);
            AddGuesedCharecter(g);
            int index = CurrentWord.IndexOf(g);
            if (index != -1)
            {
                CorrectGuessedCharecters[index] = g;
                return true;
            }
            else
            {
                FailsCount++;
                return false;
            }
        }

        private void AddGuesedCharecter(char g)
        {
            if (Char.IsLetter(g))
            {
                _TotalguessedCharecters[TotalGuessCount] = g;
                TotalGuessCount++;
            }
        }

        public void GenarateWord()
        /* Genarate a random word in lower case*/
        {
            CurrentWord = _vocabulary[_random.Next(0, _vocabulary.Length)].ToLower();
            _correctGuessedCharecters = new char[CurrentWord.Length];
        }
        public bool IsLost()
        {
            return _failsCount >= MAXFAILURES;
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
