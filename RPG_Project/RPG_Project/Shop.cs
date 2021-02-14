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

namespace RPG_Project
{
    class Shop
    {
        /***Constants***/
        const int BRONZE_LEVEL_LOCK = 3;
        const int STEEL_LEVEL_LOCK = 6;
        const int LEGEND_LEVEL_LOCK = 10;

        /***Variables***/
        List<Weapon> weaponList;
        List<Potion> potionList;

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


        /***Constructor***/
        public Shop()
        {
            weaponList = new List<Weapon>();
            potionList = new List<Potion>();
            
        }

        /***Methods***/
        public void InitShop(Player player)
        {
            bool keepBuying = false;
            int option = 0;
            Console.WriteLine("==================================== Shop ========================================");
            int lvl = player.Level;
            //Starting shop
            do {
                Console.WriteLine("Welcome to the shop! How can we be able to help you hero?");
                Console.WriteLine("(Please type one of the options below)");
                Console.WriteLine("1: Show Weapons");
                Console.WriteLine("2: Show Potions");
                Console.WriteLine("3: Exit shop");
                Console.WriteLine("{0}'s Current money: {1}", player.Name, player.Money);

                option = ValidateNumber(4);

                switch (option)
                {
                    case 1:
                        ChooseWeaponMat(lvl, player);
                        break;

                    case 2:
                        PotionBuy(player);
                        break;

                    case 3:
                        Console.WriteLine("Thank you for visiting us! Come back soon!");
                        return;

                    default:
                        Console.WriteLine("Error: Invalid option selected");
                        keepBuying = true;
                        break;
                }

                if (!keepBuying)
                {
                    Console.WriteLine("Would you like to keep buying? (Type 1 if yes, else type 2)");
                    option = ValidateNumber(3);

                    if(option == 1)
                    {
                        keepBuying = true;
                    }
                    else
                    {
                        keepBuying = false;
                    }

                }

            } while (keepBuying);
           
            Console.WriteLine("=================================================================================");
        }


        //According to player's level unlock items
        private void ChooseWeaponMat(int lvl, Player player)
        {
            int count = 0;
            var type = (WeaponTypes)1;
            int option;
            Console.WriteLine("Choose the weapon material you want to buy");
           for (int i= 0; i < Enum.GetValues(typeof(WeaponTypes)).Length; i++)
            {
                //Check player's level for filters
                if (lvl < BRONZE_LEVEL_LOCK && i >= 1) { break; }
                if (lvl < STEEL_LEVEL_LOCK && i >= 2) { break; }
                if (lvl < LEGEND_LEVEL_LOCK && i >= 3) { break; }
                //print Materials
                
                //Prevent user selection of an invalid or locked material (level blocks)
                count++;
                type = (WeaponTypes)count;
                Console.WriteLine("{0}: {1}", count, type);

            }
           //User typed validation
            option = ValidateNumber(count+1);
            WeaponTypes material = (WeaponTypes)option;

            WeaponBuy(player, material.ToString());
        }

        private void WeaponBuy(Player player, string material)
        {
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

            Console.WriteLine("Choose the weapon you want to buy");
            Console.WriteLine("1) Sword");
            Console.WriteLine("2) Axe");
            Console.WriteLine("3) Lance");
            Console.WriteLine("4) return to menu");

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
                    Console.WriteLine("Sorry, the total price is {0} and you have {1}... you can't afford it...", cost, player.Money);
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
                    Console.WriteLine("You already have that weapon...");
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

            Console.WriteLine("Choose the potion you want to buy");
            Console.WriteLine("1) Normal-HP Potion Cost: 10");
            Console.WriteLine("2) Super-HP Potion  Cost: 20");
            Console.WriteLine("3) Mega-HP Potion   Cost: 30");
            Console.WriteLine("4) Normal-PP Potion Cost: 10");
            Console.WriteLine("5) Super-PP Potion  Cost: 20");
            Console.WriteLine("6) Mega-PP Potion   Cost: 30");
            Console.WriteLine("7) return to menu");

            do
            {
                //Validate user input
                choice = ValidateNumber(8);
                if(choice == 7)
                {
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
                        quality = 0;
                        type = 1;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 3:
                        cost = 30 * amount;
                        quality = 0;
                        type = 2;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    case 4:
                        cost = 10 * amount;
                        quality = 1;
                        type = 0;
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
                        quality = 1;
                        type = 2;
                        noMoney = ValidateMoney(cost, player);
                        break;
                    default:
                        err = true;
                        Console.WriteLine("Error: Not a valid option");
                        break;
                }

                if (noMoney)
                {
                    Console.WriteLine("Sorry, the total price is {0} and you have {1}... you can't afford it...", cost, player.Money);
                    return;
                    
                }

                if (!player.InventHasSpace() || player.ReturnMaxCapacity() <= amount)
                {
                    Console.WriteLine("You don't have space on the inventory!");
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
            int limitToBuy = 100;
            int amount;
            Console.WriteLine("How many would you like?");
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
            Console.WriteLine("The cost of the {0} is: {1} ... Would you like to buy it? (Type 1 to accept, else type any other number)", name, cost);
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
                    Console.WriteLine("Error: Not a valid option");
                    err = true;
                }
            } while (err);
                
            return true;
        }
    }
}
