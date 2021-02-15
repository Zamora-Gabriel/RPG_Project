using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace RPG_Project
{
    enum ActiveMenu
    {
        General,
        Movement,
        Inventory,
        Inventory_Potions,
        Inventory_Weapons,
        Inventory_Weapon_Detail,
        Stats,
        Abilities,
        Abilitie_Details,
        Dead
    }
    class OverWorldManager
    {
        const int ENCOUNTER_CHANCE = 25;
        const int ENCOUNTER_CHANCE_FUll = 180;

        Printer printer = new Printer();


        Map theMap = new Map();
        Player player;
        Shop theShop = new Shop();
        Random rand = new Random();

        

        ActiveMenu currentMenu = ActiveMenu.General;

        bool hasMap = false;
        bool inLocation = false;
        bool musicPlaying = false;
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

        void PlayMusic(bool play)
        {
            var soundPlayer = new SoundPlayer
            {
                SoundLocation = @"../../audio/overWorld.wav"
            };
            if (play)
            {
                try
                {
                    soundPlayer.PlayLooping();
                }
                catch
                {

                }
                
                musicPlaying = true;
                return;
            }
            soundPlayer.Stop();
            musicPlaying = false;
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
            if (!musicPlaying)
            {
                PlayMusic(true);
            }
            Console.Clear();
            if (player.HasDied || player.HasWon)
            {
                currentMenu = ActiveMenu.Dead;
                return;
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
                case ActiveMenu.Abilities:
                    InventoryAbilities();
                    break;
                case ActiveMenu.Abilitie_Details:

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
                case ActiveMenu.Inventory_Weapon_Detail:
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
            while (!player.HasDied && !player.HasWon)
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
            while (!player.HasDied && !player.HasWon)
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
            while (!player.HasDied && !player.HasWon)
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

            while (!player.HasDied && !player.HasWon)
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

        //Generates list of weapons in inventory and dynamically sets number to each.
        void InventoryUiWeapons()
        {
            int choice;
            int count = 0;
            string[] formatedString;

            //Format incomming string for better viewing
            if (player.ReturnWeaponList().Length % 3 != 0)
            {
                formatedString = new string[(player.ReturnWeaponList().Length / 3) + 2];
            }
            else
            {
                formatedString = new string[(player.ReturnWeaponList().Length / 3) + 1];
            }
            
            for (int i = 0; i < formatedString.Length -1 ; i++)
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
            formatedString[formatedString.Length-1] += string.Format("{0}) Return     ", count+1);
            //print formated string
            printer.PrintArray(formatedString);

            while (!player.HasDied && !player.HasWon)
            {
                //loop through posible choices
                choice = ReturnChoice()-1;
                
                for (int i = 0; i < player.ReturnWeaponList().Length; i++)
                {
                    if (choice == i)
                    {
                        InventroyItemStats(choice);
                        break;
                    }
                    if (choice == player.ReturnWeaponList().Length)
                    {
                        currentMenu = ActiveMenu.Inventory;
                        DrawUi();
                        break;
                    }
                }
                Console.WriteLine(player.ReturnWeaponList().Length);
                if (choice == 0)
                {
                    Console.WriteLine("Exiting menu");
                    currentMenu = ActiveMenu.Inventory;
                    DrawUi();
                    break;
                }
            }
        }

        void InventroyItemStats(int item)
        {
            bool isEquiped = false;
            Weapon weapon = player.ReturnWeaponInt(item);

            printer.PrintSingle(string.Format("{0}",weapon.Name), true, false);
            printer.PrintSingle(string.Format("Attack:  {0}", weapon.AtkBonus), false, false);
            printer.PrintSingle(string.Format("Defense: {0}", weapon.DefBonus), false, false);
            printer.PrintSingle(string.Format("Speed:   {0}",weapon.SpdBonus), false, false);

            //Check if weapon is equiped
            if (player.EquipedWeapon == weapon)
            {
                isEquiped = true;
                printer.PrintSingle("1) Unequip weapon           2) Return", false, true);
            }
            else
            {
                printer.PrintSingle("1) Equip weapon             2) Return", false, true);
            }

            int choice;
            while (true)
            {
                choice = ReturnChoice();
                switch (choice)
                {
                    case 1:
                        if (isEquiped)
                        {

                            player.UnequipWeapon(player.EquipedWeapon);
                            currentMenu = ActiveMenu.Inventory_Weapons;
                            DrawUi();
                            return;
                        }
                        else
                        {
                            if(player.EquipedWeapon != null)
                            {

                                player.ChangeWeapons(player.EquipedWeapon, item);
                                currentMenu = ActiveMenu.Inventory_Weapons;
                                DrawUi();
                                return;
                            }

                            player.EquipWeapon(item);
                            currentMenu = ActiveMenu.Inventory_Weapons;
                            DrawUi();
                            return;
                        }
                        break;
                    case 2:
                        currentMenu = ActiveMenu.Inventory_Weapons;
                        DrawUi();
                        break;
                }
                UpdateMap();
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

        void InventoryAbilities()
        {
            while (true)
            {
                
                string[] bottomAbilitiesList = new string[] { "[ 1){0}] [ 2){1} ] [ 3){2}]", "[ 4){0}] [ 5){1} ] [ 6){2}]", "TmpBack" };
                string[,] abilitiesList = player.ReturnUnlockedAbilities();
                for (int i = 0; i < bottomAbilitiesList.Length - 1; i++)
                {
                    bottomAbilitiesList[i] = string.Format(bottomAbilitiesList[i], abilitiesList[i, 0], abilitiesList[i, 1], abilitiesList[i, 2]);
                }
                bottomAbilitiesList[2] = string.Format("[ 7): Return to menu ]");
                printer.PrintArray(bottomAbilitiesList);
                


                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    
                    case 1:
                        if (abilitiesList[0, 0] != "Locked")
                        {
                            currentMenu = ActiveMenu.Abilitie_Details;
                            InventoryAbilitiesDetails(1);
                            break;
                        }
                        break;
                    //Shatter
                    case 2:
                        if (abilitiesList[0, 1] != "Locked")
                        {
                            currentMenu = ActiveMenu.Abilitie_Details;
                            InventoryAbilitiesDetails(2);
                            break;
                        }
                        break;

                    case 3:
                        if (abilitiesList[0, 1] != "Locked")
                        {
                            currentMenu = ActiveMenu.Abilitie_Details;
                            InventoryAbilitiesDetails(3);
                            break;
                        }
                        break;

                    case 4:
                        if (abilitiesList[1, 0] != "Locked")
                        {
                            currentMenu = ActiveMenu.Abilitie_Details;
                            InventoryAbilitiesDetails(4);
                            break;
                        }
                        break;

                    case 5:
                        if (abilitiesList[1, 1] != "Locked")
                        {
                            currentMenu = ActiveMenu.Abilitie_Details;
                            InventoryAbilitiesDetails(5);
                            break;
                        }
                        break;

                    case 6:
                        if (abilitiesList[1, 2] != "Locked")
                        {
                            currentMenu = ActiveMenu.Abilitie_Details;
                            InventoryAbilitiesDetails(6);
                            break;
                        }
                        break;

                    case 7:
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        return;
                    default:
                        currentMenu = ActiveMenu.General;
                        DrawUi();
                        return;
                }
                DrawUi();
            }
        }

        void InventoryAbilitiesDetails(int abilitiyNumber)
        { 
            while(true)
            {
                DrawUi();
                string[] abilty;
                switch (abilitiyNumber)
                {
                    case 1:
                        abilty = new string[] { "Heal", "", "heals 5 hp", " multiplied by x which increases each 5 levels starting at 1","","[ 1) Return ]" };
                        printer.PrintArray(abilty);
                        break;
                    case 2:
                        abilty = new string[] { "Shatter", "", "Reduces enemy defense by 1/3", "for the next attack only", "", "[ 1) Return ]" };
                        printer.PrintArray(abilty);
                        break;
                    case 3:
                        abilty = new string[] { "Avenger Soul", "", "Do bonus damage equal to half", "of your total missing hp", "", "[ 1) Return ]" };
                        printer.PrintArray(abilty);
                        break;
                    case 4:
                        abilty = new string[] { "Devour", "", "Heals 50% of damage done", "", "[ 1) Return ]" };
                        printer.PrintArray(abilty);
                        break;
                    case 5:
                        abilty = new string[] { "Wind God", "", "Attacks five times at once", "each attack does 50% of its normal damage", "", "[ 1) Return ]" };
                        printer.PrintArray(abilty);
                        break;
                    case 6:
                        abilty = new string[] { "Heal", "", "heals 5 hp", " multiplied by x which increases each 5 levels starting at 1", "", "[ 1) Return ]" };
                        printer.PrintArray(abilty);
                        break;
                }
                int playerChoice = ReturnChoice();
                if(playerChoice == 1)
                {
                    currentMenu = ActiveMenu.Abilities;
                    DrawUi();
                    return;
                }
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
                    PlayMusic(false);
                    enemylist.GrassLandEncounter(danger);
                    break;
            }
        }
        void ForestEncounterGen(int danger)
        {
            EnemyGenerator enemyGenerator = new EnemyGenerator(player);
            switch (danger)
            {
                case 1:
                    PlayMusic(false);
                    enemyGenerator.ForestEncounter(danger);
                    break;
            }
        }
        void WaterEncounterGen(int danger)
        {
            EnemyGenerator enemyGenerator = new EnemyGenerator(player);
            switch (danger)
            {
                case 1:
                    PlayMusic(false);
                    enemyGenerator.WaterEncounter(danger);
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
                    theShop.InitShop(player);
                    currentMenu = ActiveMenu.General;
                    DrawUi();
                    break;
                case 5:
                    //Restores player hp and energy
                    inLocation = true;
                    printer.PrintSingle("You feel restored after a good sleep!");
                    player.Health = player.MaxHealth;
                    player.Energy = player.MaxEnergy;
                    Console.ReadLine();
                    break;
                case 6:
                    //ENTER BOSS FIGHT!!
                    EnemyGenerator enemyGenerator = new EnemyGenerator(player);
                    enemyGenerator.BossBattle();
                    player.HasWon = true;
                    DrawUi();
                    break;
                default:

                    break;
            }
            return;
        }

    }
}
