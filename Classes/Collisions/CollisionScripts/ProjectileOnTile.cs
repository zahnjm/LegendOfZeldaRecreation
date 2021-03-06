using CSE3902_Game_Sprint0.Classes.NewBlocks;
using CSE3902_Game_Sprint0.Classes.Projectiles;
using CSE3902_Game_Sprint0.Classes.Tiles;
using CSE3902_Game_Sprint0.Interfaces;

namespace CSE3902_Game_Sprint0.Classes.Collisions.CollisionScripts
{
    public class ProjectileOnTile : ICommand
    {
        private IProjectile projectile { get; set; }
        private ITile tile { get; set; }
        private Collision.Collision.Direction direction { get; set; }

        public ProjectileOnTile(IProjectile projectile, ITile tile, Collision.Collision.Direction direction)
        {
            this.projectile = projectile;
            this.tile = tile;
            this.direction = direction;
        }

        public void Execute()
        {
            if (projectile is Arrow) ((Arrow)projectile).myState.hit = true;
            if (projectile is LinkBoomerangProjectile)
            {
                if (tile is WallTile || tile is GateKeeperTile) ((LinkBoomerangProjectile)projectile).myState.returning = true;
            }
            if (projectile is GoriyaBoomerang)
            {
                if (tile is WallTile || tile is GateKeeperTile) ((GoriyaBoomerang)projectile).myState.returning = true;
            }
        }
    }
}
