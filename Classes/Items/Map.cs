using CSE3902_Game_Sprint0.Classes.SpriteFactories;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;

namespace CSE3902_Game_Sprint0.Classes.Items
{
    public class Map : IItem, ICollisionEntity
    {
        private ZeldaGame game { get; set; }
        private ISprite itemSprite { get; set; }
        private ItemSpriteFactory itemFactory { get; set; }
        public Vector2 spriteSize = new Vector2(10, 10);
        public Rectangle hitbox = new Rectangle(0, 0, 0, 0);
        public Vector2 position;
        public float spriteScalar { get; set; }
        public Map(ZeldaGame game, ItemSpriteFactory itemFactory, Vector2 location)
        {
            this.game = game;
            this.spriteScalar = game.util.spriteScalar;
            this.position = location;
            this.itemFactory = itemFactory;
            this.itemSprite = itemFactory.Map();
            game.collisionManager.collisionEntities.Add(this, hitbox);
        }
        public void Update()
        {
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            hitbox.Width = (int)(spriteSize.X * spriteScalar);
            hitbox.Height = (int)(spriteSize.Y * spriteScalar);

            game.collisionManager.collisionEntities[this] = hitbox;
        }

        public void Draw()
        {
            itemSprite.Draw(position);
        }

        public Rectangle CollisionRectangle()
        {
            return hitbox;
        }
    }
}
