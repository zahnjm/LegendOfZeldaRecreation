﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Gel.GelScripts
{
    public class GelDying : ICommand
    {
        private EnemySlime gel;
        private GelSpriteFactory gelSpriteFactory;
        private GelStateMachine gelStateMachine;

        public GelDying(EnemySlime gel, GelSpriteFactory gelSpriteFactory, GelStateMachine gelStateMachine)
        {
            this.gel = gel;
            this.gelSpriteFactory = gelSpriteFactory;
            this.gelStateMachine = gelStateMachine;
        }

        public void Execute()
        {
            gel.spriteSize.X = 16;
            gel.spriteSize.Y = 16;
            gel.velocity.X = 0;
            gel.velocity.Y = 0;

            if (gelStateMachine.currentState != GelStateMachine.CurrentState.dying)
            {
                gelStateMachine.currentState = GelStateMachine.CurrentState.dying;
                gel.mySprite = gelSpriteFactory.SpawnGel();
                gel.game.collisionManager.collisionEntities.Remove(gel);
                gel.game.sounds["enemyDie"].CreateInstance().Play();
            }
        }
    }
}
