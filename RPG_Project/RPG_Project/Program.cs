using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sets console Size
            int origWidth;
            int origHeight;
            int width;
            int height;

            origWidth = Console.WindowWidth;
            origHeight = Console.WindowHeight;
            width = 150;
            height = 50;
            Console.SetWindowSize(width, height);
            //






            //Disabled testing for game loop, can re-enable at any point. Purly for testing start of game

            //Test
            //Player player = new Player("Raph");
            //Weapon weap1 = new Weapon("gauntlet");

            //Console.WriteLine(weap1.Name);

            //string[] enemyOneArt = new string[] {"HP: {0}\\{1}","", "o~\\", "|_-__\\", "", "{2}" };
            //Weapon weap2 = new Weapon("pistol");
            //Console.WriteLine(weap2.Name);

            //Potion pot1 = new Potion(1,1);
            //Potion pot2 = new Potion(0,1);
            //player.PrintStats();

<<<<<<< Updated upstream


            ////Player class Test
            //player.Exp = 120;
            //Console.WriteLine("Exp: {0}", player.Exp);
=======
            //Player class Test
            player.Exp = 420;
            Console.WriteLine("Exp: {0}", player.Exp);

            //Shop test
            Shop shop1 = new Shop();
            shop1.InitShop(player);


            player.PrintInvent();
>>>>>>> Stashed changes

            //player.PrintInvent();



            //player.AddPotionToInvent(pot1);
            //player.AddPotionToInvent(pot2);
            //player.AddPotionToInvent(pot2);
            //player.AddPotionToInvent(pot2);
            //player.AddWeaponToInvent(weap2);
            ////player.EquipWeapon(0);
            ////player.EquipWeapon(1);
            ////player.ChangeWeapons(0, 1);
            //player.AddWeaponToInvent(weap2);

            //player.PrintStats();

            //player.PrintInvent();

            //player.RcvDamage(6);
            //Console.WriteLine("Current Health {0}:", player.Health);

            //player.RcvDamage(6);
            //Console.WriteLine("Current Health {0}:", player.Health);

            //player.DrinkPotion(0);

            //player.PrintInvent();

            //player.RcvDamage(6);
            //Console.WriteLine("Current Health {0}:", player.Health);

            //Printer printer = new Printer();

            //BasicEnemy[] enemies = new BasicEnemy[3];
            //enemies[0] = new BasicEnemy("tmp ", printer);
            //enemies[1] = new BasicEnemy("tmp ", printer);
            //enemies[2] = new BasicEnemy("tmp ", printer);
            //player.Health += 100;
            //Player player = new Player("Test");
            //EnemyGenerator generator = new EnemyGenerator(player);
            //generator.forceEncounter(2);

            var soundPlayer = new SoundPlayer
            {
                SoundLocation = @"D:\0_School Work\GitHub\Intro_To_Html5\New folder\audio\mainMenu.wav"
            };
            Printer printer = new Printer();
            soundPlayer.Play();
            PrintMainMenu(printer, 1, soundPlayer);
           

            //Starts Game

        }

        static void GameLoop()
        {

            

            while (true)
            {
                Console.Clear();
                Weapon weap1 = new Weapon("Sword");
                Printer printer = new Printer();
                Console.Clear();
                Player thePlayer = new Player(ChooseName(printer));

                OverWorldManager worldManager = new OverWorldManager(thePlayer);
                thePlayer.AddWeaponToInvent(weap1);
                Console.ReadLine();
                worldManager.DrawUi();

                Console.Clear();
                DeathChoice(printer, thePlayer);
            }
            
        }

        static void PrintMainMenu(Printer printer, int menu, SoundPlayer soundPlayer)
        {

            string[] TitleArtOne = new string[] { "██████╗░███████╗███╗░░░███╗░█████╗░███╗░░██╗  ██╗░░██╗██╗███╗░░██╗░██████╗░██╗░██████╗", "██╔══██╗██╔════╝████╗░████║██╔══██╗████╗░██║  ██║░██╔╝██║████╗░██║██╔════╝░╚█║██╔════╝",
                "██║░░██║█████╗░░██╔████╔██║██║░░██║██╔██╗██║  █████═╝░██║██╔██╗██║██║░░██╗░░╚╝╚█████╗░", "██║░░██║██╔══╝░░██║╚██╔╝██║██║░░██║██║╚████║  ██╔═██╗░██║██║╚████║██║░░╚██╗░░░░╚═══██╗",
                "██████╔╝███████╗██║░╚═╝░██║╚█████╔╝██║░╚███║  ██║░╚██╗██║██║░╚███║╚██████╔╝░░░██████╔╝", "╚═════╝░╚══════╝╚═╝░░░░░╚═╝░╚════╝░╚═╝░░╚══╝  ╚═╝░░╚═╝╚═╝╚═╝░░╚══╝░╚═════╝░░░░╚═════╝░" };
            string[] TitleArtTwo = new string[] { "██████╗░███████╗██╗░██████╗░███╗░░██╗  ░█████╗░███████╗  ████████╗███████╗██████╗░██████╗░░█████╗░██████╗░",
            "██╔══██╗██╔════╝██║██╔════╝░████╗░██║  ██╔══██╗██╔════╝  ╚══██╔══╝██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔══██╗",
            "██████╔╝█████╗░░██║██║░░██╗░██╔██╗██║  ██║░░██║█████╗░░  ░░░██║░░░█████╗░░██████╔╝██████╔╝██║░░██║██████╔╝",
            "██╔══██╗██╔══╝░░██║██║░░╚██╗██║╚████║  ██║░░██║██╔══╝░░  ░░░██║░░░██╔══╝░░██╔══██╗██╔══██╗██║░░██║██╔══██╗",
            "██║░░██║███████╗██║╚██████╔╝██║░╚███║  ╚█████╔╝██║░░░░░  ░░░██║░░░███████╗██║░░██║██║░░██║╚█████╔╝██║░░██║",
            "╚═╝░░╚═╝╚══════╝╚═╝░╚═════╝░╚═╝░░╚══╝  ░╚════╝░╚═╝░░░░░  ░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝"};
            string[] TitleMenuOptions = new string[] { "", "", "", "[ 1) Embark on a new journy ]", "", "", "[ 2) Instructions ]", "", "", "[ 3) Quit Game ]", "", "" };
            string[] TitleMenuInstuctions = new string[] {"Instructions","","",
                "To select an option enter the number next to it!", "", "Your goal it to defeat the demon king in his dungeon", "at the north-west corner of the world!",
                "You can do this by fighting him when you're ready.","",
                "You gain exp by fighting and beating enemies to level up.", "You also gain money from enemies that you can use to buy potions and weapons at the shop!",
                "If you need to heal, go to one of the Cabins in the world, there you can rest for free!",
                "","","Map Reference"
                };
            printer.PrintArray(TitleArtOne, true, false, true);
            printer.PrintArray(TitleArtTwo, false, true, true);

            switch (menu)
            {
                case 1:
                    printer.PrintArray(TitleMenuOptions);
                    ChooseMenuOptions(printer, soundPlayer);
                    break;
                case 2:
                    printer.PrintArray(TitleMenuInstuctions, true, false);
                    printer.PrintMapReference();
                    InstructionMenu(printer, soundPlayer);
                    break;
            }
            
        }
        static void ChooseMenuOptions(Printer printer, SoundPlayer soundPlayer)
        {

            int choice;
            while (true)
            {
                //prompts for user input to choose name
                choice = ReturnChoice(printer);
                switch (choice)
                {
                    case 1:
                        soundPlayer.Stop();
                        GameLoop();
                        return;
                    case 2:
                        Console.Clear();
                        PrintMainMenu(printer, 2, soundPlayer);
                        InstructionMenu(printer, soundPlayer);
                        return;
                    case 3:
                        Environment.Exit(0);
                        return;
                }
            }
        }

        static void InstructionMenu(Printer printer, SoundPlayer soundPlayer)
        {

            int choice;
            while (true)
            {
                //prompts for user input to choose name
                choice = ReturnChoice(printer);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        PrintMainMenu(printer, 1, soundPlayer);
                        ChooseMenuOptions(printer, soundPlayer);

                        return;
                }
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
        static void DeathChoice(Printer printer, Player player)
        {
            int choice;
            string[] deathMessage = new string[] { "{0}, you have died", "The demon lord will now surely rule this land forever", "Unless, you want to try again? ", "1) Yes     2) No  " };
            while (true)
            {
                deathMessage[0] = string.Format(deathMessage[0], player.Name);
                printer.PrintArray(deathMessage);
                choice = ReturnChoice(printer);
                switch (choice)
                {
                    case 1:
                        return;
                    case 2:
                        printer.PrintSingle("Good bye.");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                        break;
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
