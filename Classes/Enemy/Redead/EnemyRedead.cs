﻿using CSE3902_Game_Sprint0.Classes._21._2._13;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Redead
{
    public class EnemyRedead : IEnemy, ICollisionEntity
    {
        public ZeldaGame game;
        public RedeadStateMachine myState;
        public RedeadSpriteFactory enemySpriteFactory;
        public ISprite mySprite;
        public Vector2 drawLocation;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 spriteSize = new Vector2(16, 16);
        public Rectangle collisionRectangle = new Rectangle(0, 0, 0, 0);
        private float spriteScalar;
        private static int HITBOX_OFFSET = 6;
        public int health = 2;
        private int hurtTimer = 0;

        public EnemyRedead(ZeldaGame game, Vector2 spawnLocation)
        {
            this.game = game;
            this.spriteScalar = game.util.spriteScalar;
            this.enemySpriteFactory = new RedeadSpriteFactory(game);
            this.mySprite = this.enemySpriteFactory.RedeadIdle();
            drawLocation = spawnLocation;
            myState = new RedeadStateMachine(this);
            game.collisionManager.collisionEntities.Add(this, this.CollisionRectangle());
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
            if (hurtTimer > 0)
            {
                hurtTimer--;
            }
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

            if (myState.idle)
            {
                collisionRectangle.X = (int)drawLocation.X - (int)(spriteSize.X * spriteScalar);
                collisionRectangle.Y = (int)drawLocation.Y - (int)(spriteSize.Y * spriteScalar);
                collisionRectangle.Width = ((int)(spriteSize.X * spriteScalar) - 2 * HITBOX_OFFSET) * 3;
                collisionRectangle.Height = ((int)(spriteSize.Y * spriteScalar) - 2 * HITBOX_OFFSET) * 3;
            }
            else
            {
                collisionRectangle.X = (int)drawLocation.X + HITBOX_OFFSET;
                collisionRectangle.Y = (int)drawLocation.Y + HITBOX_OFFSET;
                collisionRectangle.Width = (int)(spriteSize.X * spriteScalar) - 2 * HITBOX_OFFSET;
                collisionRectangle.Height = (int)(spriteSize.Y * spriteScalar) - 2 * HITBOX_OFFSET;
            }

            if (myState.currentState != RedeadStateMachine.CurrentState.dying)
            {
                game.collisionManager.collisionEntities[this] = collisionRectangle;
            }
        }
        public void Draw()
        {
            mySprite.Draw(drawLocation);
        }
    }
}
