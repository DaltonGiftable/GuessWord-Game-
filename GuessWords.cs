using System;
using System.Linq;

namespace GuessWords
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //global restrictions
            const int maxLives = 3;
            const int maxLevels = 3;
            const int maxWords = 5;
            const int maxRounds = 5;

            //game logic
            int totalCorrect = 0;

            for (int round = 1; round <= maxRounds; round++)
            {
                Console.WriteLine($"\nRound {round}:");

                //levels progress restrictions
                int currentLevel = 1;

                while (currentLevel <= maxLevels)
                {
                    int wordsToGuess = currentLevel; //adjusting words based on the current level
                    int lives = maxLives;

                    Console.WriteLine($"\nLevel {currentLevel}:");


                    //player1 sets the word
                    string[] words = GetWordsFromPlayer(wordsToGuess);

                    //player1 sets the hint
                    string hint = GetHintFromPlayer();

                    //player2 starts guessing
                    PlayGuessingGame(words, hint, lives, currentLevel, ref totalCorrect);

                    int correctGuesses = 0;
                    while (correctGuesses < words.Length && lives > 0)
                    {
                        Console.Write($"\nHint: {hint}\n");
                        Console.Write($"Chances left: {lives}. Enter your guess: ");

                        string guess = Console.ReadLine();

                        if (words.Contains(guess))
                        {
                            Console.WriteLine("Correct!");
                            correctGuesses++;
                            totalCorrect++;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect!");
                            lives--;
                        }
                    }

                    if (correctGuesses == words.Length)
                    {
                        Console.WriteLine("Congratulations, you have completed the level!");
                        DisplayLevelScore(correctGuesses, words.Length);
                        currentLevel++; //proceeding to the next level
                    }
                    else
                    {
                        Console.WriteLine("You have ran out of lives. Better luck next time!");
                        DisplayLevelScore(correctGuesses, words.Length);
                        break; //stays on the same level in the next round
                    }
                }
            }

            Console.WriteLine("\nGAME OVER!!!");
            double scorePercentage = CalculateScorePercentage(totalCorrect, maxLevels, maxRounds, maxWords);
            Console.WriteLine($"Your overall score: {scorePercentage:0.00}%");

        }

        static string[] GetWordsFromPlayer(int wordCount)
        {
            string[] words = new string[wordCount];
            for (int i = 0; i < wordCount; i++)
            {
                Console.Write($"Enter word {i + 1}: ");
                words[i] = Console.ReadLine();
            }
            return words;
        }

        static string GetHintFromPlayer()
        {
            Console.Write("Enter a hint for the word: ");
            return Console.ReadLine();
        }

        static void PlayGuessingGame(string[] words, string hint, int lives, int currentLevel, ref int totalCorrect)
        {
            Console.Clear();
            Console.WriteLine("Level {0}: Hint: {1}", currentLevel, hint);

            Console.WriteLine("\nPlayer 2, try to guess the word:");
        }

        static double CalculateScorePercentage(int totalCorrect, int maxLevels, int maxRounds, int maxWords)
        {
            int totalWords = maxLevels * maxRounds * maxWords;
            return (double)totalCorrect / totalWords * 100;
        }

        static void DisplayLevelScore(int correctGuesses, int totalWords)
        {
            double levelScorePercentage = (double)correctGuesses / totalWords * 100;
            Console.WriteLine($"Your score for this level: {levelScorePercentage:0.00}%");
        }
    }
}
