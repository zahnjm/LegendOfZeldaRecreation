﻿using CSE3902_Game_Sprint0.Classes.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Stalfos
{
    public class StalfosSpriteFactory
    {
        private ZeldaGame game { get; set; }
        private Texture2D linkSpriteSheet;
        private Texture2D enemySpriteSheet;
        private float linkLayerDepth { get; set; } = 0.2f;
        public StalfosSpriteFactory(ZeldaGame game)
        {
            this.game = game;
            game.spriteSheets.TryGetValue("DungeonEnemies", out enemySpriteSheet);
            game.spriteSheets.TryGetValue("Link", out linkSpriteSheet);
        }
        public UniversalSprite SpawnStalfos()
        {
            
           return new UniversalSprite(game, linkSpriteSheet, new Rectangle(138, 185, 16, 16), Color.White, SpriteEffects.None, new Vector2(1, 3), 30, linkLayerDepth);
        }

        public UniversalSprite StalfosMovingUp()
        {
            
           return new UniversalSprite(game, enemySpriteSheet, new Rectangle(383, 146, 16, 16), Color.White, SpriteEffects.None, new Vector2(1, 2), 10, linkLayerDepth);
        }

        public UniversalSprite StalfosMovingDown()
        {
            return new UniversalSprite(game, enemySpriteSheet, new Rectangle(383, 146, 16, 16), Color.White, SpriteEffects.None, new Vector2(1, 2), 10, linkLayerDepth);
        }

        public UniversalSprite StalfosMovingLeft()
        {
            return new UniversalSprite(game, enemySpriteSheet, new Rectangle(383, 146, 16, 16), Color.White, SpriteEffects.None, new Vector2(1, 2), 10, linkLayerDepth);
        }

        public UniversalSprite StalfosMovingRight()
        {
           return new UniversalSprite(game, enemySpriteSheet, new Rectangle(383, 146, 16, 16), Color.White, SpriteEffects.None, new Vector2(1, 2), 10, linkLayerDepth);
        }
        public UniversalSprite StalfosIdle()
        {
           return new UniversalSprite(game, enemySpriteSheet, new Rectangle(383, 146, 16, 16), Color.White, SpriteEffects.None, new Vector2(1, 2), 10, linkLayerDepth);
        }
    }
}
