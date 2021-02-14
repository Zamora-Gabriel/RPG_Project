using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Lance : Weapon
    {
        //Default constructor
        public Lance() { }

        public Lance(string weapName)
        {
            this.Name = weapName;
            this.cost = 10;
        }

        /***Methods***/
        //Establish bonus stats for the weapon
        public override int CreateWeapForShop(string material)
        {
            base.CreateWeapForShop(material);

            //Update bonus points for lances (Spd focus)
            AtkBonus += 5;
            SpdBonus += 7;
            DefBonus += 2;

            return cost;
        }
    }
}
