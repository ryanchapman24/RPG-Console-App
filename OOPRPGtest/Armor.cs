using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Armor
    {
        public string Name { get; set; }
        public int Defense { get; set; }
        public int Value { get; set; }
        public Special Special { get; set; }

        public Armor()
        {
            this.Name = "Broken Shield";
            this.Defense = 5;
            this.Value = 5;
        }

        public Armor(string name, int defense, int value)
        {
            this.Name = name;
            this.Defense = defense;
            this.Value = value;
        }

    }
}
