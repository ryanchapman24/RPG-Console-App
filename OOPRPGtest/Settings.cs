using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Settings
    {
        public List<Monster> Monsters { get; set; }
        public Shop Shop { get; set; }

        public Settings(Shop shop)
        {
            this.Monsters = new List<Monster>();
            this.Shop = shop;
        }

        public void ShowSettings()
        {
            Console.Clear();
            Console.WriteLine("********** Settings **********");
            Console.WriteLine("1. Add new monster");
            Console.WriteLine("2. Add new weapon");
            Console.WriteLine("3. Add new armor");
            Console.WriteLine();
            Console.WriteLine("Here are the available monsters for this game session:");
            Console.WriteLine();
            int counter = 1;
            foreach (var m in Monsters)
            {
                Console.WriteLine(counter + ". " + m.Name);
                counter++;
            }
            Console.WriteLine();
            Console.WriteLine("Press any other key to return to main menu.");
            var input = Console.ReadLine();
            if (input == "")
            {
                Shop.Game.Main();
            }
            if (input == "1")
            {
                Console.Clear();
                Console.WriteLine("Enter the name of the monster");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the strength of the monster (1 ~ 100)");
                var strength = int.Parse(Console.ReadLine());
                if (strength > 100)
                {
                    strength = 100;
                }
                Console.WriteLine("Enter the defense of the monster (1 ~ 100)");
                var defense = int.Parse(Console.ReadLine());
                if (defense > 100)
                {
                    defense = 100;
                }
                Console.WriteLine("Enter the original health points of the monster (1 ~ 100)");
                var originalhp = int.Parse(Console.ReadLine());
                if (originalhp > 100)
                {
                    originalhp = 100;
                }
                Random rnd = new Random();
                int currency = rnd.Next(1, 201);
                AddMonster(name, strength, defense, originalhp, currency);
                ShowSettings();
            }
            else if (input == "2")
            {
                Console.Clear();
                Console.WriteLine("Enter the name of the weapon");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the strength of the weapon (1 ~ 100)");
                var strength = int.Parse(Console.ReadLine());
                if (strength > 100)
                {
                    strength = 100;
                }
                Console.WriteLine("Enter the value of the weapon (1 ~ 200)");
                var value = int.Parse(Console.ReadLine());
                if (value > 200)
                {
                    value = 200;
                }
                Shop.AddWeapons(name, strength, value);
                this.ShowSettings();
            }
            else if (input == "3")
            {
                Console.Clear();
                Console.WriteLine("Enter the name of the armor");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the defense of the armor (1 ~ 100)");
                var defense = int.Parse(Console.ReadLine());
                if (defense > 100)
                {
                    defense = 100;
                }
                Console.WriteLine("Enter the value of the armor (1 ~ 200)");
                var value = int.Parse(Console.ReadLine());
                if (value > 200)
                {
                    value = 200;
                }
                Shop.AddArmors(name, defense, value);
                this.ShowSettings();
            }
            Shop.Game.Main();
        }

        public void AddMonster(string name, int strength, int defense, int originalhp, int currency)
        {
            Monster monster = new Monster(name, strength, defense, originalhp, currency);
            Monsters.Add(monster);
        }
    }
}
