using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test
            Player player = new Player("Raph");
            Weapon weap1 = new Weapon("gauntlet");
            
            Console.WriteLine(weap1.Name);

            string[] enemyOneArt = new string[] {"HP: {0}\\{1}","", "o~\\", "|_-__\\", "", "{2}" };
            Weapon weap2 = new Weapon("pistol");
            Console.WriteLine(weap2.Name);

            Potion pot1 = new Potion(1,1);
            Potion pot2 = new Potion(0,1);
            player.PrintStats();



            //Player class Test
            player.Exp = 120;
            Console.WriteLine("Exp: {0}", player.Exp);

            player.PrintInvent();

            
            
            player.AddPotionToInvent(pot1);
            player.AddPotionToInvent(pot2);
            player.AddPotionToInvent(pot2);
            player.AddPotionToInvent(pot2);
            player.AddWeaponToInvent(weap2);
            player.EquipWeapon(0);
            player.EquipWeapon(1);
            player.ChangeWeapons(0, 1);
            player.AddWeaponToInvent(weap2);

            player.PrintStats();

            player.PrintInvent();

            player.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", player.Health);

            player.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", player.Health);

            player.DrinkPotion(0);

            player.PrintInvent();

            player.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", player.Health);
            
            Printer printer = new Printer();

            BasicEnemy[] enemies = new BasicEnemy[3];
            //enemies[0] = new BasicEnemy("tmp ", printer);
            //enemies[1] = new BasicEnemy("tmp ", printer);
            //enemies[2] = new BasicEnemy("tmp ", printer);


            //Starts Game
            GameLoop();
        }
        static void GameLoop()
        {
            while (true)
            {
                Printer printer = new Printer();

                Player thePlayer = new Player(ChooseName(printer));
                Console.WriteLine("Got here");
                OverWorldManager worldManager = new OverWorldManager(thePlayer);
                worldManager.DrawUi();

                Console.Clear();
                printer.PrintSingle("Hah I knew you'd try again!");
            }
            
        }


        static string ChooseName(Printer thePrinter)
        {
            //loops until valid input
            while (true)
            {
                //prompts for user input to choose name
                thePrinter.PrintSingle("What shall I call you hero?", true, true);
                string tempName = Console.ReadLine();
                //check to make sure the string is not empty
                if (tempName != "")
                {
                    //Finally asks player to confirm name choice, returning to the start if they select no
                    thePrinter.PrintSingle("{0}. Is this what you wish to be called? 1: yes / 2: no", true, true, tempName);
                    int choice = ReturnChoice(thePrinter);
                    if (choice == 1 || choice == 2)
                    {
                        if (choice == 1)
                        {
                            return tempName;
                        }
                        thePrinter.PrintSingle("Of course, what will it be then?", true, true);
                    }
                    else
                    {
                        //Prompts for valid input
                        thePrinter.PrintSingle("Invalid choice ", true, false);
                        thePrinter.PrintSingle("Please confirm with 1: yes / 2: no", false, true);
                    }

                }
                else
                {
                    //Prints if the player enteres nothing, prompting them to name the pet something.
                    thePrinter.PrintSingle("You must have a name Hero! How else will the land know who saved them!", true, true);
                }
            }
        }

        static int ReturnChoice(Printer thePrinter)
        {
            while (true)
            {
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                    thePrinter.PrintSingle("Invalid selection, please use a number!", true, true);
                }
            }
        }
    }
}
