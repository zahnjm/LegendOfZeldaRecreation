using CSE3902_Game_Sprint0.Classes.SpriteFactories;

namespace CSE3902_Game_Sprint0.Classes.LinkContent.LinkScripts
{
    public class LinkSword : ICommand
    {
        private Link link { get; set; }
        private LinkSpriteFactory spriteFactory { get; set; }
        private LinkStateMachine linkStateMachine { get; set; }

        public LinkSword(Link link, LinkSpriteFactory spriteFactory, LinkStateMachine linkStateMachine)
        {
            this.link = link;
            this.spriteFactory = spriteFactory;
            this.linkStateMachine = linkStateMachine;
        }

        public void Execute()
        {
            link.velocity.X = 0; link.velocity.Y = 0;
            switch (linkStateMachine.direction)
            {
                case LinkStateMachine.Direction.right:
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.swordRight)
                    {
                        link.spriteSize.X = 27; link.spriteSize.Y = 16;
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.swordRight;
                        link.linkSprite = spriteFactory.SwordRight();
                    }
                    break;
                case LinkStateMachine.Direction.up:
                    link.drawOffset.X = 0 * link.spriteScalar; link.drawOffset.Y = -12 * link.spriteScalar;
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.swordUp)
                    {
                        link.spriteSize.X = 16; link.spriteSize.Y = 28;
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.swordUp;
                        link.linkSprite = spriteFactory.SwordUp();
                    }
                    break;
                case LinkStateMachine.Direction.left:
                    link.drawOffset.X = -11 * link.spriteScalar; link.drawOffset.Y = 0 * link.spriteScalar;
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.swordLeft)
                    {
                        link.spriteSize.X = 27; link.spriteSize.Y = 16;
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.swordLeft;
                        link.linkSprite = spriteFactory.SwordLeft();
                    }
                    break;
                case LinkStateMachine.Direction.down:
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.swordDown)
                    {
                        link.spriteSize.X = 16; link.spriteSize.Y = 27;
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.swordDown;
                        link.linkSprite = spriteFactory.SwordDown();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
