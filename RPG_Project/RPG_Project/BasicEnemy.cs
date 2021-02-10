using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class BasicEnemy : Enemy
    {

        int moneyValue;


        //Constructor 
        public BasicEnemy()
        {

        }

        public int MoneyValue
        {
            get { return moneyValue; }
            private set
            {
                moneyValue = value;
            }
        }

    }

}
