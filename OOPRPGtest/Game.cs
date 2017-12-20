using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Game
    {
        //PROPERTY
        public Hero Hero { get; set; }
        public Settings Settings { get; set; }
        public Shop Shop { get; set; }

        //CONSTRUCTOR
        public Game()
        {
            this.Hero = new Hero();
            this.Shop = new Shop(this, this.Hero);
            this.Settings = new Settings(Shop);
            this.Settings.AddMonster("Fluffy Bunny", 20, 20, 40, 40);
            this.Settings.AddMonster("Lord Maximus", 30, 30, 60, 60);
            this.Settings.AddMonster("Mechanical Elf", 40, 40, 80, 80);
            this.Settings.AddMonster("Unbroken Knight", 50, 50, 100, 100);
            this.Settings.AddMonster("Prince of Demons", 60, 60, 120, 150);
            this.Settings.AddMonster("God Slayer", 60, 60, 120, 200);
        }

        //METHOD
        public void Start()
        {
            //Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            SpeechSynthesizer sp = new SpeechSynthesizer();
            sp.SetOutputToDefaultAudioDevice();
            sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
            Console.WriteLine("Welcome Hero!");
            Console.WriteLine("Please enter your name:");
            this.Hero.Name = Console.ReadLine();
            sp.Speak("Hello " + this.Hero.Name);
            this.Main();
        }

        public void Main()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(@"              _                                 ");
            Console.WriteLine(@"  /\/\   __ _(_)_ __     /\/\   ___ _ __  _   _ ");
            Console.WriteLine(@" /    \ / _` | | '_ \   /    \ / _ \ '_ \| | | |");
            Console.WriteLine(@"/ /\/\ \ (_| | | | | | / /\/\ \  __/ | | | |_| |");
            Console.WriteLine(@"\/    \/\__,_|_|_| |_| \/    \/\___|_| |_|\__,_|");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Please choose an option by entering a number");
            Console.WriteLine();
            Console.WriteLine("1. View Stats");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. Visit Shop");
            Console.WriteLine("4. Fight Monster");
            Console.WriteLine("5. Settings");
            Console.WriteLine("6. Exit");
            var input = Console.ReadLine();

            if (input == "1")
            {
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("View Stats");
                this.HeroStats();
            }
            else if (input == "2")
            {
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("View Inventory");
                this.HeroInventory();
            }
            else if (input == "3")
            {
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("Visit Shop");
                this.VisitShop();
            }
            else if (input == "4")
            {
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                if (Hero.MonstersKilled == 0)
                {
                    sp.Speak("And so it begins");
                }
                else if (Hero.MonstersKilled > 0 && Settings.Monsters.Count() > 1)
                {
                    sp.Speak("Back at it again");
                }
                else if (Hero.MonstersKilled > 0 && Settings.Monsters.Count() == 1)
                {
                    sp.Speak("The final challenge");
                }
                this.Fight();
            }
            else if (input == "5")
            {
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("Settings");
                this.VisitSettings();
            }
            else if (input == "6")
            {
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("Peace out fool");
                Environment.Exit(0);
            }
            else
            {
                return;
            }
        }

        public void HeroStats()
        {
            Console.Clear();
            Hero.ShowStats();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            this.Main();
        }

        public void HeroInventory()
        {
            Console.Clear();
            Hero.ShowInventory();
            Console.WriteLine();
            Console.WriteLine("Enter item that you want to equip or any other key to return to the main menu.");
            var input = Console.ReadLine();
            if (input == "")
            {
                this.Main();
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "a" && Convert.ToInt32(input.Substring(0, 1)) <= Hero.ArmorsBag.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var armor = Hero.ArmorsBag[number - 1];
                Hero.EquipArmor(armor);
            }
            else if (input.Length == 2 && input.Substring(input.Length - 1, 1) == "w" && Convert.ToInt32(input.Substring(0, 1)) <= Hero.WeaponsBag.Count())
            {
                var number = Convert.ToInt32(input.Substring(0, 1));
                var weapon = Hero.WeaponsBag[number - 1];
                Hero.EquipWeapon(weapon);
            }
            this.Main();
        }

        public void VisitShop()
        {
            Console.Clear();
            Shop.ShowItems();
        }

        public void Fight()
        {
            Console.Clear();
            Fight fight = new Fight(this.Hero, this, this.Settings);
            fight.Start();
        }

        public void VisitSettings()
        {
            Console.Clear();
            Settings.ShowSettings();
        }
    }
}
