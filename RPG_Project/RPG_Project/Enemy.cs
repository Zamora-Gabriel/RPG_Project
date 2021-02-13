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
        int moneyValue;
        //Combat Stats
        bool hasDied;

        int maxHealth;
        int health;
        int speed;
        int attack;
        int defense;
        
        // Boss might not drop money. Tenativly commentted out
        //int moneyDropped;

        //Constructor
        public Enemy(string name)
        {

            this.name = name;
            expValue = 5;
            moneyValue = 5;
            //These are all temp and need to be removed eventually
            maxHealth = 10;
            health = 5;
            speed = 10;
            attack = 1;
            defense = 1;

        }

        public Enemy(string name, int maxHealth, int attack, int defense, int speed, int expValue, int moneyValue)
        {

            this.name = name;
            this.expValue = expValue;
            this.moneyValue = moneyValue;
            this.maxHealth = maxHealth;
            health = maxHealth;
            this.speed = speed;
            this.attack = attack;
            this.defense = defense;

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
        public int MoneyValue
        {
            get { return moneyValue; }
            set
            {
                moneyValue = value;
            }
        }
        public bool HasDied
        {
            get { return hasDied; }
            private set
            {
                hasDied = value;
                Die();
            }
        }

        //Combat properties
        public int MaxHealth
        {
            get { return maxHealth; }
            private set
            {
                maxHealth = value;
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health <= 0)
                {
                    Die();
                }
                if(health < 0)
                {
                    Health = 0;
                }
                    
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

        public int Attack
        {
            get { return attack; }
            private set
            {
                attack = value;
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

        protected int AttackPlayer(Player player)
        {
        
            int outgoingDmg = attack;
            //reduce by armour;
            outgoingDmg -= player.Defense;
            if(outgoingDmg < 0)
            {
                outgoingDmg = 0;
            }

            return outgoingDmg;
        }

        protected void Block()
        {
            //TODO decide how this will work,
            //will it just increase defense stat for the turn or two?
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                HasDied = true;
            }
        }

        protected virtual void Die()
        {
            //TODO
            //Give exp
            //Give gold
            //Remove self <- possibly done in the game object
            Name = "Dead";
        }



    }
}
