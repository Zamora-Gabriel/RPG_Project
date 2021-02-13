using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class EnemyList
    {
        Printer printer = new Printer();
        BattleManager battleManager;
        Random rand = new Random();

        Player player;

        public EnemyList(Player player)
        {
            this.player = player;
        }
        ///
        /// FOREST ENEMIES - EASY
        ///
        
        BasicEnemy[] forestEnemy = new BasicEnemy[1];
        void ForestEnemies()
        {
            
            forestEnemy[0] = new BasicEnemy("Wolf", 10, 3, 1, 15, 10, 5);
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

    }
}
