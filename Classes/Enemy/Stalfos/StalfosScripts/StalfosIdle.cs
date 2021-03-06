using CSE3902_Game_Sprint0.Classes._21._2._13;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Stalfos.StalfosScripts
{
    public class StalfosIdle : ICommand
    {
        private EnemyStalfos stalfos { get; set; }
        private StalfosSpriteFactory stalfosSpriteFactory { get; set; }
        private StalfosStateMachine stalfosStateMachine { get; set; }
        public StalfosIdle(EnemyStalfos stalfos, StalfosSpriteFactory stalfosSpriteFactory, StalfosStateMachine stalfosStateMachine)
        {
            this.stalfos = stalfos;
            this.stalfosSpriteFactory = stalfosSpriteFactory;
            this.stalfosStateMachine = stalfosStateMachine;
        }

        public void Execute()
        {
            if (stalfosStateMachine.currentState != StalfosStateMachine.CurrentState.idle)
            {
                stalfosStateMachine.currentState = StalfosStateMachine.CurrentState.idle;
                this.stalfos.mySprite = stalfosSpriteFactory.StalfosIdle();
            }
        }
    }
}
