using CSE3902_Game_Sprint0.Classes.GameState;
using CSE3902_Game_Sprint0.Classes.Items;
using CSE3902_Game_Sprint0.Interfaces;

namespace CSE3902_Game_Sprint0.Classes.Collisions.CollisionScripts
{
    public class LinkOnItem : ICommand
    {
        private Link link { get; set; }
        private IItem item { get; set; }
        private Collision.Collision.Direction direction { get; set; }

        public LinkOnItem(Link link, IItem item, Collision.Collision.Direction direction)
        {
            this.link = link;
            this.item = item;
            this.direction = direction;
        }

        public void Execute()
        {
            link.game.collisionManager.collisionEntities.Remove((ICollisionEntity)item);
            link.game.currentRoom.removeItem(item);
            if (item is Triforce)
            {
                link.game.sounds["fanfare"].CreateInstance().Play(); link.game.sounds["getItem"].CreateInstance().Play();
                link.linkState.grabItem = true; link.linkState.isTriforce = true;
                link.game.currentGameState = new WinState(link.game);
            }
            else if (item is Bow)
            {
                link.linkState.grabItem = true; link.linkState.isBow = true;
                link.game.sounds["fanfare"].CreateInstance().Play(); link.game.sounds["getItem"].CreateInstance().Play();
            }
            else if (item is Key || item is Heart || item is Bomb) link.game.sounds["getHeart"].CreateInstance().Play();
            else if (item is XP)
            {
                link.game.sounds["getHeart"].CreateInstance().Play();
                link.game.util.numXP++;
                if (link.game.util.numXP % link.game.util.XPPerLevel * link.game.util.difficultyMult == 0)
                {
                    link.game.sounds["fanfare"].CreateInstance().Play();
                    if (link.game.util.linkXPlevel <= 9) link.game.util.linkXPlevel += 1;
                }
            }
            else if (item is Boomerang || item is Compass || item is Fairy || item is HeartContainer || item is Map || item is Triforce) link.game.sounds["getItem"].CreateInstance().Play();
            else if (item is BlueRupee || item is YellowRupee) link.game.sounds["getRupee"].CreateInstance().Play();
        }
    }
}
