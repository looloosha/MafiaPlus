using System;
using MafiaPlus.Enumerations;

namespace MafiaPlus.Entities
{
    public class Doctor : Player
    {
        public Doctor(string name) : base(name, ROLE.Doctor){}
    }
}
