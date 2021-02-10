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
        int expValue;

        //Combat Stats
        int health;
        int speed;
        int damage;
        int defense;
        
        // Boss might not drop money. Tenativly commentted out
        //int moneyDropped;

        //Constructor
        public Enemy()
        {
            name = "Default";
            expValue = 5;

            //These are all temp and need to be removed eventually
            health = 5;
            speed = 1;
            damage = 1;
            defense = 1;
            
        }

        /*Getters and Setters*/
        //Non combat properties
        public string Name
        {
            get { return name; }
            private set
            {
                name = value;
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

        //Combat properties

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

        public int Damage
        {
            get { return damage; }
            private set
            {
                damage = value;
            }
        }

        public int Defense
        {
            get { return defense; }
            private set
            {
                defense = value;
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

        //Can do all damage taking by simply using the setter with health and a negative number, 
        //if this does not work can easily use below function

        //public void TakeDamage(int damage)
        //{
        //    Health -= damage;
        //}

    }
}
