using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Fight
    {
        public Game Game { get; set; }
        public Hero Hero { get; set; }
        public Settings Settings { get; set; }
        public Monster Monster { get; set; }

        public Fight(Hero hero, Game game, Settings settings)
        {
            this.Game = game;
            this.Hero = hero;
            this.Settings = settings;
        }

        public void Start()
        {
            if (Settings.Monsters.Count() > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(1, Settings.Monsters.Count() + 1);
                this.Monster = Settings.Monsters[index - 1];

                Console.WriteLine("You've encountered the " + Monster.Name + "!");
                Console.WriteLine("---------------");
                Console.WriteLine("Strength: " + Monster.Strength);
                Console.WriteLine("Defense: " + Monster.Defense);
                Console.WriteLine("HP: " + Monster.OriginalHP);
                Console.WriteLine("Currency: " + Monster.Currency);
                Console.WriteLine("---------------");
                Console.WriteLine();
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("You've encountered the " + Monster.Name + "!");
                Console.WriteLine("Choose your own fate, " + Hero.Name + ":");
                Console.WriteLine("1. Fight the " + Monster.Name);
                Console.WriteLine("2. Run");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    var round = Hero.MonstersKilled + 1;
                    sp.Speak("Round " + round + ", fight!");
                    Console.Clear();
                    this.FightMonster();
                }
                else if (input == "2")
                {
                    Console.Clear();
                    this.Run();
                }
            }
            else
            {
                Console.WriteLine("There are no more monsters to fight! Press any key to return to the main menu");
                Console.ReadKey(true);
                Game.Main();
            }
        }

        public void FightMonster()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 3);
            if (number == 1)
            {
                Random miss = new Random();
                var monDef = Convert.ToInt32(Math.Round(Convert.ToDouble(Monster.Defense) / 10));
                var herDef = Convert.ToInt32(Math.Round(Convert.ToDouble(Hero.Defense) / 10));
                while (Hero.CurrentHP > 0 && Monster.CurrentHP > 0)
                {
                    if (Hero.CurrentHP > 0)
                    {
                        var heroDamage = Hero.Strength - monDef;
                        if (heroDamage <= 0)
                        {
                            heroDamage = 1;
                        }
                        var chanceOfHeroMiss = miss.Next(1, 101);
                        if (chanceOfHeroMiss <= 10)
                        {
                            heroDamage = 0;
                        }
                        this.HeroAttack(heroDamage);
                        if (heroDamage > 0)
                        {
                            Console.WriteLine(">>> " + Hero.Name + " damaged " + Monster.Name + " with a " + Hero.EquippedWeapon.Name + " for " + heroDamage + " health points");
                        }
                        else
                        {
                            Console.WriteLine(">>> " + Hero.Name + " missed!");
                        }
                    }
                    if (Monster.CurrentHP > 0)
                    {
                        var monsterDamage = Monster.Strength - herDef;
                        if (monsterDamage <= 0)
                        {
                            monsterDamage = 1;
                        }
                        var chanceOfMonsterMiss = miss.Next(1, 101);
                        if (chanceOfMonsterMiss <= 10)
                        {
                            monsterDamage = 0;
                        }
                        this.MonsterAttack(monsterDamage);
                        if (monsterDamage > 0)
                        {
                            Console.WriteLine(Monster.Name + " damaged " + Hero.Name + " for " + monsterDamage + " health points");
                        }
                        else
                        {
                            Console.WriteLine(Monster.Name + " missed!");
                        }
                    }
                }
            }
            else if (number == 2)
            {
                Random miss = new Random();
                var monDef = Convert.ToInt32(Math.Round(Convert.ToDouble(Monster.Defense) / 10));
                var herDef = Convert.ToInt32(Math.Round(Convert.ToDouble(Hero.Defense) / 10));
                while (Hero.CurrentHP > 0 && Monster.CurrentHP > 0)
                {
                    if (Monster.CurrentHP > 0)
                    {
                        var monsterDamage = Monster.Strength - herDef;
                        if (monsterDamage <= 0)
                        {
                            monsterDamage = 1;
                        }
                        var chanceOfMonsterMiss = miss.Next(1, 101);
                        if (chanceOfMonsterMiss <= 10)
                        {
                            monsterDamage = 0;
                        }
                        this.MonsterAttack(monsterDamage);
                        if (monsterDamage > 0)
                        {
                            Console.WriteLine(Monster.Name + " damaged " + Hero.Name + " for " + monsterDamage + " health points");
                        }
                        else
                        {
                            Console.WriteLine(Monster.Name + " missed!");
                        }
                    }
                    if (Hero.CurrentHP > 0)
                    {
                        var heroDamage = Hero.Strength - monDef;
                        if (heroDamage <= 0)
                        {
                            heroDamage = 1;
                        }
                        var chanceOfHeroMiss = miss.Next(1, 101);
                        if (chanceOfHeroMiss <= 10)
                        {
                            heroDamage = 0;
                        }
                        this.HeroAttack(heroDamage);
                        if (heroDamage > 0)
                        {
                            Console.WriteLine(">>> " + Hero.Name + " damaged " + Monster.Name + " with a " + Hero.EquippedWeapon.Name + " for " + heroDamage + " health points");
                        }
                        else
                        {
                            Console.WriteLine(">>> " + Hero.Name + " missed!");
                        }
                    }
                }
            }
            if (Hero.CurrentHP > 0 && Monster.CurrentHP <= 0)
            {
                Hero.Currency = Hero.Currency + Monster.Currency;
                Hero.MonstersKilled = Hero.MonstersKilled + 1;
                Settings.Monsters.Remove(Monster);
                Console.WriteLine();
                Console.WriteLine(Hero.Name + " defeated the " + Monster.Name + " with " + Hero.CurrentHP + " health points remaining and obtained " + Monster.Currency + " currency from the fight.");
                Console.WriteLine();
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                if (Hero.MonstersKilled == 1)
                {
                    sp.Speak("You emerged victorious. " + Hero.MonstersKilled + " monster down.");
                }
                else
                {
                    sp.Speak("You emerged victorious. " + Hero.MonstersKilled + " monsters down.");
                }
                if (Settings.Monsters.Count() > 0)
                {
                    Console.WriteLine("1. Take your chances and fight another monster");
                    Console.WriteLine("2. Return to the main menu");
                }
                else
                {
                    Console.WriteLine("There are no more monsters to fight! Press any key to return to the main menu.");
                    Console.ReadKey(true);
                    Game.Main();
                }
                var input = Console.ReadLine();
                if (input == "1")
                {
                    Console.Clear();
                    this.Start();
                }
                else if (input == "2")
                {
                    Console.Clear();
                    this.Game.Main();
                }
            }
            else if (Monster.CurrentHP > 0 && Hero.CurrentHP <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("The " + Monster.Name + " defeated " + Hero.Name + " with " + Monster.CurrentHP + " health points remaining.");
                Console.WriteLine();
                Console.WriteLine("---------------");
                Console.WriteLine("   GAME OVER   ");
                Console.WriteLine("---------------");
                SpeechSynthesizer sp = new SpeechSynthesizer();
                sp.SetOutputToDefaultAudioDevice();
                sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                sp.Speak("GAME OVER");
                Console.WriteLine();
                Console.WriteLine("1. Start a new game");
                Console.WriteLine("2. Exit");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    Console.Clear();
                    Game game = new Game();
                    game.Start();
                }
                else if (input == "2")
                {
                    sp.Speak("Peace out fool");
                    Environment.Exit(0);
                }
                sp.Speak("Peace out fool");
                Environment.Exit(0);
            }
            Console.ReadKey();
        }

        public void Run()
        {
            var penalty = Convert.ToInt32(Math.Round(Convert.ToDouble(Hero.Currency) / 10 * 2));
            if (Hero.Currency == 1 || Hero.Currency == 2)
            {
                penalty = 1;
            }
            Hero.Currency = Hero.Currency - penalty;
            Console.WriteLine(Hero.Name + " runs away and screams like a little girl.");
            Console.WriteLine("Penalty: -" + penalty + " currency");
            Console.WriteLine();
            SpeechSynthesizer sp = new SpeechSynthesizer();
            sp.SetOutputToDefaultAudioDevice();
            sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
            sp.Speak("You are a disgrace to the realm.");
            Console.WriteLine("1. Take your chances and fight another monster");
            Console.WriteLine("2. Return to the main menu");
            var input = Console.ReadLine();
            if (input == "1")
            {
                Console.Clear();
                this.Start();
            }
            else if (input == "2")
            {
                Console.Clear();
                this.Game.Main();
            }
        }

        public void HeroAttack(int heroDamage)
        {
            Monster.CurrentHP = Monster.CurrentHP - heroDamage;
            Thread.Sleep(800);
        }

        public void MonsterAttack(int monsterDamage)
        {
            Hero.CurrentHP = Hero.CurrentHP - monsterDamage;
            Thread.Sleep(800);
        }
    }
}
