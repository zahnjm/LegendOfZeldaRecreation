namespace CSE3902_Game_Sprint0.Classes.Controllers.GameCommands
{
    public class TwoPlayer : ICommand
    {
        private ZeldaGame game { get; set; }
        public TwoPlayer(ZeldaGame game)
        {
            this.game = game;
        }

        public void Execute()
        {
            if (!(game.twoPlayer))
            {
                game.twoPlayer = true;
                game.littleHelper = new LittleHelper.LittleHelper(game);
            }
        }
    }
}
