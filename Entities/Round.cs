using System;
using System.Collections.Generic;
using MafiaPlus.Entities;
using MafiaPlus.Enumerations;
namespace MafiaPlus
{
    public class Round : IPrintable
    {
        private int totalPlayers;
        private int numberOfMafia;
        private int numberOfDoctor;
        private int numberOfSheriff;
        private int numberOfCivilian;
        public List<Player> playersAlive = new List<Player>();
        private List<ROLE> availableRoles = new List<ROLE>();
        private int currCivilian, currMafia, currDoctor, currSherrif = 0;
        private static Random roleRand = new Random();

        // Asks user for input to setup an instance of one Round
        public Round(int totalPlayers)
        {
            this.totalPlayers = totalPlayers;

            availableRoles.Add(ROLE.Civilian);
            availableRoles.Add(ROLE.Mafia);
            availableRoles.Add(ROLE.Doctor);
            availableRoles.Add(ROLE.Sheriff);

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

            Console.WriteLine("Thanks! We're all set and ready to start a game of Mafia!. Give the device back to the MODERATOR");
            Console.WriteLine("Moderator, press any key to commence the game...");

            Console.ReadKey();
            Console.Clear();
        }

        public void getPlayerNamesAndAssignRandomRoles(){

            int count = 1;

            while(count <= totalPlayers){

                Console.WriteLine("Name for Player " + count  + ":");

                ROLE currentRole = getRandomAvailableRole();
                Player p = null;

                if(currentRole == ROLE.Civilian){
                    p = new Player(Console.ReadLine(), ROLE.Civilian);
                    playersAlive.Add(p);
                }
                else if(currentRole == ROLE.Doctor){
                    p = new Doctor(Console.ReadLine());
                    playersAlive.Add(p);
                }
                else if (currentRole == ROLE.Mafia){
                    p = new Mafia(Console.ReadLine());
                    playersAlive.Add(p);
                }
                else if (currentRole == ROLE.Sheriff)
                {
                    p = new Sheriff(Console.ReadLine());
                    playersAlive.Add(p);
                }

                count++;

                Console.WriteLine("Thanks " + p.name + "! You're role is " + currentRole.ToString() + 
                                  ". When you're ready to pass the device to the next person, press any key to clear the screen so that they don't see your role!");

                Console.ReadKey();
                Console.Clear();
            }

        }

        public ROLE getRandomAvailableRole(){

            int r = roleRand.Next(availableRoles.Count);
            ROLE randomRole = availableRoles[r];

            if(randomRole == ROLE.Civilian){
                currCivilian++;
                if(currCivilian == numberOfCivilian){
                    availableRoles.Remove(ROLE.Civilian);
                }
            }

            if (randomRole == ROLE.Mafia)
            {
                currMafia++;
                if (currMafia == numberOfMafia)
                {
                    availableRoles.Remove(ROLE.Mafia);
                }
            }

            if (randomRole == ROLE.Doctor)
            {
                currDoctor++;
                if (currDoctor == numberOfDoctor)
                {
                    availableRoles.Remove(ROLE.Doctor);
                }
            }

            if (randomRole == ROLE.Sheriff)
            {
                currSherrif++;
                if (currSherrif == numberOfSheriff)
                {
                    availableRoles.Remove(ROLE.Sheriff);
                }
            }

            return randomRole;

        }


        public void print(){

            Console.WriteLine("Players Alive");
            Console.WriteLine("-------------");

            foreach(Player p in playersAlive){
                p.print();
            }
        }


        public void runGameSequence(){

            ROLE winner = ROLE.None;
            string playerToMurder = "";
            string playerToHeal = "";
            string playerToAccuse = "";

            while(checkWinCondition() == ROLE.None){

                Console.WriteLine("Announce for the town to go to sleep. Then tell Mafia to awake and choose player to murder.");

                print();

                Console.WriteLine("Type the name of the person that the mafia murdered:");
                playerToMurder = Console.ReadLine();

                while (!isValidPlayerAlive(playerToMurder)){
                    ;
                    Console.WriteLine("That is not a valid player to kill. Retry!");

                    playerToMurder = Console.ReadLine();
                }

                Console.Clear();

                if(numberOfDoctor > 0){
                    Console.WriteLine("Announce for the mafia to go back to sleep and the doctor to awake and to choose a player to heal");

                    print();

                    Console.WriteLine("Type the name of the person that the doctor healed:");

                    playerToHeal = Console.ReadLine();

                    while (!isValidPlayerAlive(playerToHeal)){
                        Console.WriteLine("That is not a valid player to heal. Retry!");

                        playerToHeal = Console.ReadLine();
                    }

                    Console.Clear();
                }


                if (numberOfSheriff > 0)
                {

                    Console.WriteLine("Announce for the doctor to go back to sleep and the sheriff to awake and to choose a player to accuse");

                    print();

                    Console.WriteLine("Type the name of the person that the sheriff accused:");

                    playerToAccuse = Console.ReadLine();

                    while (!isValidPlayerAlive(playerToAccuse))
                    {
                        Console.WriteLine("That is not a valid player to accuse. Retry!");

                        playerToAccuse = Console.ReadLine();
                    }

                    Console.Clear();
                }

                Console.WriteLine("The mafia murdered " + playerToMurder);
                Console.WriteLine("The doctor healed " + playerToHeal);
                Console.WriteLine("The sheriff accused " + playerToAccuse);

                if (playerToMurder != playerToHeal)
                {
                    Player murdered = getAlivePlayerByName(playerToMurder);
                    Mafia.kill(murdered, this);
                    if (murdered.role == ROLE.Mafia){
                        numberOfMafia--;
                    }else if(murdered.role == ROLE.Doctor){
                        numberOfDoctor--;
                    }else if (murdered.role == ROLE.Sheriff)
                    {
                        numberOfSheriff--;
                    }
                    else if (murdered.role == ROLE.Civilian)
                    {
                        numberOfCivilian--;
                    }
                }

                Console.WriteLine("Towns-people must now choose someone to hang");

                print();

                Console.WriteLine("Type the name of the player that the towns-people want to hang:");

                string playerToHang = Console.ReadLine();

                while (!isValidPlayerAlive(playerToHang)){
                    Console.WriteLine("That is not a valid player to hang. Retry!");

                    playerToHang = Console.ReadLine();
                }

                Player hung = getAlivePlayerByName(playerToHang);

                if (hung.role == ROLE.Mafia)
                {
                    numberOfMafia--;
                }
                else if (hung.role == ROLE.Doctor)
                {
                    numberOfDoctor--;
                }
                else if (hung.role == ROLE.Sheriff)
                {
                    numberOfSheriff--;
                }
                else if (hung.role == ROLE.Civilian)
                {
                    numberOfCivilian--;
                }

                playersAlive.Remove(getAlivePlayerByName(playerToHang));
                //Mafia.kill(hung, this);

                winner = checkWinCondition();
                playerToMurder = "";
                playerToHeal = "";
                playerToAccuse = "";
            }

            Console.WriteLine(winner.ToString() + " wins!");

        }

        private Player getAlivePlayerByName(string name){
            foreach(Player p in playersAlive){
                if (p.name == name)
                    return p;
            }

            return null;
        }

        private ROLE checkWinCondition(){

            if (numberOfMafia == 0)
                return ROLE.Civilian;
            if (numberOfCivilian + numberOfDoctor + numberOfSheriff == 0)
                return ROLE.Mafia;


            return ROLE.None;
        }

        private bool isValidPlayerAlive(string name){
            bool result = false;

            foreach(Player p in playersAlive){
                if (p.name == name)
                    result = true;
            }

            return result;
        }

    }
}
