using CSE3902_Game_Sprint0.Classes.Doors;
using Microsoft.Xna.Framework;

namespace CSE3902_Game_Sprint0.Classes.Level
{
    public class LeftDoor : IDoor
    {
        private ZeldaGame game { get; set; }
        private ISprite leftDoorSprite { get; set; }
        public Vector2 position;
        private readonly RoomTextureStorage leftDoorTexture;
        private int windowWidth { get; set; }
        private int windowHeight { get; set; }
        private int doorValue { get; set; }
        public LeftDoor(ZeldaGame game, RoomTextureStorage textures, int doorValue)
        {
            this.game = game;
            windowWidth = game.GraphicsDevice.Viewport.Width;
            windowHeight = game.GraphicsDevice.Viewport.Height;
            this.doorValue = doorValue;

            int windowHeightFloor = ((windowHeight / ParserUtility.SCALE_FACTOR - ParserUtility.WINDOW_X_ADJUST / ParserUtility.SCALE_FACTOR) / ParserUtility.GEN_ADJUST) + ParserUtility.GAME_FRAME_ADJUST;
            int windowWidthFloor = (windowWidth / ParserUtility.SCALE_FACTOR - ParserUtility.WINDOW_Y_ADJUST / ParserUtility.SCALE_FACTOR) / ParserUtility.GEN_ADJUST;
            windowHeightFloor = windowHeightFloor + DoorUtility.leftDoorAdjust * ParserUtility.SCALE_FACTOR;

            this.position = new Vector2(windowWidthFloor, windowHeightFloor);
            this.leftDoorTexture = textures;
            this.leftDoorSprite = this.leftDoorTexture.getDoor(doorValue);
        }
        public void Update()
        {
        }

        public void Draw()
        {
            leftDoorSprite.Draw(position);
        }
        public int getDoorValue()
        {
            return doorValue;
        }
    }
}