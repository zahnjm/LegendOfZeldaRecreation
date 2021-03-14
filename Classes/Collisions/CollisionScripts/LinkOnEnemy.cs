﻿using CSE3902_Game_Sprint0.Classes._21._2._13;
using CSE3902_Game_Sprint0.Classes.Enemy.Wallmaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Collisions.CollisionScripts
{
    public class LinkOnEnemy : ICommand
    {
        private Link link;
        private IEnemy enemy;
        private Collision.Collision.Direction direction;

        public LinkOnEnemy(Link link, IEnemy enemy, Collision.Collision.Direction direction)
        {
            this.link = link;
            this.enemy = enemy;
            this.direction = direction;
        }

        public void Execute()
        {
            //If link's timer has expired, set his stateMachine to be damaged
            if (link.linkState.timer <= 0)
            {
                link.linkState.isDamaged = true;
            }

            if (enemy is EnemyWallmaster)
            {
                link.drawLocation = ((EnemyWallmaster)enemy).drawLocation;
            }
        }
    }
}