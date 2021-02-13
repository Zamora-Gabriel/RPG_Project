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
            Player player = new Player("Raph");
            Weapon weap1 = new Weapon("gauntlet");
            
            Console.WriteLine(weap1.Name);

            string[] enemyOneArt = new string[] {"HP: {0}\\{1}","", "o~\\", "|_-__\\", "", "{2}" };
            Weapon weap2 = new Weapon("pistol");
            Console.WriteLine(weap2.Name);

            Potion pot1 = new Potion(1,1);
            Potion pot2 = new Potion(0,1);
            player.PrintStats();



            //Player class Test
            player.Exp = 120;
            Console.WriteLine("Exp: {0}", player.Exp);

            player.PrintInvent();

            
            
            player.AddPotionToInvent(pot1);
            player.AddPotionToInvent(pot2);
            player.AddPotionToInvent(pot2);
            player.AddPotionToInvent(pot2);
            player.AddWeaponToInvent(weap2);
            player.EquipWeapon(0);
            player.EquipWeapon(1);
            player.ChangeWeapons(0, 1);
            player.AddWeaponToInvent(weap2);

            player.PrintStats();

            player.PrintInvent();

            player.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", player.Health);

            player.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", player.Health);

            player.DrinkPotion(0);

            player.PrintInvent();

            player.RcvDamage(6);
            Console.WriteLine("Current Health {0}:", player.Health);
            
            Printer printer = new Printer();

            BasicEnemy[] enemies = new BasicEnemy[3];
            enemies[0] = new BasicEnemy("tmp ", printer);
            enemies[1] = new BasicEnemy("tmp ", printer);
            enemies[2] = new BasicEnemy("tmp ", printer);

            Console.ReadLine();
            Console.Clear();
            OverWorldManager worldManager = new OverWorldManager(player);
            worldManager.DrawUi();
            Console.ReadLine();
            BattleManager newManager = new BattleManager(printer, player, enemies);

            newManager.BattleLoop();
        }
    }
}
