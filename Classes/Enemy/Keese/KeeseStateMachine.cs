using CSE3902_Game_Sprint0.Classes.Enemy.Keese.keeseScripts;
using CSE3902_Game_Sprint0.Classes.Items;
using CSE3902_Game_Sprint0.Interfaces;
using System;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Keese
{
    public class KeeseStateMachine : IEnemyStateMachine
    {
        private ZeldaGame game { get; set; }
        private EnemyKeese keese { get; set; }
        private KeeseSpriteFactory enemySpriteFactory { get; set; }

        public enum Direction { north, northEast, east, southEast, south, southWest, west, northWest };
        public Direction direction { get; set; } = Direction.north;
        private int directionNumber = 0;
        bool moving { get; set; } = false;
        bool landing { get; set; } = false;
        bool takeOff { get; set; } = false;
        bool spawning { get; set; } = true;
        bool spawned { get; set; } = false;
        private int timer { get; set; } = 3;
        private int deathTimer { get; set; } = 30;
        private int directionTimer { get; set; } = 0;
        public enum CurrentState { none, idle, flyingNorth, flyingNorthEast, flyingEast, flyingSouthEast, flyingSouth, flyingSouthWest, flyingWest, flyingNorthWest, landingNorth, landingNorthEast, landingEast, landingSouthEast, landingSouth, landingSouthWest, landingWest, landingNorthWest, spawning, dying };
        public CurrentState currentState { get; set; } = CurrentState.none;

        public KeeseStateMachine(EnemyKeese keese)
        {
            this.game = keese.game;
            this.keese = keese;
            enemySpriteFactory = new KeeseSpriteFactory(game);
        }
        private Direction ChangeDirection(ref int directionNumber)
        {
            var random = new Random();

            switch (random.Next(3))
            {
                case 0:
                    directionNumber = directionNumber - 1;
                    break;
                case 1:
                    break;
                case 2:
                    directionNumber = directionNumber + 1;
                    break;
                default:
                    break;
            }

            if (directionNumber > 7)
            {
                directionNumber = 0;
            }
            else if (directionNumber < 0)
            {
                directionNumber = 7;
            }

            switch (directionNumber)
            {
                case 0:
                    return Direction.north;
                case 1:
                    return Direction.northEast;
                case 2:
                    return Direction.east;
                case 3:
                    return Direction.southEast;
                case 4:
                    return Direction.south;
                case 5:
                    return Direction.southWest;
                case 6:
                    return Direction.west;
                case 7:
                    return Direction.northWest;
                default:
                    return Direction.north;
            }
        }

        public void Spawning()
        {
            timer = 90;
            spawning = false;
            new KeeseSpawning(keese, enemySpriteFactory, this).Execute();
        }
        public void Dying()
        {
            new KeeseDying(keese, enemySpriteFactory, this).Execute();
        }

        public void Idle()
        {
            new KeeseIdle(keese, enemySpriteFactory, this).Execute();
        }

        public void Flying()
        {
            new KeeseFlying(keese, enemySpriteFactory, this).Execute();

        }

        public void Landing()
        {
            new KeeseLanding(keese, enemySpriteFactory, this, landing, takeOff).Execute();
        }

        public void Update()
        {
            if (timer > 0)
            {
                timer--;
            }
            if (directionTimer <= 0)
            {
                directionTimer = 30;
                direction = ChangeDirection(ref directionNumber);
            }
            else
            {
                directionTimer--;
            }

            if (timer <= 0)
            {
                if (!spawned)
                {
                    spawned = true;
                }

                if (!moving && !landing && !takeOff)
                {
                    timer = 120;
                    moving = true;
                    takeOff = true;
                }
                else if (moving && takeOff)
                {
                    timer = 180;
                    takeOff = false;
                }
                else if (moving && !landing && !takeOff)
                {
                    timer = 120;
                    landing = true;
                }
                else if (moving && landing)
                {
                    timer = 180;
                    moving = false;
                    landing = false;
                }
            }

            if (spawning)
            {
                Spawning();
            }
            else if (spawned)
            {

                if (keese.health <= 0)
                {
                    Dying();
                    deathTimer--;
                    if (deathTimer == 0)
                    {
                        new DropMinorItem(keese.game, keese.drawLocation).Execute();
                        keese.game.collisionManager.collisionEntities.Remove(keese);
                        keese.game.currentRoom.removeEnemy(keese);
                    }
                }
                else if (moving)
                {
                    if (landing || takeOff)
                    {
                        Landing();
                    }
                    else
                    {
                        Flying();
                    }
                }
                else
                {
                    Idle();
                }
            }
        }
    }
}
