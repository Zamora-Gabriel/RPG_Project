using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{

    enum BattleState
    {
        PlayerTurn = 0,
        EnemyOneTurn,
        EnemyTwoTurn,
        EnemyThreeTurn,
        DeterminingState,
        End
    }


    class BattleManager
    {

        string[] bottomButtons = new string[] { "", "[ 1) Attack ]    [ 2) Abilites ]", "[ 3) Items  ]    [ 4) Run      ]" };
        string[] bottomAttacks;

        static int SPEED_TO_MOVE = 50;

        Player player;
        BasicEnemy[] enemy;
        Printer printer;

        int xpFromBattle;
        int goldFromBattle;

        BattleState current = BattleState.DeterminingState;

        bool inCombat = true;

        int[] CombatentsSpeed = new int[4];

        public BattleManager(Printer printer, Player player, BasicEnemy[] newenemy)
        {
            this.printer = printer;
            this.player = player;

            enemy = new BasicEnemy[newenemy.Length];
            for (int i = 0; i < newenemy.Length; i++)
            {
                this.enemy[i] = newenemy[i];
            }
        }


        public void BattleLoop()
        {
            while (current != BattleState.End)
            {
                switch (current)
                {
                    case BattleState.PlayerTurn:
                        //TODO have player make choice
                        printer.PrintSingle("It's your move!");
                        PlayerChoice();
                        current = BattleState.DeterminingState;
                        break;
                    case BattleState.EnemyOneTurn:
                        //Determins if enemy attacks and sends damage to player
                        player.RcvDamage(enemy[0].TakeAction(player));
                        Console.ReadLine();
                        current = BattleState.DeterminingState;
                        //TODO Print message that enemy attackes
                        break;
                    case BattleState.EnemyTwoTurn:
                        //Determins if enemy attacks and sends damage to player
                        player.RcvDamage(enemy[1].TakeAction(player));
                        Console.ReadLine();
                        current = BattleState.DeterminingState;
                        //TODO Print message that enemy attackes
                        break;
                    case BattleState.EnemyThreeTurn:
                        //Determins if enemy attacks and sends damage to player
                        player.RcvDamage(enemy[2].TakeAction(player));
                        Console.ReadLine();
                        current = BattleState.DeterminingState;
                        //TODO Print message that enemy attackes
                        break;
                    case BattleState.DeterminingState:
                        UpdateBoard();
                        SwitchState();
                        break;
                    case BattleState.End:
                        break;
                }
            }

        }


        void SwitchState()
        {
            //If all enemies or player is dead end combat
            if (player.Health <= 0 && enemy[0].Health <= 0)
            {
                current = BattleState.End;
            }

            //Determine next move by speed. 
            //Currently the player or enemy can move multiple times in a row if they have high enough speed.
            while (true)
            {
                if (CombatentsSpeed[0] >= SPEED_TO_MOVE)
                {
                    current = BattleState.PlayerTurn;
                    CombatentsSpeed[0] -= SPEED_TO_MOVE;
                    return;
                }

                for (int i = 0; i < enemy.Length; i++)
                {
                    if (CombatentsSpeed[i + 1] >= SPEED_TO_MOVE)
                    {
                        current = (BattleState)i + 1;
                        CombatentsSpeed[i + 1] -= SPEED_TO_MOVE;
                        return;
                    }
                }

                for (int i = 0; i < enemy.Length; i++)
                {
                    CombatentsSpeed[i + 1] += enemy[0].Speed;
                }
                //Increase cumulative speed
                CombatentsSpeed[0] += player.Speed;


            }
        }

        void UpdateBoard()
        {
            Console.Clear();
            string[][] centeredObjects = new string[enemy.Length][];

            //Update and pack enemies
            for (int i = 0; i < enemy.Length; i++)
            {
                centeredObjects[i] = printer.PrepareString(enemy[i].EnemyArt, enemy[i].Health, enemy[i].MaxHealth, enemy[i].Name);
            }
            //Print top data
            printer.PrintTopScreen(centeredObjects);

            //Update and pack player
            printer.PrintMiddleScreen(printer.PrepareString(player.art, player.Health, player.MaxHealth, player.Name));

            //Print bottom interface
            printer.PrintBottomScreen(bottomButtons);
        }

        void UpdateBoard(int choice)
        {
            Console.Clear();
            string[][] centeredObjects = new string[enemy.Length][];

            //Update and pack enemies
            for (int i = 0; i < enemy.Length; i++)
            {
                centeredObjects[i] = printer.PrepareString(enemy[i].EnemyArt, enemy[i].Health, enemy[i].MaxHealth, enemy[i].Name);
            }
            //Print top data
            printer.PrintTopScreen(centeredObjects);

            //Update and pack player
            printer.PrintMiddleScreen(printer.PrepareString(player.art, player.Health, player.MaxHealth, player.Name));

            switch (choice)
            {
                case 1:

                    bottomAttacks = new string[3];
                    bottomAttacks[0] = "";
                    for(int i = 0; i < enemy.Length; i++)
                    {
                        bottomAttacks[1] += string.Format(" [ {0}): Attack {1} ] ", i+1, enemy[i].Name);
                    }
                    bottomAttacks[2] = string.Format("[ {0}): Return to menu ]", enemy.Length+1);
                    printer.PrintBottomScreen(bottomAttacks);
                    break;

                case 2:
                    //TODO pull abilities list and print out options to use
                    break;
                case 3:
                    //TODO pull list of items in inventory and options to use
                    break;
                case 4:
                    //TODO Runaway
                    break;
            }
        }

        //Waits for player input and then attacks
        //Depending on the number of enemies on screen selections have different effects
        void ChooseAttack()
        {
            while (true)
            {
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    case 1:
                        //Check if enemy is dead and prevent attacking it again.
                        if (enemy[0].HasDied)
                        {
                            PlayerChoice(1);
                            return;
                        }
                        //Attack enemy
                        enemy[0].TakeDamage(player.AtkDamage(enemy[0]));
                        //if the enemy dies increase battle rewards
                        if (enemy[0].HasDied)
                        {
                            IncreaseBattleRewards(0);
                        }
                        return;

                    case 2:
                        if (enemy.Length <= 1)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            return;
                        }

                        enemy[1].TakeDamage(player.AtkDamage(enemy[1]));
                        if (enemy[1].HasDied)
                        {
                            IncreaseBattleRewards(1);
                        }

                        return;

                    case 3:
                        if (enemy.Length <= 2)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            return;
                        }

                        enemy[2].TakeDamage(player.AtkDamage(enemy[2]));
                        if (enemy[3].HasDied)
                        {
                            IncreaseBattleRewards(3);
                        }

                        return;
                    case 4:
                        if(enemy.Length <= 3)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            return;
                        }
                        break;
                }
            }
        }

        void PlayerChoice()
        {
            while (true)
            {
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    //Attack
                    case 1:
                        //Display enemies available to attack
                        UpdateBoard(playerChoice);

                        //Choose player to attack
                        ChooseAttack();
                        return;
                    //Abilitiy
                    case 2:
                        //TODO ADD ABILITIES
                        return;
                    //Items
                    case 3:
                        //TODO LET PLAYER USE ITEMS
                        return;
                    //Run
                    case 4:
                        //TODO RUNNING FROM FIGHTS
                        return;
                    //Catch for invalid input
                    default:
                        printer.PrintSingle("Please choose a valid selection");
                        break;
                }
            }
        }

        //Only to be called by internal functions. Used for resetting menus
        void PlayerChoice(int choice)
        {
            while (true)
            {
                switch (choice)
                {
                    //Attack
                    case 1:
                        //Display enemies available to attack
                        UpdateBoard(choice);
                        //Choose player to attack and notify player that their choice is dead
                        printer.PrintSingle("Choose a living target!");
                        ChooseAttack();
                        return;
                    //Abilitiy
                    case 2:
                        //TODO ADD ABILITIES
                        return;
                    //Items
                    case 3:
                        //TODO LET PLAYER USE ITEMS
                        return;
                    //Run
                    case 4:
                        //TODO RUNNING FROM FIGHTS
                        return;
                    //Catch for invalid input
                    default:
                        printer.PrintSingle("Please choose a valid selection");
                        break;
                }
            }
        }


        int ReturnChoice()
        {
            while (true)
            {
                
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                    printer.PrintSingle("Invalid selection, please use a number!", true, true);
                }
            }
        }

        void IncreaseBattleRewards(int enemyPos)
        {
            xpFromBattle += enemy[enemyPos].ExpValue;
            goldFromBattle += enemy[enemyPos].MoneyValue;
        }
    }
    }
