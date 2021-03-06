namespace CSE3902_Game_Sprint0.Classes.LittleHelper.LittleHelperScripts
{
    public class LittleHelperInteracting : ICommand
    {
        private LittleHelper littleHelper { get; set; }
        private LittleHelperSpriteFactory spriteFactory { get; set; }
        private LittleHelperStateMachine littleHelperStateMachine { get; set; }
        public LittleHelperInteracting(LittleHelper littleHelper, LittleHelperSpriteFactory spriteFactory, LittleHelperStateMachine littleHelperStateMachine)
        {
            this.littleHelper = littleHelper;
            this.spriteFactory = spriteFactory;
            this.littleHelperStateMachine = littleHelperStateMachine;
        }
        public void Execute()
        {
            if (littleHelperStateMachine.currentState != LittleHelperStateMachine.CurrentState.interacting)
            {
                littleHelper.spriteSize.X = 16;
                littleHelper.spriteSize.Y = 16;
                littleHelperStateMachine.currentState = LittleHelperStateMachine.CurrentState.interacting;
                littleHelper.mySprite = spriteFactory.Interacting();
                littleHelper.game.sounds["bossHit"].CreateInstance().Play();
            }
        }
    }
}
