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

        //Fat frog art "HP: {0}\\{1}", "        ", "   o~\\  ", " |_-__\\", "        ", "{2}" };

        string[] wolfArt = new string[] { "HP: {0}\\{1} ", "   /\\_/\\    ", "  / 0  0\\   ", " |   []  | |", "  \\     / / ", "  /    \\/   ", " |_║__║_|   ", "{2}" };
        string[] jaguarArt = new string[] { "HP: {0}\\{1}   ", "            ", "    /\\/\\    ", "|  | o o|   ", "  \\ \\ =-=/   ", "  \\/    \\   ", "  |_║__║_|  ", "{2}" };
        string[] snakeArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "      __    ", "     /  o>~ ", "___/ /    ", "/_______\\   ", "{2}" };

        public EnemyGenerator(Player player)
        {
            this.player = player;
        }
        ///
        /// FOREST ENEMIES - EASY
        ///
        
        BasicEnemy[] forestEnemy = new BasicEnemy[3];
        BasicEnemy[] grassLandEnemy = new BasicEnemy[3];
        BasicEnemy[] waterEnemy = new BasicEnemy[3];
        BasicEnemy boss;

        void GrassLandEnemies()
        {
            grassLandEnemy[0] = new BasicEnemy("  Spearman   ", 13, 2, 1, 5, 10, 5, wolfArt); //Weak Spearman
            grassLandEnemy[1] = new BasicEnemy("  Swordman   ", 10, 3, 1, 5, 5, 5, wolfArt); //Weal Swordman;
            grassLandEnemy[2] = new BasicEnemy("   Archer    ", 5, 6, 0, 5, 5, 5, wolfArt); //Weal Archer;
        }

        void ForestEnemies()
        {
            forestEnemy[0] = new BasicEnemy("    Wolf     ", 10, 1, 0, 10, 10, 5, wolfArt); //Weak wolf
            forestEnemy[1] = new BasicEnemy("   Jaguar    ", 7, 3, 0, 20, 10, 5, jaguarArt); //Weal jaguar;
            forestEnemy[2] = new BasicEnemy("    Snake    ", 5, 2, 0, 10, 5, 3, snakeArt); //Weal snake;
        }

        void WaterEnemies()
        {

            waterEnemy[0] = new BasicEnemy("  Fat Frog   ",5,1,0,10,2,2, wolfArt); //Weak wolf
            waterEnemy[1] = new BasicEnemy("    Fish     ",5,4,0,25,5,5,wolfArt); //Weal jaguar;
            waterEnemy[2] = new BasicEnemy("   Lobster   ",15,2,4,10,10,5,wolfArt); //Weal snake;
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
            //Boss();
            //BasicEnemy[] theEnemies = new BasicEnemy[1];
            //theEnemies[0] = boss;
            //battleManager = new BattleManager(player, theEnemies);
            //battleManager.BattleLoop();
        }


        public void forceEncounter(int enemies)
        {
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            for (int i = 0; i < enemies; i++)
            {
                ForestEnemies();
                int rnd = rand.Next(0, forestEnemy.Length);
                theEnemies[i] = forestEnemy[rnd];
            }

            battleManager = new BattleManager(player, theEnemies);
            battleManager.BattleLoop();
        }

    }
}
