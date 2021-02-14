using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Axe : Weapon
    {
        //Default constructor
        public Axe() { }

        public Axe(string weapName)
        {
            this.Name = weapName;
            this.cost = 10;
        }

        /***Methods***/
        //Establish bonus stats for the weapon
        public override int CreateWeapForShop(string material)
        {
            base.CreateWeapForShop(material);

            //Update bonus points for axes (Attack focus)
            AtkBonus += 7;
            SpdBonus += 2;
            DefBonus += 5;

            return cost;
        }
    }
}
