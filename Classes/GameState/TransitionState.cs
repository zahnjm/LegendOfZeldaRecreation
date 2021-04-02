﻿using CSE3902_Game_Sprint0.Classes.Level;
using CSE3902_Game_Sprint0.Classes.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using CSE3902_Game_Sprint0.Classes.Items;
using Microsoft.Xna.Framework;
using CSE3902_Game_Sprint0.Classes.Doors;

namespace CSE3902_Game_Sprint0.Classes.GameState
{
    public class TransitionState : IGameState
    {
        public ZeldaGame game;
        public Room oldroom;

        public Room nextroom;
        int timer = 0;
        public Vector2 drawLocationInteriorOld;
        public Vector2 drawLocationExteriorOld;
        public Vector2 drawLocationInteriorNext;
        public Vector2 drawLocationExteriorNext;

        public ISprite roominteriorOld;
        public ISprite roomexteriorOld;

        public ISprite roominteriorNext;
        public ISprite roomexteriorNext;

        public ISprite topDoorOld;
        public ISprite leftDoorOld;
        public ISprite rightDoorOld;
        public ISprite bottomDoorOld;

        public ISprite topDoorNext;
        public ISprite leftDoorNext;
        public ISprite rightDoorNext;
        public ISprite bottomDoorNext;

        public Vector2 drawLocationTopDoorOld;
        public Vector2 drawLocationLeftDoorOld;
        public Vector2 drawLocationRightDoorOld;
        public Vector2 drawLocationBottomDoorOld;
        public Vector2 drawLocationTopDoorNext;
        public Vector2 drawLocationLeftDoorNext;
        public Vector2 drawLocationRightDoorNext;
        public Vector2 drawLocationBottomDoorNext;

        private int windowWidth;
        private int windowHeight;
        private Texture2D itemSpriteSheet;
        private int roomLimiter;
        private int drawOffset;
        private Collision.Collision.Direction transitionDirection;
        //public int transitionDirection = 0;
        public int animationSpeed = 6;
       
        public TransitionState(ZeldaGame game, Room oldroom, Room nextroom, Collision.Collision.Direction transitionDirection)
        {

            this.game = game;
            this.oldroom = oldroom;
            this.nextroom = nextroom;
            timer = 128;

            RoomTextureStorage roomTextures = new RoomTextureStorage(this.game);
            game.spriteSheets.TryGetValue("DungeonTileset", out itemSpriteSheet);

            windowWidth = game.GraphicsDevice.Viewport.Width;
            windowHeight = game.GraphicsDevice.Viewport.Height;

            int windowHeightFloor = ((windowHeight / ParserUtility.SCALE_FACTOR - ParserUtility.WINDOW_X_ADJUST / ParserUtility.SCALE_FACTOR) / ParserUtility.GEN_ADJUST) + ParserUtility.GAME_FRAME_ADJUST;
            int windowWidthFloor = (windowWidth / ParserUtility.SCALE_FACTOR - ParserUtility.WINDOW_Y_ADJUST / ParserUtility.SCALE_FACTOR) / ParserUtility.GEN_ADJUST;

            roomLimiter = 10;
            drawOffset = 96;

            this.transitionDirection = transitionDirection;

            Vector2 newRoomShift = new Vector2(0, 0);

            //SLIDE
            switch(transitionDirection)
            { 
                case Collision.Collision.Direction.up:
                    newRoomShift = new Vector2(0, -176 * 3);
                    timer = 176 * 3 / animationSpeed;
                    break;
                case Collision.Collision.Direction.left:
                    newRoomShift = new Vector2(-256 * 3, 0);
                    timer = 256 * 3 / animationSpeed;
                    break;
                case Collision.Collision.Direction.right:
                    newRoomShift = new Vector2(256 * 3,0);
                    timer = 256 * 3 / animationSpeed;
                    break;
                case Collision.Collision.Direction.down:
                    newRoomShift = new Vector2(0, 176 * 3);
                    timer = 176 * 3 / animationSpeed;
                    break;
            }

            drawLocationInteriorOld = new Vector2(windowWidthFloor + drawOffset, windowHeightFloor + drawOffset);
            drawLocationExteriorOld = new Vector2(windowWidthFloor, windowHeightFloor);


            drawLocationInteriorNext = new Vector2(windowWidthFloor + drawOffset, windowHeightFloor + drawOffset) + newRoomShift;
            drawLocationExteriorNext = new Vector2(windowWidthFloor, windowHeightFloor) + newRoomShift;


            int oldRoomNumber = this.oldroom.getRoomNumber();
            roominteriorOld = roomTextures.getRoom(oldRoomNumber);
            roomexteriorOld = new UniversalSprite(game, itemSpriteSheet, new Rectangle(521, 11, 256, 176), Color.White, SpriteEffects.None, new Vector2(1, 1), roomLimiter, 0.0f);

            int nextRoomNumber = nextroom.getRoomNumber();
            roominteriorNext = roomTextures.getRoom(nextRoomNumber);

            roomexteriorNext = new UniversalSprite(game, itemSpriteSheet, new Rectangle(521, 11, 256, 176), Color.White, SpriteEffects.None, new Vector2(1, 1), roomLimiter, 0.0f);

            drawLocationTopDoorOld = new Vector2(windowWidthFloor + 112 * ParserUtility.SCALE_FACTOR, windowHeightFloor);
            drawLocationLeftDoorOld = new Vector2(windowWidthFloor, windowHeightFloor + 72 * ParserUtility.SCALE_FACTOR);
            drawLocationRightDoorOld = new Vector2(windowWidthFloor + 224 * ParserUtility.SCALE_FACTOR, windowHeightFloor + 72 * ParserUtility.SCALE_FACTOR);
            drawLocationBottomDoorOld = new Vector2(windowWidthFloor + 112 * ParserUtility.SCALE_FACTOR, windowHeightFloor + 144 * ParserUtility.SCALE_FACTOR);

            drawLocationTopDoorNext = new Vector2(windowWidthFloor + 112 * ParserUtility.SCALE_FACTOR, windowHeightFloor) + newRoomShift;
            drawLocationLeftDoorNext = new Vector2(windowWidthFloor, windowHeightFloor + 72 * ParserUtility.SCALE_FACTOR) + newRoomShift;
            drawLocationRightDoorNext = new Vector2(windowWidthFloor + 224 * ParserUtility.SCALE_FACTOR, windowHeightFloor + 72 * ParserUtility.SCALE_FACTOR) + newRoomShift;
            drawLocationBottomDoorNext = new Vector2(windowWidthFloor + 112 * ParserUtility.SCALE_FACTOR, windowHeightFloor + 144 * ParserUtility.SCALE_FACTOR) + newRoomShift;

            foreach (IDoor door in oldroom.getDoors())
            {
                if (door.GetType() == typeof(TopDoor))
                {
                    topDoorOld = roomTextures.getDoor(door.getDoorValue());
                }
                else if(door.GetType() == typeof(LeftDoor))
                {
                    leftDoorOld = roomTextures.getDoor(door.getDoorValue());
                }
                else if (door.GetType() == typeof(RightDoor))
                {
                    rightDoorOld = roomTextures.getDoor(door.getDoorValue());
                }
                else if (door.GetType() == typeof(BottomDoor))
                {
                    bottomDoorOld = roomTextures.getDoor(door.getDoorValue());
                }
            }

            foreach (IDoor door in nextroom.getDoors())
            {
                if (door.GetType() == typeof(TopDoor))
                {
                    topDoorNext = roomTextures.getDoor(door.getDoorValue());
                }
                else if (door.GetType() == typeof(LeftDoor))
                {
                    leftDoorNext = roomTextures.getDoor(door.getDoorValue());
                }
                else if (door.GetType() == typeof(RightDoor))
                {
                    rightDoorNext = roomTextures.getDoor(door.getDoorValue());
                }
                else if (door.GetType() == typeof(BottomDoor))
                {
                    bottomDoorNext = roomTextures.getDoor(door.getDoorValue());
                }
            }

        }

        public void Draw()
        {
            roomexteriorOld.Draw(drawLocationExteriorOld);
            roominteriorOld.Draw(drawLocationInteriorOld);
            roomexteriorNext.Draw(drawLocationExteriorNext);
            roominteriorNext.Draw(drawLocationInteriorNext);
            topDoorOld.Draw(drawLocationTopDoorOld);
            leftDoorOld.Draw(drawLocationLeftDoorOld);
            rightDoorOld.Draw(drawLocationRightDoorOld);
            bottomDoorOld.Draw(drawLocationBottomDoorOld);
            topDoorNext.Draw(drawLocationTopDoorNext);
            leftDoorNext.Draw(drawLocationLeftDoorNext);
            rightDoorNext.Draw(drawLocationRightDoorNext);
            bottomDoorNext.Draw(drawLocationBottomDoorNext);

        }
        public void UpdateCollisions()
        {

        }
        public void Update()
        {
            int windowHeightFloor = ((windowHeight / ParserUtility.SCALE_FACTOR - ParserUtility.WINDOW_X_ADJUST / ParserUtility.SCALE_FACTOR) / ParserUtility.GEN_ADJUST) + ParserUtility.GAME_FRAME_ADJUST;
            int windowWidthFloor = (windowWidth / ParserUtility.SCALE_FACTOR - ParserUtility.WINDOW_Y_ADJUST / ParserUtility.SCALE_FACTOR) / ParserUtility.GEN_ADJUST;
            timer--;
            if(timer == 0)
            {
                game.currentGameState = game.currentMainGameState;
                game.keyPressedTempVariable = false;
                switch (transitionDirection)
                {
                    case Collision.Collision.Direction.up:
                        game.linkStateMachine.ChangeDirection(LinkStateMachine.Direction.up);
                        game.link.SetLocation(new Vector2(windowWidthFloor + 112 * ParserUtility.SCALE_FACTOR + 8 * ParserUtility.SCALE_FACTOR, windowHeightFloor + 144 * ParserUtility.SCALE_FACTOR));
                        break;
                    case Collision.Collision.Direction.left:
                        game.linkStateMachine.ChangeDirection(LinkStateMachine.Direction.left);
                        game.link.SetLocation(new Vector2(windowWidthFloor + 224 * ParserUtility.SCALE_FACTOR, windowHeightFloor + 72 * ParserUtility.SCALE_FACTOR + 8 * ParserUtility.SCALE_FACTOR));
                        break;
                    case Collision.Collision.Direction.right:
                        game.linkStateMachine.ChangeDirection(LinkStateMachine.Direction.right);
                        game.link.SetLocation(new Vector2(windowWidthFloor + ParserUtility.SPRITE_SIZE * ParserUtility.SCALE_FACTOR, windowHeightFloor + 72 * ParserUtility.SCALE_FACTOR + 8 * ParserUtility.SCALE_FACTOR));
                        break;
                    case Collision.Collision.Direction.down:
                        game.linkStateMachine.ChangeDirection(LinkStateMachine.Direction.down);
                        game.link.SetLocation(new Vector2(windowWidthFloor + 112 * 3 + 8 * ParserUtility.SCALE_FACTOR, windowHeightFloor + ParserUtility.SPRITE_SIZE * ParserUtility.SCALE_FACTOR));
                        break;
                }
            }



            Vector2 animationShift = new Vector2(0, 0);

            switch (transitionDirection)
            {
                case Collision.Collision.Direction.up:
                    animationShift = new Vector2(0, animationSpeed);
                    break;
                case Collision.Collision.Direction.left:
                    animationShift = new Vector2(animationSpeed, 0);
                    break;
                case Collision.Collision.Direction.right:
                    animationShift = new Vector2(-animationSpeed, 0);
                    break;
                case Collision.Collision.Direction.down:
                    animationShift = new Vector2(0, -animationSpeed);
                    break;
            }

            drawLocationExteriorOld = drawLocationExteriorOld + animationShift;
            drawLocationInteriorOld = drawLocationInteriorOld + animationShift;

            drawLocationExteriorNext = drawLocationExteriorNext + animationShift;
            drawLocationInteriorNext = drawLocationInteriorNext + animationShift;

            drawLocationTopDoorOld = drawLocationTopDoorOld + animationShift;
            drawLocationLeftDoorOld = drawLocationLeftDoorOld + animationShift;
            drawLocationRightDoorOld = drawLocationRightDoorOld + animationShift;
            drawLocationBottomDoorOld = drawLocationBottomDoorOld + animationShift;

            drawLocationTopDoorNext = drawLocationTopDoorNext + animationShift;
            drawLocationLeftDoorNext = drawLocationLeftDoorNext + animationShift;
            drawLocationRightDoorNext = drawLocationRightDoorNext + animationShift;
            drawLocationBottomDoorNext = drawLocationBottomDoorNext + animationShift;

        }
}
}
