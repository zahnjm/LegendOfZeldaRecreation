using CSE3902_Game_Sprint0.Classes._21._2._13;

namespace CSE3902_Game_Sprint0.Classes.Enemy.Goriya.Scripts
{
    public class GoriyaSpawning : ICommand
    {
        private EnemyGoriya goriya { get; set; }
        private GoriyaSpriteFactory enemySpriteFactory { get; set; }
        private GoriyaStateMachine GoriyaStateMachine { get; set; }
        public GoriyaSpawning(EnemyGoriya goriya, GoriyaSpriteFactory enemySpriteFactory, GoriyaStateMachine GoriyaStateMachine)
        {
            this.goriya = goriya;
            this.enemySpriteFactory = enemySpriteFactory;
            this.GoriyaStateMachine = GoriyaStateMachine;
        }
        public void Execute()
        {
            goriya.spriteSize.X = GoriyaHelper.xsize;
            goriya.spriteSize.Y = GoriyaHelper.ysize;
            goriya.velocity.X = 0;
            goriya.velocity.Y = 0;
            if (GoriyaStateMachine.currentState != GoriyaStateMachine.CurrentState.spawning)
            {
                GoriyaStateMachine.currentState = GoriyaStateMachine.CurrentState.spawning;
                goriya.mySprite = enemySpriteFactory.SpawnGoriya();
            }

        }

    }

}