using CSE3902_Game_Sprint0.Classes._21._2._13;
using CSE3902_Game_Sprint0.Classes.Enemy.Gel;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;

namespace CSE3902_Game_Sprint0.Classes.Enemy
{
    public class EnemySlime : IEnemy, ICollisionEntity
    {
        public ZeldaGame game { get; set; }
        private GelStateMachine myState { get; set; }
        public GelSpriteFactory enemySpriteFactory { get; set; }
        public ISprite mySprite { get; set; }
        public Vector2 drawLocation;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 spriteSize = new Vector2(8, 16);
        public Rectangle collisionRectangle = new Rectangle(0, 0, 0, 0);
        private float spriteScalar { get; set; }
        private static int HITBOX_OFFSET { get; set; } = 6;
        public int health { get; set; } = 1;
        private int hurtTimer { get; set; } = 0;

        public EnemySlime(ZeldaGame game, Vector2 spawnLocation)
        {
            this.game = game;
            this.spriteScalar = game.util.spriteScalar;
            this.enemySpriteFactory = new GelSpriteFactory(game);
            this.mySprite = this.enemySpriteFactory.GelIdle();
            drawLocation = spawnLocation;
            myState = new GelStateMachine(this);
            game.collisionManager.collisionEntities.Add(this, CollisionRectangle());
            health = health * game.util.difficultyMult;
        }
        public void TakeDamage(int damage)
        {
            if (hurtTimer <= 0)
            {
                hurtTimer = 30;
                this.health = this.health - damage;
                game.sounds["enemyHit"].CreateInstance().Play();
            }
        }
        public Rectangle CollisionRectangle()
        {
            return collisionRectangle;
        }

        public void Update()
        {
            myState.Update();
            mySprite.Update();

            drawLocation.X = drawLocation.X + velocity.X;
            drawLocation.Y = drawLocation.Y + velocity.Y;
            collisionRectangle.X = (int)drawLocation.X + HITBOX_OFFSET;
            collisionRectangle.Y = (int)drawLocation.Y + HITBOX_OFFSET;
            collisionRectangle.Width = (int)(spriteSize.X * spriteScalar) - 6 * HITBOX_OFFSET;
            collisionRectangle.Height = (int)(spriteSize.Y * spriteScalar) - 3 * HITBOX_OFFSET;
            if (myState.currentState != GelStateMachine.CurrentState.dying)
            { game.collisionManager.collisionEntities[this] = collisionRectangle; }
        }

        public void Draw()
        {
            mySprite.Draw(drawLocation);
        }
    }
}
