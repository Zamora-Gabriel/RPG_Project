using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Weapon
    {
        /***Variables***/

        string name;
        int atkbonus;
        int defbonus;
        int spdbonus;
        int cost; //how much will it cost on shop

        /***Properties***/

        public string Name { get; protected set; }

        public int AtkBonus
        {
            get { return atkbonus; }

            protected set
            {
                atkbonus = value;
            }
        }

        public int DefBonus
        {
            get { return spdbonus; }

            protected set
            {
                defbonus = value;
            }
        }

        public int SpdBonus
        {
            get { return spdbonus; }

            protected set
            {
                spdbonus = value;
            }
        }

        /***Constructor***/

        public Weapon()
        {
            Name = "None";
            AtkBonus = 1;
            DefBonus = 1;
            SpdBonus = 1;
            cost = 1;
        }

        public Weapon(string weapName)
        {
            Name = weapName;
            AtkBonus = 1;
            DefBonus = 1;
            SpdBonus = 1;
            cost = 1;
        }

        /***Methods***/

        public void CheckName()
        {

        }
    }
}
