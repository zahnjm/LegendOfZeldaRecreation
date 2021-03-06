using CSE3902_Game_Sprint0.Classes.Projectiles;
using CSE3902_Game_Sprint0.Classes.SpriteFactories;
using Microsoft.Xna.Framework;

namespace CSE3902_Game_Sprint0.Classes.LinkContent.LinkScripts
{
    public class LinkArrow : ICommand
    {
        private Link link { get; set; }
        private LinkSpriteFactory spriteFactory { get; set; }
        private LinkStateMachine linkStateMachine { get; set; }

        public LinkArrow(Link link, LinkSpriteFactory spriteFactory, LinkStateMachine linkStateMachine)
        {
            this.link = link;
            this.spriteFactory = spriteFactory;
            this.linkStateMachine = linkStateMachine;
        }

        public void Execute()
        {
            link.spriteSize.X = 16; link.spriteSize.Y = 16;
            link.velocity.X = 0; link.velocity.Y = 0;
            switch (linkStateMachine.direction)
            {
                case LinkStateMachine.Direction.right:
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.arrowRight)
                    {
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.arrowRight;
                        link.linkSprite = spriteFactory.ArrowRight();
                        linkStateMachine.projectileHandler.Add(new Arrow(link.game, new Vector2(link.drawLocation.X + 16, link.drawLocation.Y), linkStateMachine.projectileHandler, Projectiles.Arrow.Direction.right));
                    }
                    break;
                case LinkStateMachine.Direction.up:
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.arrowUp)
                    {
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.arrowUp;
                        link.linkSprite = spriteFactory.ArrowUp();
                        linkStateMachine.projectileHandler.Add(new Arrow(link.game, new Vector2(link.drawLocation.X + 4, link.drawLocation.Y - 16), linkStateMachine.projectileHandler, Projectiles.Arrow.Direction.up));
                    }
                    break;
                case LinkStateMachine.Direction.left:
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.arrowLeft)
                    {
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.arrowLeft;
                        link.linkSprite = spriteFactory.ArrowLeft();
                        linkStateMachine.projectileHandler.Add(new Arrow(link.game, new Vector2(link.drawLocation.X - 16, link.drawLocation.Y), linkStateMachine.projectileHandler, Projectiles.Arrow.Direction.left));
                    }
                    break;
                case LinkStateMachine.Direction.down:
                    if (linkStateMachine.currentState != LinkStateMachine.CurrentState.arrowDown)
                    {
                        linkStateMachine.currentState = LinkStateMachine.CurrentState.arrowDown;
                        link.linkSprite = spriteFactory.ArrowDown();
                        linkStateMachine.projectileHandler.Add(new Arrow(link.game, new Vector2(link.drawLocation.X + 4, link.drawLocation.Y + 16), linkStateMachine.projectileHandler, Projectiles.Arrow.Direction.down));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
