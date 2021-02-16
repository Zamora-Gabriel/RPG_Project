using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum WeaponTypes
{
    Wooden = 1,
    Bronze,
    Steel,
    //More speed
    Celestial,
    //More Defense
    Titan,
    //More Attack
    Dragon,
}

enum MenuType
{
    MainMenu = 0,
    WeaponMenu,
    PotionMenu,

}

namespace RPG_Project
{
    class Shop
    {
        /***Constants***/
        const int BRONZE_LEVEL_LOCK = 5;
        const int STEEL_LEVEL_LOCK = 8;
        const int LEGEND_LEVEL_LOCK = 12;

        /***Variables***/
        List<Weapon> weaponList;
        List<Potion> potionList;

        Printer printer = new Printer();
        Player player;
        MenuType currentMenu = MenuType.MainMenu;



        string[] mainMenu = new string[] { "", "Shop", "", "", "What could I getcha?", "", "[ 1) Weapons ]     [ 2) Potions ]" ,"[ 3) Exit Shop ]" };
        String[] weaponMenu = new string[] { "", "Weapons", "", "", "Alright what type of material?","","" };
        string[] potionMenu = new string[] {"", "Potions","","","So, ya need healing, or energy?", "", "",
                "[ 1) Normal-HP Potion   Cost: 10 ]","[ 2) Super-HP Potion    Cost: 20 ]","[ 3) Mega-HP Potion     Cost: 30 ]",
                "[ 4) Normal-PP Potion   Cost: 10 ]","[ 5) Super-PP Potion    Cost: 20 ]","[ 6) Mega-PP Potion     Cost: 30 ]","","",
                "[ 7) Return ]"};

        bool keepBuying = true;

        /***Properties***/
        public List<Weapon> WeaponList
        {
            get { return weaponList; }

            set
            {
                weaponList = value;
            }
        }

        public List<Potion> PotionList
        {
            get { return potionList; }

            private set
            {
                potionList = value;
            }
        }

        public Player Player
        {
            get { return player; }
            private set
            {
                player = value;
            }
        }

        /***Constructor***/
        public Shop()
        {
            weaponList = new List<Weapon>();
            potionList = new List<Potion>();
            
        }

        /***Methods***/
        public void InitShop(Player player)
        {
            Player = player;
            keepBuying = true;
            int option = 0;
            while (keepBuying)
            {
                UpdateMenu();
            }
            

            //Console.WriteLine("==================================== Shop ========================================");
            //int lvl = player.Level;
            //Starting shop
            //do {
            //    Console.WriteLine("Welcome to the shop! How can we be able to help you hero?");
            //    Console.WriteLine("(Please type one of the options below)");
            //    Console.WriteLine("1: Show Weapons");
            //    Console.WriteLine("2: Show Potions");
            //    Console.WriteLine("3: Exit shop");
            //    Console.WriteLine("{0}'s Current money: {1}", player.Name, player.Money);

            //    option = ValidateNumber(4);

            //    switch (option)
            //    {
            //        case 1:
            //            ChooseWeaponMat();
            //            break;

            //        case 2:
            //            PotionBuy(player);
            //            break;

            //        case 3:
            //            Console.WriteLine("Thank you for visiting us! Come back soon!");
            //            return;

            //        default:
            //            Console.WriteLine("Error: Invalid option selected");
            //            keepBuying = true;
            //            break;
            //    }

            //    if (!keepBuying)
            //    {
            //        Console.WriteLine("Would you like to keep buying? (Type 1 if yes, else type 2)");
            //        option = ValidateNumber(3);

            //        if(option == 1)
            //        {
            //            keepBuying = true;
            //        }
            //        else
            //        {
            //            keepBuying = false;
            //        }

            //    }

            //} while (keepBuying);
           
            //Console.WriteLine("=================================================================================");
        }

        //Draw shop menus
        void UpdateMenu()
        {
            Console.Clear();
            switch (currentMenu)
            {
                case MenuType.MainMenu:
                    printer.PrintArray(mainMenu);
                    MainMenu();
                    break;
                case MenuType.WeaponMenu:
                    printer.PrintArray(weaponMenu, true, false);
                    ChooseWeaponMat();
                    break;
                case MenuType.PotionMenu:
                    printer.PrintArray(potionMenu, true, true);
                    PotionBuy(player);
                    break;
            }
        }


        void MainMenu()
        {
            int choice;
            while(true)
            {
                choice = ValidateNumber(4);
                switch (choice)
                {
                    case 1:
                        currentMenu = MenuType.WeaponMenu;
                        UpdateMenu();
                        return;

                    case 2:
                        currentMenu = MenuType.PotionMenu;
                        UpdateMenu();
                        return;

                    case 3:
                        keepBuying = false;
                        printer.PrintSingle("Thank you for visiting us! Come back soon!");
                        Console.ReadLine();
                        return;

                    default:
                        Console.WriteLine("Error: Invalid option selected");
                        break;
                }
            } 
        }

        //According to player's level unlock items
        private void ChooseWeaponMat()
        {
            int count = 0;
            var type = (WeaponTypes)1;
            int option;
           for (int i= 0; i < Enum.GetValues(typeof(WeaponTypes)).Length; i++)
            {
                //Check player's level for filters
                if (player.Level < BRONZE_LEVEL_LOCK && i >= 1) { break; }
                if (player.Level < STEEL_LEVEL_LOCK && i >= 2) { break; }
                if (player.Level < LEGEND_LEVEL_LOCK && i >= 3) { break; }
                //print Materials
                
                //Prevent user selection of an invalid or locked material (level blocks)
                count++;
                type = (WeaponTypes)count;
                printer.PrintSingle(string.Format("[ {0}: {1} ]", count, type),false,false);
            }
            printer.PrintSingle(string.Format("[ {0}) Return ]", count+1), false, false);
            printer.PrintSingle(string.Format(""), false, true);
            //User typed validation
            Console.WriteLine(count + 1);
            option = ValidateNumber(count+2);
            if(option == count + 1)
            {
                currentMenu = MenuType.MainMenu;
                UpdateMenu();
                return;
            }
            WeaponTypes material = (WeaponTypes)option;

            WeaponBuy(player, material.ToString());
            UpdateMenu();
        }

        private void WeaponBuy(Player player, string material)
        {
            Console.Clear();
            int choice;
            int cost = 0;
            string type = "";
            string name = "";
            Weapon weap = null;
            bool err = false;
            bool noMoney = false;
            bool noSpace = false;
            bool weaponChoiceValid = false;
            bool alreadyHave = false;
            string[] weaponList = new string[] {"Now then, what type of weapon suites your fancy?","", "[ 1) Sword ]",
                "[  2) Axe  ]", "[ 3) Lance ]","","[ 4) return to menu ]" };

            printer.PrintArray(weaponList);
            do
            {
                //Validate user input
                choice = ValidateNumber(5);
                if (choice == 4)
                {
                    return;
                }
                //Create new weapon depending on the type
                switch (choice)
                {
                    case 1:
                        type = " Sword";
                        name = material + type;
                        weap = new Sword(name);
                        cost = ((Sword)weap).CreateWeapForShop(material);
                        weaponChoiceValid = ValidateWeapChoice(cost, name);
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 2:
                        type = " Axe";
                        name = material + type;
                        weap = new Axe(name);
                        cost = ((Axe)weap).CreateWeapForShop(material);
                        weaponChoiceValid = ValidateWeapChoice(cost, name);
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 3:
                        type = " Lance";
                        name = material + type;
                        weap = new Lance(name);
                        cost = ((Lance)weap).CreateWeapForShop(material);
                        weaponChoiceValid = ValidateWeapChoice(cost, name);
                        noMoney = ValidateMoney(cost, player);
                        break;
                    default:
                        err = true;
                        Console.WriteLine("Error: Not a valid option");
                        break;
                }

                if (!weaponChoiceValid)
                {
                    Console.WriteLine("Returning to shop menu...");
                    return;
                }

                if (noMoney)
                {
                    printer.PrintSingle(string.Format("Sorry, the total price is {0} and you have {1}... you can't afford it...", cost, player.Money));
                    Console.ReadKey();
                    //Console.WriteLine("Sorry, the total price is {0} and you have {1}... you can't afford it...", cost, player.Money);
                    return;
                }

                if (!player.InventHasSpace())
                {
                    Console.WriteLine("You don't have space on the inventory!");
                    noSpace = true;
                    return;
                }

                if (player.IsTheWeaponOnInventory(name))
                {
                    printer.PrintSingle("You already have that weapon.");
                    Console.ReadLine();
                    //Console.WriteLine("You already have that weapon...");
                    alreadyHave = true;
                    return;
                }

                //Checkout
                if (!noMoney && !noSpace && !alreadyHave)
                {

                    //Add weapon to inventory
                    player.AddWeaponToInvent(weap);
                    Console.WriteLine("Successfully added {0} weapon!", weap.Name);
                    //Substract total from player's money
                    player.Money -= cost;
                    return;
                }
                Console.WriteLine("Error: Transaction failed");

            } while (err);
            return;
        }

        private void PotionBuy(Player player)
        {
            int choice;
            int amount;
            int cost=0;
            int quality = 0;
            int type = 0;
            Potion potion;
            bool err = false;
            bool noMoney = false;
            bool noSpace = false;
            
            //Console.WriteLine("Choose the potion you want to buy");
            //Console.WriteLine("1) Normal-HP Potion Cost: 10");
            //Console.WriteLine("2) Super-HP Potion  Cost: 20");
            //Console.WriteLine("3) Mega-HP Potion   Cost: 30");
            //Console.WriteLine("4) Normal-PP Potion Cost: 10");
            //Console.WriteLine("5) Super-PP Potion  Cost: 20");
            //Console.WriteLine("6) Mega-PP Potion   Cost: 30");
            //Console.WriteLine("7) return to menu");

            do
            {
                //Validate user input
                choice = ValidateNumber(8);
                if(choice == 7)
                {
                    currentMenu = MenuType.MainMenu;
                    UpdateMenu();
                    return;
                }
                //Amount to buy
                amount = AmountAsk();
                switch (choice)
                {
                    case 1:
                        cost = 10 * amount;
                        quality = 0;
                        type = 0;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 2:
                        cost = 20 * amount;
                        quality = 1;
                        type = 0;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 3:
                        cost = 30 * amount;
                        quality = 2;
                        type = 0;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 4:
                        cost = 10 * amount;
                        quality = 0;
                        type = 1;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 5:
                        cost = 20 * amount;
                        quality = 1;
                        type = 1;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 6:
                        cost = 30 * amount;
                        quality = 2;
                        type = 1;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    default:
                        currentMenu = MenuType.MainMenu;
                        UpdateMenu();
                        return;
                }

                if (noMoney)
                {
                    printer.PrintSingle(string.Format("Sorry, the total price is {0} and you have {1}... you can't afford it...", cost, player.Money));
                    Console.ReadLine();
                    return;
                }

                if (!player.InventHasSpace() || player.ReturnMaxCapacity() <= amount)
                {
                    printer.PrintSingle("You don't have space on the inventory!");
                    Console.ReadLine();
                    noSpace = true;
                    return;
                }

                //Checkout
                if (!noMoney && !noSpace)
                {
                    while(amount != 0)
                    {
                        potion = new Potion(quality, type);
                        player.AddPotionToInvent(potion);
                        amount--;
                    }
                    //Substract total from player's money
                    player.Money -= cost;
                    return;
                }
                Console.WriteLine("Error: Transaction failed");

            } while (err);
            return;
        }

        private int AmountAsk()
        {
            int limitToBuy = 101;
            int amount;
            printer.PrintSingle("How many would ya like?");
            //Console.WriteLine("How many would you like?");
            return amount = ValidateNumber(limitToBuy);
        }

        private int ValidateNumber(int limit)
        {
            int option = 0;
            bool err = false;
            do
            {
                //Check user's option
                try
                {
                    option = int.Parse(Console.ReadLine());

                    //Validate user input
                    if (option <= 0 || option >= limit)
                    {
                        throw new Exception("Error: Not a valid number, try again");
                    }
                    err = false;

                }
                catch (FormatException)
                {
                    err = true;
                    Console.WriteLine("Error, please type a number");
                }
                catch (Exception numEx)
                {
                    err = true;
                    Console.WriteLine(numEx.Message);
                }
            } while (err);

            return option;
        }

        private bool ValidateMoney(int price, Player player)
        { 
            if(price <= player.Money) { return false; }

            return true;
        }

        private bool ValidateWeapChoice( int cost, string name )
        {
            bool err = false;
            printer.PrintSingle(string.Format("The cost of the {0} is: {1} ... Would you like to buy it?", name, cost), true, false);
            printer.PrintSingle(string.Format("[ 1) Yes ]       [ 2) No ]", name, cost), false, true);
            do
            {
                try
                {
                    int option = int.Parse(Console.ReadLine());
                    if(option == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    printer.PrintSingle("Error: not a valid options");
                    
                    err = true;
                }
            } while (err);
            return true;
        }
    }
}
