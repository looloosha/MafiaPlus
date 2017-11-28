using System;
using MafiaPlus.Enumerations;

namespace MafiaPlus.Entities
{
    public class Player
    {
        public string name;
        public ROLE role;

        public Player(string name, ROLE role){

            this.name = name;
            this.role = role;

        }

    }
}
