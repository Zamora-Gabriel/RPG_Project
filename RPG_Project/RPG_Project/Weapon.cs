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
        const int WOODEN_COST = 10;
        const int BRONZE_COST = 20;
        const int STEEL_COST = 40;
        const int LEGEND_COST = 100; //-- Celestial, Titan and Dragon weapons
        

        string name;
        int atkbonus;
        int defbonus;
        int spdbonus;
        protected int cost; //how much will it cost on shop

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
            cost = 0;
        }

        /***Methods***/

        //Check material and update price of the weapon
        public virtual int CreateWeapForShop(string material)
        {
            switch (material)
            {
                case "Wooden":
                    cost = WOODEN_COST;
                    AtkBonus = 1;
                    DefBonus = 0;
                    SpdBonus = 1;
                    break;

                case "Bronze":
                    cost = BRONZE_COST;
                    AtkBonus = 3;
                    DefBonus = 1;
                    SpdBonus = 3;
                    break;

                case "Steel":
                    cost = STEEL_COST;
                    AtkBonus = 6;
                    DefBonus = 2;
                    SpdBonus = 6;
                    break;

                case "Celestial":
                    cost = LEGEND_COST;
                    AtkBonus = 8;
                    DefBonus = 3;
                    SpdBonus = 10;
                    break;

                case "Titan":
                    cost = LEGEND_COST;
                    AtkBonus = 8;
                    DefBonus = 6;
                    SpdBonus = 8;
                    break;

                case "Dragon":
                    cost = LEGEND_COST;
                    AtkBonus = 10;
                    DefBonus = 3;
                    SpdBonus = 8;
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;

            }

            return cost;
        }
    }
}
