﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE3902_Game_Sprint0.Classes.Level
{
    public class ParserUtility
    {
        public int WINDOW_X_ADJUST = 176;
        public int WINDOW_Y_ADJUST = 256;
        public int SCALE_FACTOR = 3;
        public int GEN_ADJUST = 2;
        private static int SPRITE_SIZE = 16;
        private static int BLOCK_ADJUST = 6;
        private static int LARGE_ADJUST = 12;
        private static int CONTAINER_ADJUST = 4;
        private static int MID_ADJUST = 129;

        private ZeldaGame game;
        public ParserUtility(ZeldaGame game)
        {
            this.game = game;
        }

        public Vector2 GetBlockSecondaryItemPosition(int windowWidthFloor, int windowHeightFloor, float x, float y)
        {
            float xDiff = SCALE_FACTOR * x * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + BLOCK_ADJUST;
            float yDiff = SCALE_FACTOR * y * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + BLOCK_ADJUST;
            return new Vector2(windowWidthFloor + xDiff, windowHeightFloor + yDiff);
        }

        public Vector2 GetHeartPosition(int windowWidthFloor, int windowHeightFloor, float x, float y)
        {
            float xDiff = SCALE_FACTOR * x * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + LARGE_ADJUST;
            float yDiff = SCALE_FACTOR * y * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + LARGE_ADJUST;
            return new Vector2(windowWidthFloor + xDiff, windowHeightFloor + yDiff);
        }

        public Vector2 GetHeartContainerPosition(int windowWidthFloor, int windowHeightFloor, float x, float y)
        {
            float xDiff = SCALE_FACTOR * x * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + CONTAINER_ADJUST;
            float yDiff = SCALE_FACTOR * y * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + BLOCK_ADJUST;
            return new Vector2(windowWidthFloor + xDiff, windowHeightFloor + yDiff);
        }

        public Vector2 GetCommonItemPosition(int windowWidthFloor, int windowHeightFloor, float x, float y)
        {
            float xDiff = SCALE_FACTOR * x * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + LARGE_ADJUST;
            float yDiff = SCALE_FACTOR * y * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE;
            return new Vector2(windowWidthFloor + xDiff, windowHeightFloor + yDiff);
        }

        public Vector2 GetBombPosition(int windowWidthFloor, int windowHeightFloor, float x, float y)
        {
            float xDiff = SCALE_FACTOR * x * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + LARGE_ADJUST;
            float yDiff = SCALE_FACTOR * y * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE + GEN_ADJUST;
            return new Vector2(windowWidthFloor + xDiff, windowHeightFloor + yDiff);
        }

        public Vector2 GetEnemyPosition(int windowWidthFloor, int windowHeightFloor, float x, float y)
        {
            float xDiff = SCALE_FACTOR * x * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE;
            float yDiff = SCALE_FACTOR * y * SPRITE_SIZE + SCALE_FACTOR * SPRITE_SIZE;
            return new Vector2(windowWidthFloor + xDiff, windowHeightFloor + yDiff);
        }

        public Vector2 GetTriforceOldPosition(int windowWidth, int windowHeightFloor)
        {
            return new Vector2((windowWidth / GEN_ADJUST) - SPRITE_SIZE, windowHeightFloor + MID_ADJUST * GEN_ADJUST);
        }
    }
}
