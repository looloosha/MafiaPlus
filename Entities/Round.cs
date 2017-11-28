using System;
using System.Collections.Generic;
using MafiaPlus.Entities;
using MafiaPlus.Enumerations;
namespace MafiaPlus
{
    public class Round
    {
        private int totalPlayers;
        private int numberOfMafia;
        private int numberOfDoctor;
        private int numberOfSheriff;
        private int numberOfCivilian;
        private List<Player> playersAlive = new List<Player>();


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
            while (!Int32.TryParse(Console.ReadLine(), out numberOfSheriff))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of Sherrifs!");
            }

            numberOfCivilian = totalPlayers - numberOfDoctor - numberOfSheriff - numberOfMafia;

            Console.Clear();
            Console.WriteLine("Next we'll need the name of each player:");

            getPlayerNamesAndAssignRandomRoles(); 

            Console.WriteLine("Thanks! We're all set.");

        }

        public void getPlayerNamesAndAssignRandomRoles(){

            int count = 1;

            while(count != totalPlayers){

                Console.WriteLine("Name for Player " + count  + ":");

                ROLE currentRole = getRandomRole();

                if(currentRole == ROLE.Civilian){
                    Player p = new Player(Console.ReadLine(), ROLE.Civilian);
                    playersAlive.Add(p);
                }
                else if(currentRole == ROLE.Doctor){
                    Doctor d = new Doctor(Console.ReadLine());
                    playersAlive.Add(d);
                }
                else if (currentRole == ROLE.Mafia){
                    Mafia m = new Mafia(Console.ReadLine());
                    playersAlive.Add(m);
                }
                else if (currentRole == ROLE.Sheriff)
                {
                    Sheriff s = new Sheriff(Console.ReadLine());
                    playersAlive.Add(s);
                }
            }

        }

        public ROLE getRandomRole(){
            
            return ROLE.Civilian;
        }

    }
}
