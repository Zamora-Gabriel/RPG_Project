using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class EnemyGenerator
    {
        Printer printer = new Printer();
        BattleManager battleManager;
        Random rand = new Random();

        Player player;

        //string[] template = new string[] {"HP: {0}\\{1} ","            ","{2}"};
        ///
        /// GRASSLAND ENEMIES ART
        /// 
        string[] spearmanArt = new string[] { "HP: {0}\\{1} ", "    o ^     ", "   /|\\|     ", "    | |     ", "   / \\|     ", "{2}" };
        string[] swordmanArt = new string[] { "HP: {0}\\{1} ", "    o │     ", "   /|\\┼     ", "    |       ", "    / \\      ", "{2}" };
        string[] archerArt = new string[] { "HP: {0}\\{1}   ", "    o |\\    ", "   /|\\|─┼─> ", "    | |/    ", "    / \\      ", "{2}" };

        /// <summary>
        /// FOREST ENEMIES ART 
        /// </summary>
        string[] wolfArt = new string[] { "HP: {0}\\{1} ", "   /\\_/\\    ", "  / 0  0\\   ", " |   []  | |", "  \\     / / ", "  /    \\/   ", " |_║__║_|   ", "{2}" };
        string[] jaguarArt = new string[] { "HP: {0}\\{1}   ", "            ", "    /\\/\\    ", "|  | o o|   ", "  \\ \\ =-=/   ", "  \\/    \\   ", "  |_║__║_|  ", "{2}" };
        string[] snakeArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "      __    ", "     /  o>~ ", "___/ /    ", "/_______\\   ", "{2}" };

        /// <summary>
        /// WATER ENEMIES ART
        /// </summary>
        string[] lobsterArt = new string[] { "HP: {0}\\{1}  ", "  |/ ___ \\| ","  \\ /o o\\ / ","   \\|   |/  ","    \\___/   ","  <_/||||\\_>","  <_/ || \\_>", "   <_/ \\_>  ", "{2}" };
        string[] fishArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "            ", "            ", "|\\__/\\__    ", "| _____o>   ", "|/          ", "{2}" };
        string[] fatFrogArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "            ", "            ", "    _       ", "  o~\\\\      ", " |_-__\\\\    ", "{2}" };

        public EnemyGenerator(Player player)
        {
            this.player = player;

        }

        BasicEnemy[] forestEnemy = new BasicEnemy[3];
        BasicEnemy[] grassLandEnemy = new BasicEnemy[3];
        BasicEnemy[] waterEnemy = new BasicEnemy[3];
        BasicEnemy boss;

        ///
        /// GRASSLAND ENEMIES - EASY
        ///
        void GrassLandEnemies()
        {
            grassLandEnemy[0] = new BasicEnemy("  Spearman   ", 13, 2, 1, 5, 100, 1, spearmanArt); //Weak Spearman
            grassLandEnemy[1] = new BasicEnemy("  Swordman   ", 10, 3, 1, 5, 50, 5, swordmanArt); //Weal Swordman;
            grassLandEnemy[2] = new BasicEnemy("   Archer    ", 5, 6, 0, 5, 50, 5, archerArt); //Weal Archer;

        }

        ///
        /// FOREST ENEMIES - EASY
        ///
        void ForestEnemies()
        {
            forestEnemy[0] = new BasicEnemy("    Wolf     ", 10, 1, 0, 10, 10, 5, wolfArt); //Weak wolf
            forestEnemy[1] = new BasicEnemy("   Jaguar    ", 7, 3, 0, 20, 10, 5, jaguarArt); //Weal jaguar;
            forestEnemy[2] = new BasicEnemy("    Snake    ", 5, 2, 0, 10, 5, 3, snakeArt); //Weal snake;
        }

        ///
        /// WATER ENEMIES - EASY
        ///
        void WaterEnemies()
        {

            waterEnemy[0] = new BasicEnemy("  Fat Frog   ",5,1,0,10,2,2, fatFrogArt); //Weak wolf
            waterEnemy[1] = new BasicEnemy("    Fish     ",5,4,0,25,5,5, fishArt); //Weal jaguar;
            waterEnemy[2] = new BasicEnemy("   Lobster   ",15,2,4,10,10,5, lobsterArt); //Weal snake;
        }

        void Boss()
        {
            boss = new BasicEnemy("Demon Lord", 100, 50, 25, 20, 1000, 1000, wolfArt);
        }

        /// <summary>
        /// GRASS LANDS ENCOUNTER EASY
        /// </summary>
        public void EasyGrassLandsEncounter()
        {
            //Decide how many enemies
            int enemies = rand.Next(1, 3);

            //Choose which enemies
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            for (int i = 0; i < enemies; i++)
            {
                GrassLandEnemies();
                int rnd = rand.Next(0, grassLandEnemy.Length);
                theEnemies[i] = grassLandEnemy[rnd];
            }

            //Start Battle
            battleManager = new BattleManager(player, theEnemies);
            battleManager.BattleLoop();
        }

        ///
        /// FOREST ENCOUNTERS EASY
        ///
        public void EasyForestEncounter()
        {
            //Decide how many enemies
            int enemies = rand.Next(1, 3);

            //Choose which enemies
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            for(int i=0; i < enemies; i++)
            {
                ForestEnemies();
                int rnd = rand.Next(0, forestEnemy.Length);
                theEnemies[i] = forestEnemy[rnd];
            }

            //Start Battle
            battleManager = new BattleManager(player, theEnemies);
            battleManager.BattleLoop();
        }



        /// <summary>
        /// WATER ENCOUNTER EASY 
        /// </summary>

        public void EasyWaterEncounter()
        {
            //Decide how many enemies
            int enemies = rand.Next(1, 3);

            //Choose which enemies
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            for (int i = 0; i < enemies; i++)
            {
                WaterEnemies();
                int rnd = rand.Next(0, waterEnemy.Length);
                theEnemies[i] = waterEnemy[rnd];
            }

            //Start Battle
            battleManager = new BattleManager(player, theEnemies);
            battleManager.BattleLoop();
        }


        public void BossBattle()
        {
            Boss();
            BasicEnemy[] theEnemies = new BasicEnemy[1];
            theEnemies[0] = boss;
            battleManager = new BattleManager(player, theEnemies);
            battleManager.BattleLoop();
        }


        //FOR TESTING ONLY
        public void forceEncounter(int enemies)
        {
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            for (int i = 0; i < enemies; i++)
            {
                GrassLandEnemies();
                int rnd = rand.Next(0, grassLandEnemy.Length);
                theEnemies[i] = grassLandEnemy[rnd];
            }

            //Start Battle
            battleManager = new BattleManager(player, theEnemies);
            battleManager.BattleLoop();
        }

    }
}
