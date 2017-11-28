using System;
using MafiaPlus.Enumerations;

namespace MafiaPlus.Entities
{
    public class Mafia : Player
    {
        public Mafia(string name) : base(name, ROLE.Mafia)
        { }


        public static void kill(Player p)
        {

        }
    }
}
