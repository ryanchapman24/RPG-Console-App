using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Hero
    {
        //properties
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public int MonstersKilled { get; set; }
        public int Currency { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }

        public List<Armor> ArmorsBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }

        /*This is a Constructor.
        When we create a new object from our Hero class, the instance of this class, our hero, has:
        an empty List that has to contain instances of the Armor class,
        an empty List that has to contain instance of the Weapon class,
        stats of the "int" data type, including an intial strength and defense,
        original health points that are going to be the same as the current health points.
        */
        public Hero()
        {
            this.Strength = 10;
            this.Defense = 10;
            this.OriginalHP = 100;
            this.CurrentHP = 100;
            this.MonstersKilled = 0;
            this.Currency = 100;
            Armor armor = new Armor();
            this.EquippedArmor = armor;
            this.ArmorsBag = new List<Armor>();
            this.Defense = this.Defense + armor.Defense;
            Weapon weapon = new Weapon();
            this.EquippedWeapon = weapon;
            this.WeaponsBag = new List<Weapon>();
            this.Strength = this.Strength + weapon.Strength;
        }


        public void ShowStats()
        {
            Console.WriteLine("********* " + Name + " Stats **********");
            Console.WriteLine("Strength: " + Strength + " (+" + EquippedWeapon.Strength + ")");
            Console.WriteLine("Defense: " + Defense + " (+" + EquippedArmor.Defense + ")");
            Console.WriteLine("HP: " + CurrentHP + "/" + OriginalHP);
            Console.WriteLine("Currency: " + Currency);
            Console.WriteLine("Monsters Killed: " + MonstersKilled);
        }

        public void ShowInventory()
        {
            Console.WriteLine("********* " + Name + " Inventory **********");
            Console.WriteLine("Equipped Weapon: " + EquippedWeapon.Name + " (+" + EquippedWeapon.Strength + " Strength)" + " - $" + EquippedWeapon.Value);
            if (EquippedWeapon.Special != null)
            {
                Console.WriteLine("Weapon Mod: " + EquippedWeapon.Special.Name);
            }
            Console.WriteLine();
            Console.WriteLine("Available Weapons: ");
            int aLabel = 1;
            int wLabel = 1;
            foreach (var w in this.WeaponsBag)
            {
                Console.WriteLine(wLabel + "w. " + w.Name + " (+" + w.Strength + " Strength)" + " - $" + w.Value);
                wLabel++;
            }
            Console.WriteLine();
            Console.WriteLine("Equipped Armor: " + EquippedArmor.Name + " (+" + EquippedArmor.Defense + " Defense)" + " - $" + EquippedArmor.Value);
            if (EquippedArmor.Special != null)
            {
                Console.WriteLine("Armor Mod: " + EquippedArmor.Special.Name);
            }
            Console.WriteLine();
            Console.WriteLine("Available Armor: ");
            foreach (var a in this.ArmorsBag)
            {
                Console.WriteLine(aLabel + "a. " + a.Name + " (+" + a.Defense + " Defense)" + " - $" + a.Value);
                aLabel++;
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            this.WeaponsBag.Add(this.EquippedWeapon);
            this.EquippedWeapon = weapon;
            this.WeaponsBag.Remove(weapon);
            this.Strength = 10 + weapon.Strength;
        }

        public void EquipArmor(Armor armor)
        {
            this.ArmorsBag.Add(this.EquippedArmor);
            this.EquippedArmor = armor;
            this.ArmorsBag.Remove(armor);
            this.Defense = 10 + armor.Defense;
        }
    }
}