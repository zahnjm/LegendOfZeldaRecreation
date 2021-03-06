namespace CSE3902_Game_Sprint0.Classes.Enemy.Wallmaster.WallmasterScripts
{
    public class WallmasterIdle : ICommand
    {
        private EnemyWallmaster wallmaster { get; set; }
        private WallmasterSpriteFactory wallmasterSpriteFactory { get; set; }
        private WallmasterStateMachine wallmasterStateMachine { get; set; }

        public WallmasterIdle(EnemyWallmaster wallmaster, WallmasterSpriteFactory wallmasterSpriteFactory, WallmasterStateMachine wallmasterStateMachine)
        {
            this.wallmaster = wallmaster;
            this.wallmasterSpriteFactory = wallmasterSpriteFactory;
            this.wallmasterStateMachine = wallmasterStateMachine;
        }

        public void Execute()
        {
            wallmaster.spriteSize.X = WallmasterHelper.size;
            wallmaster.spriteSize.Y = WallmasterHelper.size;
            wallmaster.velocity.X = 0;
            wallmaster.velocity.Y = 0;

            if (wallmasterStateMachine.currentState != WallmasterStateMachine.CurrentState.idle)
            {
                wallmasterStateMachine.currentState = WallmasterStateMachine.CurrentState.idle;
                wallmaster.mySprite = wallmasterSpriteFactory.WallmasterIdle();
            }
        }
    }
}
