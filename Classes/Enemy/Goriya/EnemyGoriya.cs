﻿using CSE3902_Game_Sprint0.Classes.Enemy.Goriya;
using CSE3902_Game_Sprint0.Classes.Projectiles;
using CSE3902_Game_Sprint0.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes._21._2._13
{
    public class EnemyGoriya : IEnemy, ICollisionEntity
    {
        //When defeated, can drop a heart, rupee, 5rupee or a clock
        //Pathing is random, no sense of direction
        //Method of attack is melee, bumping into the player

        public ZeldaGame game;
        private GoriyaStateMachine myState;
        public GoriyaSpriteFactory spriteFactory { get; protected set; }
        public Vector2 drawLocation;
        public Vector2 velocity;
        public Vector2 spriteSize = new Vector2(16, 16);
        private GoriyaBoomerang boomerang;
        public ISprite mySprite;
        public Rectangle collisionRectangle = new Rectangle(0, 0, 0, 0);
        private float spriteScalar;
        private static int HITBOX_OFFSET = 6;
        public int health = 2;
        private int hurtTimer = 0;

        public EnemyGoriya(ZeldaGame game, Vector2 spawnLocation)
        {
            this.game = game;
            this.spriteFactory = new GoriyaSpriteFactory(game);
            this.mySprite = this.spriteFactory.GoriyaIdleDown();
            drawLocation = spawnLocation;
            myState = new GoriyaStateMachine(this);
            game.collisionManager.collisionEntities.Add(this, collisionRectangle);
            boomerang = new GoriyaBoomerang(game, this, myState);
            this.spriteScalar = game.util.spriteScalar;
        }
        public void TakeDamage(int damage)
        {
            if (hurtTimer <= 0)
            {
                hurtTimer = 60;
                this.health = this.health - damage;
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
            if (!myState.moving)
            {
                boomerang.Update();
            }
            //Update the position of Link here
            drawLocation = drawLocation + velocity;

            if (drawLocation.X >= game.GraphicsDevice.Viewport.Bounds.Width && velocity.X > 0)
            {
                drawLocation = new Vector2(0 - spriteSize.X, drawLocation.Y);
            }
            else if (drawLocation.X + spriteSize.X <= 0 && velocity.X < 0)
            {
                drawLocation = new Vector2(game.GraphicsDevice.Viewport.Bounds.Width, drawLocation.Y);
            }

            if (drawLocation.Y >= game.GraphicsDevice.Viewport.Bounds.Height && velocity.Y > 0)
            {
                drawLocation = new Vector2(drawLocation.X, 0 - spriteSize.Y);
            }
            else if (drawLocation.Y + spriteSize.Y <= 0 && velocity.Y < 0)
            {
                drawLocation = new Vector2(drawLocation.X, game.GraphicsDevice.Viewport.Bounds.Height);
            }
            collisionRectangle.X = (int)drawLocation.X + HITBOX_OFFSET;
            collisionRectangle.Y = (int)drawLocation.Y + HITBOX_OFFSET;
            collisionRectangle.Width = (int)(spriteSize.X * spriteScalar) - 2 * HITBOX_OFFSET;
            collisionRectangle.Height = (int)(spriteSize.Y * spriteScalar) - 2 * HITBOX_OFFSET;

            game.collisionManager.collisionEntities[this] = collisionRectangle;
        }
        public void Draw()
        {
            mySprite.Draw(drawLocation);
            if (!myState.moving)
            {
                boomerang.Draw();
            }
        }
    }
}
