using CSE3902_Game_Sprint0.Classes.SpriteFactories;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902_Game_Sprint0.Classes.Items
{
    public class Fairy : IItem, ICollisionEntity
    {
        private readonly ItemUtility util = new ItemUtility();
        private ZeldaGame game { get; set; }
        private ISprite itemSprite { get; set; }
        private ItemSpriteFactory itemFactory { get; set; }
        public Rectangle hitbox = new Rectangle(0, 0, 0, 0);
        public Vector2 position;
        public float spriteScalar { get; set; }
        public Vector2 velocity = new Vector2(0, 0);
        private int timer { get; set; } = 0;
        public Fairy(ZeldaGame game, ItemSpriteFactory itemFactory, Vector2 location)
        {

            this.game = game;
            this.spriteScalar = game.util.spriteScalar;
            this.position = location;
            this.itemFactory = itemFactory;
            this.itemSprite = itemFactory.Fairy();
            game.collisionManager.collisionEntities.Add(this, CollisionRectangle());
        }
        private void ChangeDirection()
        {
            var random = new Random();

            switch (random.Next(util.RANDOM_DIRECTIONS))
            {
                case 0:
                    velocity = new Vector2(0, -1);
                    break;
                case 1:
                    velocity = new Vector2(1, -1);
                    break;
                case 2:
                    velocity = new Vector2(1, 0);
                    break;
                case 3:
                    velocity = new Vector2(1, 1);
                    break;
                case 4:
                    velocity = new Vector2(0, 1);
                    break;
                case 5:
                    velocity = new Vector2(-1, 1);
                    break;
                case 6:
                    velocity = new Vector2(-1, 0);
                    break;
                case 7:
                    velocity = new Vector2(-1, -1);
                    break;
                default:
                    break;
            }
            timer = util.PRESET_TIMER;
        }
        public Rectangle CollisionRectangle()
        {
            return hitbox;
        }
        public void Update()
        {
            itemSprite.Update();
            if (timer <= 0)
            {
                ChangeDirection();
            }
            else
            {
                timer--;
            }
            position.X = position.X + velocity.X;
            position.Y = position.Y + velocity.Y;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            hitbox.Width = (int)(16 * spriteScalar);
            hitbox.Height = (int)(16 * spriteScalar);
            game.collisionManager.collisionEntities[this] = hitbox;
        }
        public void Draw()
        {
            itemSprite.Draw(position);
        }
    }
}
