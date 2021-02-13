using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    enum AiPersonality
    {
        Neutral,
        Defensive,
        Aggresive,
        Supportive //Possibly allows the enemy to heal allies?
    }

    class BasicEnemy : Enemy
    {

        string[] enemyArt = new string[] { "HP: {0}\\{1}", "        ", "   o~\\  ", " |_-__\\", "        ", "{2}" };


        AiPersonality aiType = AiPersonality.Neutral;

        //TODO move printer to the base enemy class
        Printer printer = new Printer();

        Random random = new Random();
        

        //Constructor 
        public BasicEnemy(string name, int maxHealth, int attack, int defense, int speed, int expValue, int moneyValue) : base(name, maxHealth, attack, defense, speed, expValue, moneyValue)
        {
            
            this.printer = printer;
        }

        /*Getters and Setters*/

        public string[] EnemyArt
        {
            get { return enemyArt; }
            private set
            {
                enemyArt = value;
            }
        }

        void HealOther(int healAmmount)
        {

        }

        public int TakeAction(Player player)
        {
            int choice = AiChoice();
            switch (aiType)
            {
                case AiPersonality.Neutral:
                    if (choice <= 50)
                    {
                        printer.PrintSingle("{0}, attacked you for {1} damage!!", true, true, Name, AttackPlayer(player));
                        return AttackPlayer(player);
                    }
                    printer.PrintSingle("{0}, is trying to block your next attack!", true, true, Name);
                    Block();
                    return 0;

                //TODO impliment other AI types
                //case AiPersonality.Defensive:
                //    break;
                //case AiPersonality.Aggresive:
                //    break;
                //case AiPersonality.Supportive:
                //    break;
            }
            return 0;
        }

        int AiChoice()
        {
            return random.Next(100);
        }

    }
}
