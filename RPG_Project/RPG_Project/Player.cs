
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{

    //TODO: Uncomment when weapons are available
    //TODO: Think of inventory Implementation
    //Maybe: Energy/Skills
    class Player
    {
        /***Variables***/
        
        string name;
        int exp;
        int level;
        int money;
        string equipedWeapon;

        //Stats
        int health;
        int attack;
        int defense;
        int speed;
        

        /***Properties***/

        public string Name
        { 
            get { return name; } 
            private set
            {
                name = value;
            } 
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;

                //Check if player dies
                if(health <= 0)
                {
                    PlayerDies();
                }
            }
        }

        public int Attack
        {
            get { return attack; }

            //Can't modify attack outside of the class
            private set 
            {
                attack = value;
            }
        }

        public int Defense
        {
            get { return defense; }

            //Can't modify attack outside of the class
            private set
            {
                defense = value;
            }
        }

        public int Speed
        {
            get { return speed; }


            //Can't modify speed outside of the class
            private set
            {
                speed = value;
            }
        }

        public int Exp
        {
            get { return exp; }

            set 
            {
                exp = value;

                if(exp >= 100)
                {
                    //save residual experience and level up
                    exp = exp - 100;
                    LevelUp();
                }
            }
        }

        public int Level
        {
            get { return level; }
            private set
            {
                level = value;
            }
        }

        public int Money
        {
            get { return money; }
            private set
            {
                money = value;
            }
        }

        public string EquipedWeapon
        {
            get { return equipedWeapon; } 
            private set
            {
                equipedWeapon = value;
            }
        }

        /***Constructors***/

        public Player(string name1)
        {
            //Provisional values
            Name = name1;
            Health = 15;
            Attack = 5;
            Speed = 5;
            Defense = 5;
            Exp = 0;
            Level = 0;
            EquipedWeapon = "";
        }

        /***Methods***/

        private void PlayerDies()
        {
            Console.WriteLine("{0} has been defeated", Name);
        }

        private void LevelUp()
        {
            Level++;
            Attack++;
            Defense++;
            Speed++;
            Console.WriteLine("Congratulations {0}! Now you're level {1}", Name, Level);
        }

        public int AtkDamage(/*Enemy object*/)
        {
            //Calculate damage to enemy
            //damage -= Attack - Enemy.Defense;
            //return damage;

            return 0; //--Placeholder
        }

        public void RcvDamage(int damage)
        {
            Health -= damage;
        }

        public void EquipWeapon(Weapon weap)
        {
            //Case the user already has an equiped weapon
            if(EquipedWeapon != "")
            {
                Console.WriteLine("Wait, you already have a weapon equiped!");
                return;
            }
            //Sum weapons' bonuses
            Attack += weap.AtkBonus;
            Defense += weap.DefBonus;
            Speed += weap.SpdBonus;
            EquipedWeapon = weap.Name;
            Console.WriteLine("Succesfully equiped weapon: {0}", EquipedWeapon);
        }

        //TODO: Make it public if user can fight barehanded
        private bool UnequipWeapon(Weapon weap)
        {
            if (EquipedWeapon == "")
            {
                Console.WriteLine("Wait... You don't have an equiped weapon!");
                return false;
            }

            //User tries to unequip a weapon that hasn't equiped
            if (EquipedWeapon != weap.Name)
            {
                Console.WriteLine("Wait... You don't have that weapon equiped!");
                return false;
            }
            //Substract weapons' bonuses
            Attack -= weap.AtkBonus;
            Defense -= weap.DefBonus;
            Speed -= weap.SpdBonus;

            //Restart equiped weapon's id to discriminate
            EquipedWeapon = "";
            Console.WriteLine("Succesfully unequiped weapon: {0}", weap.Name);

            return true;
        }

        public void ChangeWeapons(Weapon equiped, Weapon newEquip)
        {
            //Unequipment succeed
            if (UnequipWeapon(equiped))
            {
                EquipWeapon(newEquip);
                return;
            }
        }

        public void PrintStats()
        {
            Console.WriteLine("{0} Stats", Name);
            Console.WriteLine("Level: {0}", Level);
            Console.WriteLine("Exp: {0}", Exp);
            Console.WriteLine("Attack: {0}", Attack);
            Console.WriteLine("Defense: {0}", Defense);
            Console.WriteLine("Speed: {0}", Speed);
            Console.WriteLine("Money: {0}", Money);
        }

        public void DrinkPotion(/*Inventory, string PotName*/)
        {
            //Check potions list
            /*if(inv.Potion > 0)
             {
              //Only health potions?
              inv.Potion--;
              Health += 10;
             }
             else
             {
                Console.WriteLine("You have no potions!");
             }*/
        }

    }
}
