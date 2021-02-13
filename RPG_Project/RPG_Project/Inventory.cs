using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PotionType
{
    HealthPotion = 0,
    EnergyPotion
}

namespace RPG_Project
{
    class Inventory
    {
        /***Variables***/
        const int INVENTORY_CAPACITY = 10;

        List<Weapon> weaponList;
        List<Potion> potionList;
        int capacity; //--How many items the user can carry 

        

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

        public int Capacity
        {
            get { return capacity; }

            private set
            {
                capacity = value;
                if (!CheckCapacity())
                {
                    capacity--;
                }
            }
        }

        /***Constructor***/
        public Inventory()
        {
            weaponList = new List<Weapon>();
            potionList = new List<Potion>();
            capacity = 0;
        }

        /***Methods***/

        private bool CheckCapacity()
        {
            if (capacity >= INVENTORY_CAPACITY)
            {
                Console.WriteLine("Sorry, you can't carry more objects...");
                return false;
            }
            return true;
        }

        //Weapons// 

        public bool AddWeapon(Weapon weap)
        {
            capacity++;
            if (CheckCapacity())
            {
                weaponList.Add(weap);
                return true;
            }
            return false;
        }

        public bool RemoveWeapon()
        {
            return false;
        }

        //Check by number in menu 
        public Weapon CheckWeapon(int i)
        {
            int counter = 0;
            foreach (Weapon item in weaponList)
            {

                if (counter == i)
                {
                    return item;
                }
                counter++;
            }
            return null;
        }

        //Return list of weapons
        public string[] ReturnWeaponList()
        {
            int counter = 0;
            string[] returnList = new string[weaponList.Count];
            foreach (Weapon item in weaponList)
            {
                returnList[counter] += string.Format("{0}) {1}    ", counter+1, item.Name);
                counter++;

            }
            return returnList;
        }

        //Check weapon by name 
        public Weapon CheckWeaponByName(string name)
        {
            int counter = 0;
            foreach (Weapon item in weaponList)
            {

                if (item.Name == name)
                {
                    return item;
                }
                counter++;
            }
            return null;
        }

        public Weapon CheckWeaponByType(Weapon type)
        {
            int counter = 0;
            foreach (Weapon item in weaponList)
            {

                if (item == type)
                {
                    return item;
                }
                counter++;
            }
            return null;
        }

        public void PrintWeapons()
        {
            Console.WriteLine("---------------Weapon List--------------- \n");
            if (weaponList.Count == 0)
            {
                Console.WriteLine("Wait... You don't have any weapons \n");
                return;
            }

            int count = 1;
            foreach (Weapon item in weaponList)
            {
                Console.WriteLine("{0}: {1}", count, item.Name);
                Console.WriteLine("");
                count++;
            }
        }

        //Potions// 

        public bool AddPotion(Potion pot)
        {
            capacity++;
            if (CheckCapacity())
            {
                potionList.Add(pot);
                return true;
            }
            return false;
        }

        public bool RemovePotion(Potion pot)
        {
            foreach (Potion item in potionList)
            {
                if (item.Type == pot.Type)
                {
                    if (item.Quality == pot.Quality)
                    {
                        potionList.Remove(item);
                        return true;
                    }
                }
            }
            Console.WriteLine("No potion was found");
            return false;
        }

        public Potion CheckPotion(int pot)
        {
            /*int potionType = 0;

            var potiont = (PotionType)0;*/
            foreach (Potion item in potionList)
            {
                /*if (item.Type == (int)PotionType.EnergyPotion)
                {
                    //if it's a Energy potion, change the default 
                    potionType = 1;
                    potiont = (PotionType)1;
                }
                else
                {
                    potionType = 0;
                }*/
                switch (pot)
                {
                    case 0:
                        //Normal potion
                        if(item.Quality == 0)
                        {
                            return item;
                        }
                        break;
                    case 1:
                        //Super potion
                        if (item.Quality == 1)
                        {
                            return item;
                        }
                        break;
                    case 2:
                        //Mega Potion
                        if (item.Quality == 2)
                        {
                            return item;
                        }
                        break;
                    case 3:
                        //Normal potion
                        if (item.Quality == 0)
                        {
                            return item;
                        }
                        break;
                    case 4:
                        //Super potion
                        if (item.Quality == 1)
                        {
                            return item;
                        }
                        break;
                    case 5:
                        //Mega Potion
                        if (item.Quality == 2)
                        {
                            return item;
                        }
                        break;
                    default:
                        return null;
                }
            }
            return null;
        }

        //returns 2d array of potions in inventory
        public int[,] ReturnPotionCount()
        {
            int[,] potions = new int[2, 3];
            for (int i = 0; i < potions.GetLength(0); i++)
            {
                for(int y = 0; y < potions.GetLength(1); y++)
                {
                    potions[i, 0] = 0;
                }
            }
            int potionType = 0;
            var potiont = (PotionType)0;

            foreach (Potion item in potionList)
            {
                //if (item.Type == (int)PotionType.EnergyPotion)
                //{
                //    //if it's a Energy potion, change the default 
                //    potiont = (PotionType)1;
                //}
                //else
                //{
                //    //potionType = 1;
                //}
                switch (item.Quality)
                {
                    case 0:
                        potions[potionType, 0]++;
                        break;
                    case 1:
                        potions[potionType, 1]++;
                        break;
                    case 2:
                        potions[potionType, 2]++;
                        break;
                    default:
                        break;
                }

            }
            return potions;
        }

        public void PrintPotions()
        {
            Console.WriteLine("---------------Potion List--------------- \n");
            if (potionList.Count == 0)
            {
                Console.WriteLine("Wait... You don't have any potions \n");
                return;
            }

            int count = 0;
            string potionName = "";
            var potiont = (PotionType)0;
            foreach (Potion item in potionList)
            {
                if (item.Type == (int)PotionType.EnergyPotion)
                {
                    //if it's a Energy potion, change the default 
                    potiont = (PotionType)1;
                }

                switch (item.Quality)
                {
                    case 0:
                        potionName = "Normal ";
                        break;
                    case 1:
                        potionName = "Super ";
                        break;
                    case 2:
                        potionName = "Mega ";
                        break;
                    default:
                        break;
                }
                Console.WriteLine("{0}: {1}{2}", count, potionName, potiont);
                count++;
            }
            Console.WriteLine("");
        }
    }
}
