using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Shop
    {
        public Game Game { get; set; }
        public Hero Hero { get; set; }
        public List<Armor> Armors { get; set; }
        public List<Weapon> Weapons { get; set; }

        public Shop(Game game, Hero hero)
        {
            this.Game = game;
            this.Hero = hero;
            Armors = new List<Armor>();
            Weapons = new List<Weapon>();
            AddArmors("Helmet", 10, 15);
            AddArmors("Iron Shield", 20, 25);
            AddArmors("Obsidian Chestplate", 30, 35);
            AddArmors("Magic Helm", 40, 50);
            AddArmors("Shield of Angmar", 50, 60);
            AddArmors("Dragonglass Chestplate", 60, 70);
            AddWeapons("Spear", 10, 15);
            AddWeapons("Battle Axe", 20, 25);
            AddWeapons("Iron Greatsword", 30, 35);
            AddWeapons("Magic Staff", 40, 50);
            AddWeapons("Bow of Legolas", 50, 60);
            AddWeapons("Dragonglass Blade", 60, 70);
        }

        public void AddArmors(string name, int defense, int value)
        {
            Armor armor = new Armor(name, defense, value);
            Armors.Add(armor);
        }

        public void AddWeapons(string name, int strength, int value)
        {
            Weapon weapon = new Weapon(name, strength, value);
            Weapons.Add(weapon);
        }


        public void ShowItems()
        {
            Console.Clear();
            var Specials = new List<Special>();
            var AvailableSpecials = new List<Special>();
            void AddSpecials(string name, string type, int boost, int value)
            {
                Special special = new Special(name, type, boost, value);
                Specials.Add(special);
            }
            if (Hero.CurrentHP == Hero.OriginalHP)
            {
                AddSpecials("Max HP Boost", "Health", 20, 30);
            }
            AddSpecials("Healing Potion", "Health", 10, 10);
            AddSpecials("Great Healing Potion", "Health", 30, 30);
            AddSpecials("Full Healing Potion", "Health", 100, 50);
            AddSpecials("Poison", "Weapon", 5, 10);
            AddSpecials("Acid", "Weapon", 10, 30);
            AddSpecials("Black Mist", "Weapon", 20, 50);
            AddSpecials("Fortify +1", "Armor", 5, 10);
            AddSpecials("Fortify +2", "Armor", 10, 30);
            AddSpecials("Fortify +3", "Armor", 20, 50);
            int aLabel = 1;
            int wLabel = 1;
            int sLabel = 1;
            Console.WriteLine("********** Buy from Shop **********");
            Console.WriteLine("Currency: " + Hero.Currency);
            Console.WriteLine();
            Console.WriteLine("Armor: ");
            foreach (var a in this.Armors)
            {
                Console.WriteLine(aLabel + "a. " + a.Name + " (+" + a.Defense + " Defense)" + " - $" + a.Value);
                aLabel++;
            }
            Console.WriteLine();
            Console.WriteLine("Weapons: ");
            foreach (var w in this.Weapons)
            {
                Console.WriteLine(wLabel + "w. " + w.Name + " (+" + w.Strength + " Strength)" + " - $" + w.Value);
                wLabel++;
            }
            Console.WriteLine();
            Console.WriteLine("Specials: ");
            foreach (var s in Specials)
            {
                if (s.Type == "Health" && s.Name != "Max HP Boost" && Hero.CurrentHP < Hero.OriginalHP)
                {
                    Console.WriteLine(sLabel + "s. " + s.Name + " (+" + s.Boost + " Health Points)" + " - $" + s.Value);
                    AvailableSpecials.Add(s);
                    sLabel++;
                }
                else if (s.Type == "Health" && s.Name == "Max HP Boost" && Hero.CurrentHP == Hero.OriginalHP)
                {
                    Console.WriteLine(sLabel + "s. " + s.Name + " (+" + s.Boost + " Health Points)" + " - $" + s.Value);
                    AvailableSpecials.Add(s);
                    sLabel++;
                }
                else if (s.Type == "Armor" && (Hero.EquippedArmor.Special == null || (Hero.EquippedArmor != null && s.Boost > Hero.EquippedArmor.Special.Boost)))
                {
                    Console.WriteLine(sLabel + "s. " + s.Name + " (+" + s.Boost + " Defense for Equipped Armor)" + " - $" + s.Value);
                    AvailableSpecials.Add(s);
                    sLabel++;
                }
                else if (s.Type == "Weapon" && (Hero.EquippedWeapon.Special == null || (Hero.EquippedWeapon != null && s.Boost > Hero.EquippedWeapon.Special.Boost)))
                {
                    Console.WriteLine(sLabel + "s. " + s.Name + " (+" + s.Boost + " Strength for Equipped Weapon)" + " - $" + s.Value);
                    AvailableSpecials.Add(s);
                    sLabel++;
                }               
            }
            Console.WriteLine();
            Console.WriteLine("Enter item that you want to buy or any other key to return to the main menu.");
            if (Hero.ArmorsBag.Count() > 0 || Hero.WeaponsBag.Count() > 0)
            {
                Console.WriteLine("Enter 's' if you would like to sell any of your items.");
            }
            var input = Console.ReadLine();
            if (input == "")
            {
                Game.Main();
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "a" && Convert.ToInt32(input.Substring(0, 1)) <= this.Armors.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var armor = this.Armors[number - 1];
                if (Hero.Currency >= armor.Value)
                {
                    this.BuyArmor(armor);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have enough currency to purchase the " + armor.Name + ".");
                    Console.ReadKey(true);
                    this.ShowItems();
                }
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "w" && Convert.ToInt32(input.Substring(0, 1)) <= this.Weapons.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var weapon = this.Weapons[number - 1];
                if (Hero.Currency >= weapon.Value)
                {
                    this.BuyWeapon(weapon);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have enough currency to purchase the " + weapon.Name + ".");
                    Console.ReadKey(true);
                    this.ShowItems();
                }
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "s" && Convert.ToInt32(input.Substring(0, 1)) <= AvailableSpecials.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var special = AvailableSpecials[number - 1];
                if (Hero.Currency >= special.Value)
                {
                    this.BuySpecial(special);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have enough currency to purchase the " + special.Name + ".");
                    Console.ReadKey(true);
                    this.ShowItems();
                }
            }
            else if (input == "s" && (Hero.ArmorsBag.Count() > 0 || Hero.WeaponsBag.Count() > 0))
            {
                this.SellItems();
            }
            Game.Main();
        }

        public void SellItems()
        {
            Console.Clear();
            int aLabel = 1;
            int wLabel = 1;
            Console.WriteLine("********** Sell to Shop **********");
            Console.WriteLine("Armor: ");
            foreach (var a in Hero.ArmorsBag)
            {
                Console.WriteLine(aLabel + "a. " + a.Name + " (+" + a.Defense + " Defense)" + " - $" + a.Value);
                aLabel++;
            }
            Console.WriteLine();
            Console.WriteLine("Weapons: ");
            foreach (var w in Hero.WeaponsBag)
            {
                Console.WriteLine(wLabel + "w. " + w.Name + " (+" + w.Strength + " Strength)" + " - $" + w.Value);
                wLabel++;
            }
            Console.WriteLine();
            Console.WriteLine("Enter item that you want to sell or any other key to return to the main menu.");
            Console.WriteLine("Enter 'b' if you would like to buy any items.");
            var input = Console.ReadLine();
            if (input == "")
            {
                Game.Main();
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "a" && Convert.ToInt32(input.Substring(0, 1)) <= Hero.ArmorsBag.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var armor = Hero.ArmorsBag[number - 1];
                this.SellArmor(armor);
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "w" && Convert.ToInt32(input.Substring(0, 1)) <= Hero.WeaponsBag.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var weapon = Hero.WeaponsBag[number - 1];
                this.SellWeapon(weapon);
            }
            else if (input == "b")
            {
                this.ShowItems();
            }
            Game.Main();
        }

        public void BuyWeapon(Weapon weapon)
        {
            this.Weapons.Remove(weapon);
            this.Hero.WeaponsBag.Add(weapon);
            this.Hero.Currency = Hero.Currency - weapon.Value;
        }

        public void BuyArmor(Armor armor)
        {
            this.Armors.Remove(armor);
            this.Hero.ArmorsBag.Add(armor);
            this.Hero.Currency = Hero.Currency - armor.Value;
        }

        public void BuySpecial(Special special)
        {
            if (special.Type == "Health")
            {
                Hero.CurrentHP = Hero.CurrentHP + special.Boost;
                if (Hero.CurrentHP > Hero.OriginalHP && special.Name != "Max HP Boost")
                {
                    Hero.CurrentHP = Hero.OriginalHP;
                }
                Hero.Currency = Hero.Currency - special.Value;
            }
            if (special.Type == "Armor")
            {
                Hero.EquippedArmor.Defense = Hero.EquippedArmor.Defense + special.Boost;
                Hero.EquippedArmor.Special = special;
                Hero.Defense = 10 + Hero.EquippedArmor.Defense;
                Hero.Currency = Hero.Currency - special.Value;
            }
            if (special.Type == "Weapon")
            {
                Hero.EquippedWeapon.Strength = Hero.EquippedWeapon.Strength + special.Boost;
                Hero.EquippedWeapon.Special = special;
                Hero.Strength = 10 + Hero.EquippedWeapon.Strength;
                Hero.Currency = Hero.Currency - special.Value;
            }
        }

        public void SellWeapon(Weapon weapon)
        {
            this.Weapons.Add(weapon);
            this.Hero.WeaponsBag.Remove(weapon);
            this.Hero.Currency = Hero.Currency + weapon.Value;
        }

        public void SellArmor(Armor armor)
        {
            this.Armors.Add(armor);
            this.Hero.ArmorsBag.Remove(armor);
            this.Hero.Currency = Hero.Currency + armor.Value;
        }
    }
}
