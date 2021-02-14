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
            Console.WriteLine("==================================== Shop ========================================");
            int lvl = player.Level;
            //Starting shop
            ChooseWeaponMat(lvl);
            DisplayPotions(player);
            Console.WriteLine("=================================================================================");
        }


        //According to player's level unlock items
        private void ChooseWeaponMat(int lvl)
        {
            int count = 1;
            var type = (WeaponTypes)1;
            Console.WriteLine("Choose the weapon material you want to buy");
           for (int i= 0; i < Enum.GetValues(typeof(WeaponTypes)).Length; i++)
            {
                //Check player's level for filters
                if (lvl < 2 && i >= 1) { break; }
                if (lvl < 3 && i >= 2) { break; }
                if (lvl < 4 && i >= 3) { break; }
                //print weapons
                Console.WriteLine("{0}: {1}", count, type);
                count++;
                type = (WeaponTypes)count;
            }
        }

        private void DisplayPotions(Player player)
        {
            int choice;
            int amount;
            int cost;
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

                if (!player.InventHasSpace())
                {
                    Console.WriteLine("You don't have any more space on the inventory!");
                    noSpace = true;
                }

                //Checkout
                if (!noMoney && !noSpace)
                {
                    while(amount != 0)
                    {
                        potion = new Potion(quality, type);
                        
                        amount--;
                    }
                }

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

                    AmountAsk();

                    //Amount asking question

                }
                catch (FormatException formatEx)
                {
                    err = true;
                    Console.WriteLine(formatEx.Message);
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
            if(price <= player.Money) { return true; }

            return false;
        }
    }
}
