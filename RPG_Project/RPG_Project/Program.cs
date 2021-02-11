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

            Weapon weap2 = new Weapon("pistol");
            Console.WriteLine(weap2.Name);

            //Player class Test
            x.Exp = 120;
            Console.WriteLine("Exp: {0}", x.Exp);

            x.PrintStats();

            x.EquipWeapon(weap1);
            x.EquipWeapon(weap2);
            x.ChangeWeapons(weap1, weap2);

            x.PrintStats();

            x.RcvDamage(5);
            Console.WriteLine("Current Health {0}:", x.Health);
            
            x.RcvDamage(5);
            Console.WriteLine("Current Health {0}:", x.Health);
            
            x.RcvDamage(5);
            Console.WriteLine("Current Health {0}:", x.Health);
            
            Console.ReadLine();
        }
    }
}
