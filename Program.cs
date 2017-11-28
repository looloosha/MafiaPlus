using System;

namespace MafiaPlus
{
    class Program
    {
        static void Main(string[] args)
        {
            //Welcome
            Console.WriteLine("Welcome to MafiaPlus! Press any key to begin...");
            Console.ReadKey();
            Console.Clear();

            // Game Sequence Begins

            bool hasQuit = false;

            while (!hasQuit)
            {

                Console.WriteLine("How many people are playing in this round?");

                int numPlayers;

                while (!Int32.TryParse(Console.ReadLine(), out numPlayers))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of players!");
                }
                Console.Clear();

                Round round = new Round(numPlayers);














                Console.WriteLine("Press Q to quit or any other key to get another round going!");
                string quitInput = Console.ReadLine();

                if(quitInput == "q" || quitInput == "Q"){
                    hasQuit = true;
                }

                Console.Clear();
            }


        }
    }
}
