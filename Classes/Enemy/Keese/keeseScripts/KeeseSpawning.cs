namespace CSE3902_Game_Sprint0.Classes.Enemy.Keese.keeseScripts
{
    public class KeeseSpawning : ICommand
    {
        private EnemyKeese keese { get; set; }
        private KeeseSpriteFactory enemySpriteFactory { get; set; }
        private KeeseStateMachine KeeseStateMachine { get; set; }
        public KeeseSpawning(EnemyKeese keese, KeeseSpriteFactory enemySpriteFactory, KeeseStateMachine KeeseStateMachine)
        {
            this.keese = keese;
            this.enemySpriteFactory = enemySpriteFactory;
            this.KeeseStateMachine = KeeseStateMachine;
        }
        public void Execute()
        {
            keese.spriteSize.X = KeeseHelper.size;
            keese.spriteSize.Y = KeeseHelper.size;
            keese.velocity.X = 0;
            keese.velocity.Y = 0;

            if (KeeseStateMachine.currentState != KeeseStateMachine.CurrentState.spawning)
            {
                KeeseStateMachine.currentState = KeeseStateMachine.CurrentState.spawning;
                keese.mySprite = enemySpriteFactory.SpawnKeese();
            }

        }

    }

}