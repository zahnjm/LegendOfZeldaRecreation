﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CSE3902_Game_Sprint0.Classes.NewBlocks;
using CSE3902_Game_Sprint0.Classes.SpriteFactories;
using Microsoft.Xna.Framework.Graphics;

namespace CSE3902_Game_Sprint0.Classes.Tiles
{
    public class Statue1 : ITile
    {
        private SpriteBatch batch;
        private Texture2D spriteSheet;
        private Rectangle statue1Tile = TileSpriteFactory.Statue1Tile;
        public Vector2 position;
        public Statue1(ZeldaGame game, Vector2 location)
        {
            game.spriteSheets.TryGetValue("DungeonTileset", out this.spriteSheet);
            this.batch = new SpriteBatch(game.GraphicsDevice);
            this.position = location;
        }
        public void Update()
        {
        }

        public void Draw()
        {
            batch.Draw(spriteSheet, position, statue1Tile, Color.White);
        }
    }
}
