
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
        }

        /***Methods***/

        private void PlayerDies()
        {
            Console.WriteLine("{0} has been defeated", Name);
        }

        private void LevelUp()
        {
            Level++;
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

        private void EquipWeapon(/*Weapon*/)
        {
            //Sum weapons' bonuses
            //Attack += weapon.AtkBonus;
            //Defense += Weapon.DefBonus;
            //Speed += weapon.SpdBonus; 
        }

        private void UnequipWeapon(/*Weapon*/)
        {
            //Substract weapons' bonuses
            //Attack -= weapon.AtkBonus;
            //Defense -= Weapon.DefBonus;
            //Speed -= weapon.SpdBonus;
        }

        public void ChangeWeapons(/*equipedWeapon, NewEquipWeapon*/)
        {
            //UnequipWeapon(NewEquipWeapon);
            //EquipWeapon(equipedWeapon);
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
