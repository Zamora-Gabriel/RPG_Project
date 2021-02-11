using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class BossEnemy : Enemy
    {
        int healthBars;

        //TODO Specials attacks?
        



       void changePhase()
       {
           if(healthBars > 0)
            {
                //TODO set health to new value
                //Change behavior or attacks
            } 
       }

        protected override void Die()
        {
            //TODO when hits 0hp check if any health bars remain,
            //if so change phase instead of dying.
        }
        
    }
}
