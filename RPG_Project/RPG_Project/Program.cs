using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test
            Player x = new Player("Raph");
            Weapon weap1 = new Weapon("gauntlet");
            
            Console.WriteLine(weap1.Name);

            string[] enemyOneArt = new string[] {"HP: {0}\\{1}","", "o~\\", "|_-__\\", "", "{2}" };
            Weapon weap2 = new Weapon("pistol");
            Console.WriteLine(weap2.Name);

            Potion pot1 = new Potion(1,1);
            
            x.PrintStats();



            //Player class Test
            x.Exp = 120;
            Console.WriteLine("Exp: {0}", x.Exp);

            x.PrintInvent();

            x.AddPotionToInvent(pot1);
            x.AddWeaponToInvent(weap2);
            x.EquipWeapon(0);
            x.EquipWeapon(1);
            x.ChangeWeapons(0, 1);

            x.PrintStats();

            x.PrintInvent();

            x.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", x.Health);
            
            x.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", x.Health);

            x.DrinkPotion(0);

            x.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", x.Health);
            
            Printer printer = new Printer();
            Player player = new Player("Player Name");
            BasicEnemy[] enemies = new BasicEnemy[3];
            enemies[0] = new BasicEnemy("tmp ", printer);
            enemies[1] = new BasicEnemy("tmp ", printer);
            enemies[2] = new BasicEnemy("tmp ", printer);

            //BasicEnemy enemy = new BasicEnemy();
            BattleManager newManager = new BattleManager(printer, player, enemies);

            newManager.BattleLoop();
        }
    }
}
