﻿using CSE3902_Game_Sprint0.Classes._21._2._13;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CSE3902_Game_Sprint0.Classes.Projectiles
{
    public class GoriyaBoomerangStatemachine
    {
        public GoriyaBoomerang boomerang { get; set; }
        public EnemySpriteFactory spriteFactory { get; set; }
        public enum Direction { right, up, left, down, NE, SE, SW, NW, none };
        public Direction direction = Direction.down;
        public enum CurrentState { movingUp, movingDown, movingLeft, movingRight, movingNE, movingSE, movingSW, movingNW, none };
        public CurrentState currentState;
        public Direction returnDirection = Direction.none;
        private const int RANGE = 175;
        private const int RETURN_WINDOW = 30;
        private const int DESPAWN_DISTANCE = 5;
        private const float BASE_SPEED = (float)1.5, PIVOT_SPEED = (float)1.0;
        public Boolean returning = false, newItem = true, brake = false;
        private GoriyaBoomerangDirectionalCalulation directionalCalc;
        private GoriyaBoomerangMovementCalculation movementCalc;

        public GoriyaBoomerangStatemachine(GoriyaBoomerang boomerang)
        {
            this.boomerang = boomerang;
            this.spriteFactory = boomerang.spriteFactory;
            this.directionalCalc = new GoriyaBoomerangDirectionalCalulation(this);
            this.movementCalc = new GoriyaBoomerangMovementCalculation(this);
        }
        public void Outward()
        {
            switch (direction)
            {
                case Direction.down:
                    boomerang.trajectory = new Vector2(0, BASE_SPEED);
                    spriteFactory.GoriyaBoomerangAttack(boomerang);
                    break;
                case Direction.up:
                    boomerang.trajectory = new Vector2(0, -BASE_SPEED);
                    spriteFactory.GoriyaBoomerangAttack(boomerang);
                    break;
                case Direction.right:
                    boomerang.trajectory = new Vector2(BASE_SPEED, 0);
                    spriteFactory.GoriyaBoomerangAttack(boomerang);
                    break;
                case Direction.left:
                    boomerang.trajectory = new Vector2(-BASE_SPEED, 0);
                    spriteFactory.GoriyaBoomerangAttack(boomerang);
                    break;
                default:
                    break;
            }
        }
        public void OutwardReturnWindow()
        {
            switch (direction)
            {
                case Direction.down:
                    if (currentState != CurrentState.movingDown)
                    {
                        currentState = CurrentState.movingDown;
                        boomerang.trajectory = new Vector2(0, PIVOT_SPEED);
                        spriteFactory.GoriyaBoomerangAttack(boomerang);
                    }
                    break;
                case Direction.up:
                    if (currentState != CurrentState.movingUp)
                    {
                        currentState = CurrentState.movingUp;
                        boomerang.trajectory = new Vector2(0, -PIVOT_SPEED);
                        spriteFactory.GoriyaBoomerangAttack(boomerang);
                    }
                    break;
                case Direction.right:
                    if (currentState != CurrentState.movingRight)
                    {
                        currentState = CurrentState.movingRight;
                        boomerang.trajectory = new Vector2(PIVOT_SPEED, 0);
                        spriteFactory.GoriyaBoomerangAttack(boomerang);
                    }
                    break;
                case Direction.left:
                    if (currentState != CurrentState.movingLeft)
                    {
                        currentState = CurrentState.movingLeft;
                        boomerang.trajectory = new Vector2(-PIVOT_SPEED, 0);
                        spriteFactory.GoriyaBoomerangAttack(boomerang);
                    }
                    break;
                default:
                    break;
            }
        }
        public void Inward()
        {
            currentState = (CurrentState) movementCalc.MovementCalculation(BASE_SPEED);
        }
        public void InwardReturnWindow()
        {
            currentState = (CurrentState) movementCalc.MovementCalculation(PIVOT_SPEED);
        }
        public void Update()
        {
            movementCalc.Update();
            if ((int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) < (RANGE - RETURN_WINDOW) && newItem)
            {
                direction = (Direction)boomerang.goriyaState.direction;
                Outward();
                newItem = false;
            }
            else if ((int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) > (RANGE - RETURN_WINDOW) && (int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) < RANGE && !returning)
            {
                OutwardReturnWindow();
            }
            else if ((int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) > RANGE && !returning)
            {
                returning = true;
            }
            else if ((int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) > (RANGE - RETURN_WINDOW) && returning)
            {
                returnDirection = (Direction)directionalCalc.CalculateReturnDirection();
                InwardReturnWindow();
            }
            else if ((int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) < (RANGE - RETURN_WINDOW) && (int)Vector2.Distance(boomerang.drawLocation, boomerang.SpawnLocation) > DESPAWN_DISTANCE && returning)
            {
                if (!brake) { currentState = CurrentState.none; brake = true; }
                returnDirection = (Direction) directionalCalc.CalculateReturnDirection();
                Inward();
            }
        }
    }
}
