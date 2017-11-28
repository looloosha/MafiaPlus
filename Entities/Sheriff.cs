using System;
using MafiaPlus.Enumerations;

namespace MafiaPlus.Entities
{
    public class Sheriff : Player
    {
        public Sheriff(string name) : base(name, ROLE.Sheriff)
        { }


        public static void accuse(Player p)
        {

        }
    }
}