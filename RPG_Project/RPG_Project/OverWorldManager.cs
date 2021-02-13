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
        Abilities,
        Dead
    }
    class OverWorldManager
    {
        const int ENCOUNTER_CHANCE = 25;
        const int ENCOUNTER_CHANCE_FUll = 180;

        Printer printer = new Printer();


        Map theMap = new Map();

        Player player;

        Random rand = new Random();

       

        ActiveMenu currentMenu = ActiveMenu.General;

        bool hasMap = false;
        bool inLocation = false;

        string[] enemyArt = new string[] { "Movement Controls", "", "1) North", "3) West   4) East", "2) South" };
        string[] movementUI = new string[] { "Movement Controls", "", "1) Up",    "3) Left   4) Right", "2) Down", "" ,"5) Back" };
        string[] generalUI = new string[] { "Controls", "", "1) Movement      2) Inventory", "3) Stats         4) Abilities" };
        string[] inventoryUiBase = new string[] { "Inventory", "", "1) Weapons      2) Potions", "3) Back" };
        string[] inventroyUiPotions = new string[] {"[ 1) Normal-HP Potion: {0}] [ 2) Super-HP Potion: {1} ] [ 2) Mega-HP Potion: {2}]", "[ 1) Normal-PP Potion: {0}] [ 2) Super-PP Potion: {1} ] [ 2) Mega-PP Potion: {2}]", "TmpBack"};
        string[] inventoryStats = new string[] {"Player: {0}","","Health: {0}/{1}      Energy: {2}/{3}" , "Level: {0}          Exp: {1}       ", "Attack: {0}         Defense: {1}   ", "Speed {0}           Money: {1}     ","","1) Back"};
        
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
            if (player.HasDied)
            {
                currentMenu = ActiveMenu.Dead;
            }
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
                case ActiveMenu.Stats:
                    InventoryStats();
                    break;
                case ActiveMenu.Dead:
                    return;
            }
            
        }

        //General menu input
        void GeneralUi()
        {
            int choice;
            while (!player.HasDied)
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
            return;
        }

        //Movement menu input
        void MovementUi()
        {

            int choice;
            while (!player.HasDied)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        theMap.CheckSurroundings(choice);
                        break;
                    case 5:
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        return;
                } 
                
                EncounterGenerator(theMap.ReturnPlayerTileLevel(), theMap.ReturnPlayerTileType());

                //check if on location tile
                if (theMap.ReturnPlayerTileType() < 4)
                {
                    inLocation = false;
                }
                //Update map
                UpdateMap();
                //Enter location if not already in location
                if (!inLocation)
                {
                    EnterLocation(theMap.ReturnPlayerTileType());
                }
                currentMenu = ActiveMenu.General;
            }
            return;
        }

        //Inventroy menu input
        void InventoryUi()
        {
            int choice;
            while (!player.HasDied)
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
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        return;

                }
                UpdateMap();
            }
            return;
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

            while (!player.HasDied)
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
            return;
        }

        //TODO MAKE THIS WORK
        //Current issue with adding weapons to inventory, so moving on for now.
        void InventoryUiWeapons()
        {
            int choice;
            int count = 0;
            string[] formatedString;

            if (player.ReturnWeaponList().Length % 3 != 0)
            {
                formatedString = new string[(player.ReturnWeaponList().Length / 3) + 1];
            }
            else
            {
                formatedString = new string[player.ReturnWeaponList().Length / 3];
            }
            
            for (int i = 0; i < formatedString.Length; i++)
            {
                for(int x = 0; x < 3; x++)
                {
                    if(count < player.ReturnWeaponList().Length)
                    {
                        formatedString[i] += string.Format("{0}", player.ReturnWeaponList()[count]);
                        count++;
                    }
                }
            }
            //Format incomming string for better viewing


            printer.PrintArray(formatedString);
            while (!player.HasDied)
            {
                //loop through posible choices
                choice = ReturnChoice();
                for(int i =0; i < player.ReturnWeaponList().Length; i++)
                {
                    if(choice == i)
                    {
                        InventoryStats();

                        break;
                    }
                    if(choice == player.ReturnWeaponList().Length + 1)
                    {
                        currentMenu = ActiveMenu.Inventory;
                        DrawUi();
                        break;
                    }
                }
            }
        }

        void InventoryStats()
        {
            string[] copyList = new string[inventoryStats.Length];
            for (int i = 0; i < inventoryStats.Length; i++)
            {
                copyList[i] = inventoryStats[i];
            }
            //Print stats
            string[] playerStats = new string[11];
            playerStats = player.ReturnStats();

            copyList[0] = string.Format(copyList[0], playerStats[0]);
            copyList[2] = string.Format(copyList[2], playerStats[1], playerStats[2], playerStats[3], playerStats[4]);
            copyList[3] = string.Format(copyList[3], playerStats[5], playerStats[6]);
            copyList[4] = string.Format(copyList[4], playerStats[7], playerStats[8]);
            copyList[5] = string.Format(copyList[5], playerStats[9], playerStats[10]);

            printer.PrintArray(copyList);
            int choice;
            while (true)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        break;
                }
                UpdateMap();
            }
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


        //TODO EnterCombat
        void EncounterGenerator(int danger, int tileType)
        {
            Console.WriteLine(danger);
            //Check danger level if none return
            if (danger == 0)
            {
                return;
            }

            //Check if encounter should happen based on percent
            int randResult = rand.Next(0, ENCOUNTER_CHANCE_FUll);
            if(randResult > ENCOUNTER_CHANCE)
            {
                return;
            }

            //Make sure tile player is one can have encounters;
            if (tileType == 0)
            {
                return;
            }

            //Generate encounter based on difficulty and terrain type
            switch (tileType)
            {
                case 1:
                    GrassLandEncounterGen(danger);
                    break;
                case 2:
                    ForestEncounterGen(danger);
                    break;
                case 3:
                    WaterEncounterGen(danger);
                    break;
            }
            DrawUi();
        }

        //TODO Enter dungeon
        void GrassLandEncounterGen(int danger)
        {
            EnemyGenerator enemylist = new EnemyGenerator(player);
            switch (danger)
            {
                case 1:
                    enemylist.EasyGrassLandsEncounter();

                    break;
            }
        }
        void ForestEncounterGen(int danger)
        {
            EnemyGenerator enemyGenerator = new EnemyGenerator(player);
            switch (danger)
            {
                case 1:
                    enemyGenerator.EasyForestEncounter();
                    break;
            }
        }
        void WaterEncounterGen(int danger)
        {
            EnemyGenerator enemyGenerator = new EnemyGenerator(player);
            switch (danger)
            {
                case 1:
                    enemyGenerator.EasyWaterEncounter();
                    break;
            }
        }


        //TODO EnterShop

        //TODO Enter Dungeon

        void EnterLocation(int tileType)
        {
            //Generate encounter based on difficulty and terrain type
            switch (tileType)
            {
                case 4:
                    //TODO ENTER SHOP INTERFACE
                    break;
                case 5:
                    //TODO RESTORE HP
                    inLocation = true;
                    printer.PrintSingle("You feel restored after a good sleep!");
                    player.Health += 1000;
                    player.Energy += 1000;
                    Console.ReadLine();
                    break;
                case 6:
                    //ENTER BOSS FIGHT!!
                    EnemyGenerator enemyGenerator = new EnemyGenerator(player);
                    enemyGenerator.BossBattle();
                    break;
                default:

                    break;
            }
            return;
        }

    }
}
