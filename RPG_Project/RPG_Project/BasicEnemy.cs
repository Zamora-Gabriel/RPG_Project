using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    enum AiPersonality
    {
        Defensive,
        Aggresive,
        Supportive //Possibly allows the enemy to heal allies?
    }

    class BasicEnemy : Enemy
    {

        int moneyValue;

        //Constructor 
        public BasicEnemy()
        {

        }

        /*Getters and Setters*/
        public int MoneyValue
        {
            get { return moneyValue; }
            private set
            {
                moneyValue = value;
            }
        }



        void HealOther(int healAmmount)
        {

        }
    }

}
