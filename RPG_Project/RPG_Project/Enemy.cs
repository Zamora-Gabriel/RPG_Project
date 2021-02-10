using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Enemy
    {
        string name;
        int health;
        int speed;
        int expValue;
        // Boss might not drop money. Tenativly commentted out
        //int moneyDropped;

        //Constructor
        Enemy()
        {
            name = "Default";
            health = 5;
            speed = 1;
            expValue = 5;
        }

        /*Getters and Setters*/
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
                if (health <= 0)
                    Die();
            }
        }

        public int Speed
        {
            get { return speed; }
            private set
            {
                speed = value;
            }
        }

        public int ExpValue
        {
            get { return expValue; }
            private set
            {
                expValue = value;
            }
        }
        /*Methods*/

        int Attack()
        {
            //TODO decide how to actually damage player
            //Current idea is this returns an negative value that is passed to the players 
            //life set. This would be done by the game object.
            //ie thePlayer.life = theEnemy.Attack();
            return 0;
        }

        void Die()
        {
            //TODO
            //Give exp
            //Give gold
            //Remove self <- possibly done in the game object
        }


    }
}
