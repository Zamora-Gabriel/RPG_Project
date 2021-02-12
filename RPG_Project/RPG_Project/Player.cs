
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{

    enum Abilities
    {
        /// <summary>
        /// Temporary skills, list can be reduced or added
        /// </summary>
        Heal = 1, //--- heals 5 hp multiplied by x which increases each 5 levels starting at 1
        Shatter, //--- Reduces 1/3 of enemy's defense for that attack only  
        AvengerSoul, //--- Adds half of player's current accrued damage to the attack
        Devour, //--- Takes 50% damage dealt to opponent as healing
        WindGod //--- Five attacks at 50% of damage
    }

    //Maybe: Energy/Skills
    class Player
    {
        /***Variables***/

        //TMP REMOVE ONCE PLAYER ART IS FINALIZED
        public string[] art = new string[] { "", "HP: {0}/{1}", "PP:     ", "o", "/|\\", "|", "/ \\", "{2}" };

        string name;
        int exp;
        int level;
        int money;
        string equipedWeapon;
        Inventory invent;

        Printer printer;
        //Stats
        int maxHealth;
        int health;
        int energy;
        int attack;
        int defense;
        int speed;

        //Limits
        int healthLimit;
        int energyLimit;


        /***Properties***/

        public string Name
        {
            get { return name; }
            private set
            {
                name = value;
            }
        }

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

                //Check if energy surpasses limits
                if (health > healthLimit)
                {
                    health = healthLimit;
                }

                //Check if player dies
                if (health <= 0)
                {
                    PlayerDies();
                }
            }
        }

        public int Energy
        {
            get { return energy; }
            set
            {
                energy = value;

                //Check if energy surpasses limits
                if (energy > energyLimit)
                {
                    energy = energyLimit;
                }
            }
        }

        public int Attack
        {
            get { return attack; }

            //Can't modify attack outside of the class
            private set
            {
                attack = value;
            }
        }

        public int Defense
        {
            get { return defense; }

            //Can't modify attack outside of the class
            private set
            {
                defense = value;
            }
        }

        public int Speed
        {
            get { return speed; }


            //Can't modify speed outside of the class
            private set
            {
                speed = value;
            }
        }

        public int Exp
        {
            get { return exp; }

            set
            {
                exp = value;

                if (exp >= 100)
                {
                    //save residual experience and level up
                    exp = exp - 100;
                    LevelUp();
                }
            }
        }

        public int Level
        {
            get { return level; }
            private set
            {
                level = value;
            }
        }

        public int Money
        {
            get { return money; }
            private set
            {
                money = value;
            }
        }

        public string EquipedWeapon
        {
            get { return equipedWeapon; }
            private set
            {
                equipedWeapon = value;
            }
        }

        /***Constructors***/

        public Player(string name1)
        {
            //Provisional values
            Name = name1;

            healthLimit = 15;
            energyLimit = 15;

            Health = 15;
            Energy = 15;
            Attack = 5;
            Speed = 5;
            Defense = 5;
            Exp = 0;
            Level = 0;
            EquipedWeapon = "";
            invent = new Inventory();

            printer = new Printer();
        }

        /***Methods***/

        private void PlayerDies()
        {
            Console.WriteLine("{0} has been defeated", Name);
        }

        private void LevelUp()
        {
            Level++;

            healthLimit += 2;
            energyLimit += 2;
            Health = healthLimit;
            Energy = energyLimit;
            Attack++;
            Defense++;
            Speed++;
            Console.WriteLine("Congratulations {0}! Now you're level {1}", Name, Level);
        }

        public int AtkDamage(Enemy enemy)
        {
            int outgoingDmg = Attack;

            //Calculate damage to enemy
            outgoingDmg -= enemy.Defense;
            if (outgoingDmg < 0)
            {
                outgoingDmg = 0;
            }
            //return damage;
            return outgoingDmg;
        }

        public void RcvDamage(int damage)
        {
            Health -= damage;
        }

        public void EquipWeapon(int weapNum)
        {
            //Case the user already has an equiped weapon
            if (EquipedWeapon != "")
            {
                Console.WriteLine("Wait, you already have a weapon equiped!");
                return;
            }
            //check if user has that weapon on inventory
            try
            {
                Weapon weap = invent.CheckWeapon(weapNum);
                if (weap == null)
                {
                    //Throw exception when no weapon is found
                    throw new Exception();
                }
                //Sum weapons' bonuses
                Attack += weap.AtkBonus;
                Defense += weap.DefBonus;
                Speed += weap.SpdBonus;
                EquipedWeapon = weap.Name;
                Console.WriteLine("Succesfully equiped weapon: {0}", EquipedWeapon);
                return;
            }
            catch
            {
                Console.WriteLine("It seems you don't have that weapon...");
                return;
            }
        }

        //TODO: Make it public if user can fight barehanded
        private bool UnequipWeapon(int weapNum)
        {
            if (EquipedWeapon == "")
            {
                Console.WriteLine("Wait... You don't have an equiped weapon!");
                return false;
            }

            try
            {
                Weapon weap = invent.CheckWeaponByName(EquipedWeapon);

                if (weap == null)
                {
                    //Throw exception when no weapon is found
                    throw new Exception();
                }

                //Substract weapons' bonuses
                Attack -= weap.AtkBonus;
                Defense -= weap.DefBonus;
                Speed -= weap.SpdBonus;

                //Restart equiped weapon's id to discriminate
                EquipedWeapon = "";
                Console.WriteLine("Succesfully unequiped weapon: {0}", weap.Name);
            }
            catch
            {
                Console.WriteLine("Wait... You didn't had that weapon to start with!");
                return false;
            }
            return true;
        }

        public void ChangeWeapons(int weap1, int weap2)
        {
            //Unequipment succeed
            if (UnequipWeapon(weap1))
            {
                EquipWeapon(weap2);
                return;
            }
        }

        public void AddWeaponToInvent(Weapon weap)
        {
            //check if user has that weapon on inventory
            if (invent.AddWeapon(weap))
            {
                Console.WriteLine("Succesfully added weapon: {0}", weap.Name);
            }
        }

        public void AddPotionToInvent(Potion pot)
        {
            //check if user has that potion on inventory
            if (invent.AddPotion(pot))
            {
                Console.WriteLine("Succesfully added potion!");
            }
        }

        public void PrintStats()
        {
            Console.WriteLine("{0} Stats", Name);
            Console.WriteLine("Level: {0}", Level);
            Console.WriteLine("Exp: {0}", Exp);
            Console.WriteLine("Attack: {0}", Attack);
            Console.WriteLine("Defense: {0}", Defense);
            Console.WriteLine("Speed: {0}", Speed);
            Console.WriteLine("Money: {0}", Money);
        }

        public void PrintInvent()
        {
            invent.PrintWeapons();
            invent.PrintPotions();
        }

        //Checks inventory for potion count
        public int[,] ReturnPotions()
        {
            return (invent.ReturnPotionCount());
        }

        /// <summary>
        /// TODO: Make player correctly use potions
        /// Not to sure why but it's not currently finding potions the player has when called externally
        /// Possibly needs to pass more than just the potNum and include a second int or a bool for potion type
        /// </summary>

        
        public void DrinkPotion(int option)
        {
            //check if user has that potion on inventory
            try
            {
                Potion pot = invent.CheckPotion(option);
                if (pot == null)
                {
                    //Throw exception when no matching potion is found
                    throw new Exception();
                }
                //add bonus and remove potion from list
                if (invent.RemovePotion(pot))
                {
                    if (pot.Type == 0)
                    {
                        //Regain health
                        Health = pot.amount;
                    }
                    else
                    {
                        Energy = pot.amount;
                    }
                    printer.PrintSingle("Succesfully used potion");
                    Console.ReadLine();
                    return;
                }
                Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("It seems you don't have that type of... potion...");
                return;
            }
        }

        /***Abilities***/

        public string[,] ReturnUnlockedAbilities()
        {
            int row = 3;
            int column = 2;
            string[,] abilityList = new string[column, row];
            int multiplier = 1;
            var abilityName = (Abilities)1;

            for(int i = 0; i <= column-1; i++)
            {
                for (int j = 0; j <= row-1; j++)
                {
                    //check level for locked or unlocked abilities
                    if (Level >= (1 * (multiplier)))
                    {
                        abilityList[i, j] = abilityName.ToString();
                    }
                    else
                    {
                        abilityList[i, j] = "Locked";
                    }
                    multiplier++;
                }
            }

            return abilityList;
        }

        public int UseAbility(int option, Enemy enemy)
        {
            int outgoingDmg = Attack;

            switch (option)
            {
                /*
                 * Heal = 0, //--- heals 5 hp multiplied by x which increases each 5 levels starting at 1
                 * Shatter, //--- Reduces 1/3 of enemy's defense for that attack only  
                 * AvengerSoul, //--- Adds half of player's current accrued damage to the attack
                 * Devour, //--- Takes 50% damage dealt to opponent as healing
                 * WindGod //--- Five attacks at 50% of damage Translation = ((outgoingDmg/2)*5)
                 */

                case 1:
                    //Heal
                    int multiplier = Level/5;
                    if (multiplier <= 0)
                    {
                        multiplier = 1;
                    }
                    Health += 5 * multiplier;
                    outgoingDmg = 0;
                    break;
                case 2:
                    //Shatter
                    outgoingDmg -= enemy.Defense;
                    outgoingDmg -= (enemy.Defense*2) / 3;
                    if (outgoingDmg < 0)
                    {
                        outgoingDmg = 0;
                        break;
                    }
                    break;
                case 3:
                    //AvengerSoul
                    int accruedDmg = maxHealth - Health;
                    outgoingDmg -= enemy.Defense - accruedDmg;
                    if (outgoingDmg < 0)
                    {
                        outgoingDmg = 0;
                        break;
                    }
                    break;
                case 4:
                    //Devour
                    outgoingDmg -= enemy.Defense;
                    if (outgoingDmg < 0)
                    {
                        outgoingDmg = 0;
                        break;
                    }
                    Health += outgoingDmg / 2;
                    break;
                case 5:
                    //WindGod
                    outgoingDmg -= enemy.Defense;
                    if (outgoingDmg < 0)
                    {
                        outgoingDmg = 0;
                        break;
                    }
                    outgoingDmg += (outgoingDmg/ 2) * 5;
                    break;
                default:
                    Console.WriteLine("Error: Not a valid option");
                    break;
            }
            return outgoingDmg;
        }
    }
}
