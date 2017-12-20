using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Weapon
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Value { get; set; }
        public Special Special { get; set; }

        public Weapon()
        {
            this.Name = "Butter Knife";
            this.Strength = 5;
            this.Value = 5;
        }

        public Weapon(string name, int strength, int value)
        {
            this.Name = name;
            this.Strength = strength;
            this.Value = value;
        }

    }
}
