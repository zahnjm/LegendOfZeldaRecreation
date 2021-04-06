﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Keese.keeseScripts
{
    public class KeeseDying : ICommand
    {
        private EnemyKeese keese;
        private KeeseSpriteFactory enemySpriteFactory;
        private KeeseStateMachine KeeseStateMachine;
        public KeeseDying(EnemyKeese keese, KeeseSpriteFactory enemySpriteFactory, KeeseStateMachine KeeseStateMachine)
        {
            this.keese = keese;
            this.enemySpriteFactory = enemySpriteFactory;
            this.KeeseStateMachine = KeeseStateMachine;
        }
        public void Execute()
        {
            keese.spriteSize.X = 16;
            keese.spriteSize.Y = 16;
            keese.velocity.X = 0;
            keese.velocity.Y = 0;

            if (KeeseStateMachine.currentState != KeeseStateMachine.CurrentState.dying)
            {
                KeeseStateMachine.currentState = KeeseStateMachine.CurrentState.dying;
                keese.mySprite = enemySpriteFactory.SpawnKeese();
                keese.game.collisionManager.collisionEntities.Remove(keese);
                keese.game.sounds["enemyDie"].CreateInstance().Play();
            }
        }
    }
}