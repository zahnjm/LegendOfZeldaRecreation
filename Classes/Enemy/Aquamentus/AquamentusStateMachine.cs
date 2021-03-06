using CSE3902_Game_Sprint0.Classes.Enemy.Aquamentus.AquamentusScripts;
using CSE3902_Game_Sprint0.Classes.Items;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Aquamentus
{
    public class AquamentusStateMachine : IEnemyStateMachine
    {
        private ZeldaGame game { get; set; }
        private EnemyAquamentus aquamentus { get; set; }
        private AquamentusSpriteFactory enemySpriteFactory { get; set; }

        public enum Direction { right, left };
        public Direction direction = Direction.right;
        bool spawning = true;
        bool roaring = false;
        private int timer = 90;
        private int deathTimer = 30;
        public enum CurrentState { none, movingLeft, movingRight, roaringLeft, roaringRight, spawning, dying, damaged };
        public CurrentState currentState = CurrentState.none;
        public Rectangle collisionRectangle = new Rectangle(0, 0, 0, 0);
        public float spriteScalar;

        public AquamentusStateMachine(EnemyAquamentus aquamentus)
        {
            game = aquamentus.game;
            this.aquamentus = aquamentus;
            enemySpriteFactory = new AquamentusSpriteFactory(game);
        }
        public Rectangle CollisionRectangle()
        {
            return collisionRectangle;
        }

        public void Spawning()
        {
            timer = 90;
            spawning = false;
            new AquamentusSpawning(aquamentus, enemySpriteFactory, this).Execute();
        }
        public void Dying()
        {
            new AquamentusDying(aquamentus, enemySpriteFactory, this).Execute();
        }
        public void Moving()
        {
            if (roaring)
            {
                if (timer <= 0)
                {
                    new AquamentusRoaring(aquamentus, enemySpriteFactory, this).Execute();
                }
            }
            else
            {
                if (timer <= 0)
                {
                    new AquamentusMoving(aquamentus, enemySpriteFactory, this).Execute();
                }
            }
        }

        public void Update()
        {
            if (!spawning)
            {
                if (timer <= 0)
                {
                    var random = new Random();
                    int randomRoar = random.Next(2);

                    if (randomRoar > 0)
                    {
                        direction = Direction.left;
                    }
                    else
                    {
                        direction = Direction.right;
                    }

                    randomRoar = random.Next(3);

                    if (randomRoar > 0)
                    {
                        roaring = false;
                        timer = 90;
                    }
                    else
                    {
                        roaring = true;
                        timer = 45;
                    }
                }
                else
                {
                    timer--;
                }
            }
            else
            {
                timer--;
            }

            if (aquamentus.health <= 0)
            {
                Dying();
                deathTimer--;
                if (deathTimer == 0)
                {
                    new DropMinorItem(aquamentus.game, aquamentus.drawLocation).Execute();
                    aquamentus.game.currentRoom.removeEnemy(aquamentus);
                }
            }
            else if (spawning)
            {
                Spawning();
            }
            else
            {
                Moving();
            }
        }
    }
}
