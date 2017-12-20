using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Monster
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public int Currency { get; set; }

        public Monster(string name, int strength, int defense, int originalhp, int currency)
        {
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = originalhp;
            CurrentHP = originalhp;
            Currency = currency;
        }
    }
}
