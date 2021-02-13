using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    enum ActiveMenu
    {
        General,
        Movement,
        Inventory,
        Inventory_Potions,
        Inventory_Weapons,
        Stats,
        Abilities
    }
    class OverWorldManager
    {
        Printer printer = new Printer();

        Map theMap = new Map();

        Player player;

        ActiveMenu currentMenu = ActiveMenu.General;

        bool hasMap = false;


        string[] enemyArt = new string[] { "Movement Controls", "", "1) North", "3) West   4) East", "2) South" };
        string[] movementUI = new string[] { "Movement Controls", "", "1) Up",    "3) Left   4) Right", "2) Down", "" ,"5) Back" };
        string[] generalUI = new string[] { "Controls", "", "1) Movement      2) Inventory", "3) Stats         4) Abilities" };
        string[] inventoryUiBase = new string[] { "Inventory", "", "1) Weapons      2) Potions", "3) Back" };
        string[] inventroyUiPotions = new string[] {"[ 1) Normal-HP Potion: {0}] [ 2) Super-HP Potion: {1} ] [ 2) Mega-HP Potion: {2}]", "[ 1) Normal-PP Potion: {0}] [ 2) Super-PP Potion: {1} ] [ 2) Mega-PP Potion: {2}]", "TmpBack"};

        //Constructor 
        public OverWorldManager(Player player)
        {
            this.player = player;
        }

        //Draw map
        void DrawMap()
        {
            if (!hasMap)
            {
                hasMap = true;
                theMap.GenerateMap();
            }
        }

        public void UpdateMap()
        {
            DrawMap();

            theMap.UpdatePlayerMap();
            printer.PrintArray(movementUI);
        }

        void DrawOnlyMap()
        {
            DrawMap();
            theMap.UpdatePlayerMap();
        }

        //TODO Draw UI on screen
        public void DrawUi()
        {
            Console.Clear();
            DrawOnlyMap();

            switch (currentMenu)
            {
                case ActiveMenu.General:
                    printer.PrintArray(generalUI);
                    GeneralUi();
                    break;
                case ActiveMenu.Movement:
                    UpdateMap();
                    MovementUi();
                    break;
                case ActiveMenu.Inventory:
                    printer.PrintArray(inventoryUiBase);
                    InventoryUi();
                    break;
                case ActiveMenu.Inventory_Potions:
                    InventoryUiPotions();
                    break;
                case ActiveMenu.Inventory_Weapons:
                    InventoryUiWeapons();
                    break;
            }
            
        }

        //General menu input
        void GeneralUi()
        {
            int choice;
            while (true)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                        currentMenu = ActiveMenu.Movement;
                        DrawUi();
                        break;
                    case 2:
                        currentMenu = ActiveMenu.Inventory;
                        DrawUi();
                        break;
                    case 3:
                        currentMenu = ActiveMenu.Stats;
                        DrawUi();
                        break;
                    case 4:
                        currentMenu = ActiveMenu.Abilities;
                        DrawUi();
                        break;
                }
            }
        }

        //Movement menu input
        void MovementUi()
        {
            int choice;
            while (true)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                        theMap.CheckSurroundings(choice);
                        break;
                    case 2:
                        theMap.CheckSurroundings(choice);
                        break;
                    case 3:
                        theMap.CheckSurroundings(choice);
                        break;
                    case 4:
                        theMap.CheckSurroundings(choice);
                        break;
                    case 5:
                        Console.WriteLine("Returning");
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        return;

                }
                UpdateMap();
            }
        }

        //Inventroy menu input
        void InventoryUi()
        {
            int choice;
            while (true)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                        currentMenu = ActiveMenu.Inventory_Weapons;
                        DrawUi();
                        break;
                    case 2:
                        currentMenu = ActiveMenu.Inventory_Potions;
                        DrawUi();
                        break;
                    case 3:
                        Console.WriteLine("Returning");
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        return;

                }
                UpdateMap();
            }
        }
        void InventoryUiPotions()
        {
            int choice;

                int[,] potionList = player.ReturnPotions();
                int count = 0;

                string[] copyList = new string[inventroyUiPotions.Length];

                //Copy string to standard string
                foreach (string text in inventroyUiPotions)
                {
                    copyList[count] = text;
                    count++;
                }

                for (int i = 0; i < copyList.Length - 1; i++)
                {
                    copyList[i] = string.Format(copyList[i], potionList[i, 0], potionList[i, 1], potionList[i, 2]);
                }
                copyList[2] = string.Format("[ 7): Return to menu ]");
            printer.PrintArray(copyList);

            while (true)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                        if (potionList[0, 0] != 0)
                        {
                            player.DrinkPotion(0);
                        }
                        break;
                    case 2:
                        if (potionList[0, 1] != 0)
                        {
                            player.DrinkPotion(1);
                        }
                        break;
                    case 3:
                        if (potionList[0, 2] != 0)
                        {
                            player.DrinkPotion(2);
                        }
                        break;
                    case 4:
                        if (potionList[1, 0] != 0)
                        {
                            player.DrinkPotion(3);

                        }
                        break;
                    case 5:
                        if (potionList[1, 1] != 0)
                        {
                            player.DrinkPotion(4);

                        }
                        break;
                    case 6:
                        if (potionList[1, 2] != 0)
                        {
                            player.DrinkPotion(5);

                        }
                        break;
                    case 7:
                        currentMenu = ActiveMenu.Inventory;
                        DrawUi();
                        break;
                }
            }
        }

        void InventoryUiWeapons()
        {
            printer.PrintArray(player.ReturnWeaponList());
        }

        int ReturnChoice()
        {
            while (true)
            {

                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                    printer.PrintSingle("Invalid selection, please use a number!", true, true);
                }
            }
        }
        //TODO EnterShop


        //TODO EnterCombat


        //TODO Enter dungeon


    }
}
