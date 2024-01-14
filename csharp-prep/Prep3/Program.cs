class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;

        while (playAgain)
        {
      
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = -1;
            int guessCount = 0;

            Console.WriteLine("Welcome to the Super  Number game!");

            
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

          
            Console.WriteLine($"Congratulations! You guessed the number in {guessCount} guesses.");

           
            Console.Write("Do you want to play again? (y/n): ");
            string playAgainResponse = Console.ReadLine().ToLower();
            
            
            playAgain = playAgainResponse.StartsWith("y");
        }

        Console.WriteLine("Thanks for playing! See you!!");