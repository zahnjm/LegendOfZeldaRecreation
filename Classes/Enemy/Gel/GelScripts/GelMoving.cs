namespace CSE3902_Game_Sprint0.Classes.Enemy.Gel.GelScripts
{
    public class GelMoving : ICommand
    {
        private EnemySlime gel { get; set; }
        private GelSpriteFactory gelSpriteFactory { get; set; }
        private GelStateMachine gelStateMachine { get; set; }

        public GelMoving(EnemySlime gel, GelSpriteFactory gelSpriteFactory, GelStateMachine gelStateMachine)
        {
            this.gel = gel;
            this.gelSpriteFactory = gelSpriteFactory;
            this.gelStateMachine = gelStateMachine;
        }

        public void Execute()
        {
            gel.spriteSize.X = GelHelper.size;
            gel.spriteSize.Y = GelHelper.size;

            switch (gelStateMachine.direction)
            {
                case GelStateMachine.Direction.right:
                    if (gelStateMachine.currentState != GelStateMachine.CurrentState.movingRight)
                    {
                        gel.velocity.X = GelHelper.pvelocity;
                        gel.velocity.Y = 0;
                        gelStateMachine.currentState = GelStateMachine.CurrentState.movingRight;
                        gel.mySprite = gelSpriteFactory.GelMovingRight();
                    }
                    break;
                case GelStateMachine.Direction.up:
                    if (gelStateMachine.currentState != GelStateMachine.CurrentState.movingUp)
                    {
                        gel.velocity.X = 0;
                        gel.velocity.Y = GelHelper.nvelocity;
                        gelStateMachine.currentState = GelStateMachine.CurrentState.movingUp;
                        gel.mySprite = gelSpriteFactory.GelMovingUp();
                    }
                    break;
                case GelStateMachine.Direction.left:
                    if (gelStateMachine.currentState != GelStateMachine.CurrentState.movingLeft)
                    {
                        gel.velocity.X = GelHelper.nvelocity;
                        gel.velocity.Y = 0;
                        gelStateMachine.currentState = GelStateMachine.CurrentState.movingLeft;
                        gel.mySprite = gelSpriteFactory.GelMovingLeft();
                    }
                    break;
                case GelStateMachine.Direction.down:
                    if (gelStateMachine.currentState != GelStateMachine.CurrentState.movingDown)
                    {
                        gel.velocity.X = 0;
                        gel.velocity.Y = GelHelper.pvelocity;
                        gelStateMachine.currentState = GelStateMachine.CurrentState.movingDown;
                        gel.mySprite = gelSpriteFactory.GelMovingDown();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
