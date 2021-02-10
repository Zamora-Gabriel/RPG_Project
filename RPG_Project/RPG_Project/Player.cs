
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Player
    {
        /*Variables*/
        string name;
        int life;
        int attack;
        int speed;
        int exp;
        int level;
        int money;

        /*Properties*/

        public string Name
        { 
            get { return name; } 
            private set
            {
                name = value;
            } 
        }

        public int Life
        {
            get { return life; }
            set
            {
                life = value;

                //Check if player dies
                if(life <= 0)
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

        /*Constructors*/
        Player(string name1)
        {
            //Provisional values
            Name = name1;
            Life = 5;
            Attack = 5;
            Exp = 0;
            Level = 0;
        }

        /*Methods*/
        private void PlayerDies()
        {
            Console.WriteLine("{0}, has been defeated", Name);
        }

        private void LevelUp()
        {
            Level++;
            Console.WriteLine("Congratulations {0}! Now you're level {1}", Name, Level);
        }

        //TODO: Inflicting damage, receiving damage
    }
}
