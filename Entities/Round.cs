using System;
namespace MafiaPlus
{
    public class Round
    {
        private int totalPlayers;
        private int numberOfMafia;
        private int numberOfDoctor;
        private int numberOfSheriff;
        private int numberOfCivilian;


        // Asks user for input to setup an instance of one Round
        public Round(int totalPlayers)
        {
            this.totalPlayers = totalPlayers;

            Console.WriteLine("Thanks! Let's get started. A few more questions and we'll start playing a game of Mafia...");

            Console.WriteLine("Amount of Mafia(s)?");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfMafia))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of mafia!");
            }


            Console.WriteLine("Amount of Doctor(s)?");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfDoctor))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of Doctors!");
            }

            Console.WriteLine("Amount of Sheriff(s)?");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfDoctor))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of Sherrifs!");
            }

            numberOfCivilian = totalPlayers - numberOfDoctor - numberOfSheriff - numberOfMafia;

            Console.WriteLine("Thanks! We're all set.");

        }

    }
}
