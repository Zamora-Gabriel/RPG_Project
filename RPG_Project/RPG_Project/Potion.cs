using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PotType
{
    HealthPotion = 0,
    EnergyPotion
}

enum PotQuality
{
    Normal = 0,
    Super,
    Mega
}

namespace RPG_Project
{
    class Potion
    {
        const int NORMAL_QUALITY = 10;
        const int SUPER_QUALITY = 20;
        const int MEGA_QUALITY = 30;
        /***Variable***/
        int type;
        int quality; //--Super, Mega, etc. 
        public int amount { get; private set; }  // number of points restoring 


        /***Property***/
        public int Type
        {
            get { return type; }
            private set
            {
                if (type != (int)PotType.HealthPotion) //--- Health 
                {
                    type = (int)PotType.EnergyPotion; //---Energy 
                }
            }
        }

        public int Quality
        {
            get { return quality; }
            private set
            {
                quality = value;
                switch (quality)
                {
                    //If quality = 0 
                    case (int)PotQuality.Normal:
                        amount = NORMAL_QUALITY;
                        break;
                    //quality = 1 
                    case (int)PotQuality.Super:
                        amount = SUPER_QUALITY;
                        break;
                    //quality = 2; 
                    case (int)PotQuality.Mega:
                        amount = MEGA_QUALITY;
                        break;
                    default:
                        Console.WriteLine("Error: Not a valid potion! Setting it to a Normal potion...");
                        quality = (int)PotQuality.Normal;
                        amount = NORMAL_QUALITY;
                        break;
                }
            }
        }

        /***Constructor***/
        public Potion(int quality, int type)
        {

            this.quality = quality;
            this.type = type;
            Quality = quality;

        }
    }
}
