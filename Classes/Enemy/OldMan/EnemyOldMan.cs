﻿using CSE3902_Game_Sprint0.Classes._21._2._13;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Enemy.OldMan
{
    public class EnemyOldMan : IEnemy, ICollisionEntity
    {
        public ZeldaGame game;
        private OldManStateMachine myState;
        public OldManSpriteFactory enemySpriteFactory;
        public ISprite mySprite;
        public Vector2 drawLocation;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 spriteSize = new Vector2(16, 16);
        public float spriteScalar;
        public Rectangle collisionRectangle = new Rectangle(0, 0, 0, 0);

        public EnemyOldMan(ZeldaGame game, Vector2 spawnLocation)
        {
            this.game = game;
            this.enemySpriteFactory = new OldManSpriteFactory(game);
            drawLocation = spawnLocation;
            myState = new OldManStateMachine(this);
            this.spriteScalar = game.spriteScalar;
            game.collisionManager.collisionEntities.Add(this, CollisionRectangle());
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

            if (drawLocation.X >= game.GraphicsDevice.Viewport.Bounds.Width && velocity.X > 0)
            {
                drawLocation.X = 0 - spriteSize.X;
            }
            else if (drawLocation.X + spriteSize.X <= 0 && velocity.X < 0)
            {
                drawLocation.X = game.GraphicsDevice.Viewport.Bounds.Width;
            }

            if (drawLocation.Y >= game.GraphicsDevice.Viewport.Bounds.Height && velocity.Y > 0)
            {
                drawLocation.Y = 0 - spriteSize.Y;
            }
            else if (drawLocation.Y + spriteSize.Y <= 0 && velocity.Y < 0)
            {
                drawLocation.Y = game.GraphicsDevice.Viewport.Bounds.Height;
            }

            collisionRectangle.X = (int)drawLocation.X;
            collisionRectangle.Y = (int)drawLocation.Y;
            collisionRectangle.Width = (int)(spriteSize.X * spriteScalar);
            collisionRectangle.Height = (int)(spriteSize.Y * spriteScalar);

            game.collisionManager.collisionEntities[this] = collisionRectangle;
        }

        public void Draw()
        {
            mySprite.Draw(drawLocation);
        }
    }
}
