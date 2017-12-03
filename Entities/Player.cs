using System;
using MafiaPlus.Enumerations;

namespace MafiaPlus.Entities
{
    public class Player : IPrintable
    {
        public string name;
        public ROLE role;

        public Player(string name, ROLE role){

            this.name = name;
            this.role = role;

        }

        public void print(){
            Console.WriteLine(name + "  |  " + role.ToString());
        }

    }
}
