using CSE3902_Game_Sprint0.Classes.LinkContent.LinkScripts;
using CSE3902_Game_Sprint0.Classes.Projectiles;
using CSE3902_Game_Sprint0.Classes.SpriteFactories;
using Microsoft.Xna.Framework.Media;
using System;
namespace CSE3902_Game_Sprint0.Classes
{
    public class LinkStateMachine
    {
        private ZeldaGame game { get; set; }
        private Link link { get; set; }
        public LinkSpriteFactory spriteFactory { get; set; }
        public ProjectileHandler projectileHandler { get; set; }

        public enum Direction { right, up, left, down };
        public Direction direction { get; set; } = Direction.down;
        public bool moving { get; set; } = false;

        public enum Weapon { none, sword, bomb, arrow, boomerang };
        public Weapon weaponSelected { get; set; } = Weapon.bomb;

        public int timer { get; set; } = 0;

        public int bowTimer { get; set; }
        private int invincibilityFrames { get; set; } = 0;
        public int wallmasterDeployedTimer { get; set; } = 0;

        public bool isRolling { get; set; } = false;
        public bool grabItem { get; set; } = false;
        public bool isTriforce = false;
        public bool isBow = false;
        public bool useSword { get; set; } = false;
        public bool usePortal { get; set; } = false;
        public bool useBomb { get; set; } = false;
        public bool useArrow { get; set; } = false;
        public bool useBoomerang { get; set; } = false;
        public bool isDamaged { get; set; } = false;
        public bool isGrabbed { get; set; } = false;
        public bool dying { get; set; } = false;
        public bool dead { get; set; } = false;
        public bool boomerangCaught { get; set; } = true;

        public enum CurrentState { none, idleUp, idleDown, idleLeft, idleRight, movingUp, movingDown, movingLeft, movingRight, rollingUp, rollingDown, rollingLeft, rollingRight, damagedUp, damagedDown, damagedLeft, damagedRight, swordUp, swordRight, swordDown, swordLeft, portalUp, portalRight, portalDown, portalLeft, boomerangUp, boomerangRight, boomerangDown, boomerangLeft, bombUp, bombRight, bombDown, bombLeft, arrowUp, arrowRight, arrowDown, arrowLeft, dying, grabbing, grabbed };
        public CurrentState currentState;

        public LinkStateMachine(Link link)
        {
            game = link.game;
            this.link = link;
            spriteFactory = new LinkSpriteFactory(link);
            this.projectileHandler = game.projectileHandler;
        }

        public void ChangeDirection(Direction toThis) { this.direction = toThis; }
        public void Idle() { if (timer == 0) new LinkIdle(link, spriteFactory, this).Execute(); }
        public void Moving() { if (timer == 0) new LinkMoving(link, spriteFactory, this).Execute(); }
        public void Roll() { if (timer == 0) { timer = link.helper.ONESECOND / 3; new LinkRoll(link, spriteFactory, this).Execute(); } }
        public void Sword()
        {
            useSword = false;
            if (timer == 0)
            {
                timer = link.helper.ONESECOND / 5;
                new LinkSword(link, spriteFactory, this).Execute(); new LinkOffset(link, false).Execute();
                link.game.sounds["swordSlash"].CreateInstance().Play();
            }
        }
        public void Portal()
        {
            usePortal = false;
            if (timer == 0)
            {
                timer = link.helper.ONESECOND / 5;
                new LinkPortal(link, spriteFactory, this).Execute(); new LinkOffset(link, false).Execute();
                var random = new Random();
                switch (random.Next(2))
                {
                    case 0:
                        link.game.sounds["portalBlue"].CreateInstance().Play();
                        break;
                    default:
                        link.game.sounds["portalOrange"].CreateInstance().Play();
                        break;
                }
            }
        }
        public void Bomb()
        {
            if (game.util.numBombs > 0)
            {
                if (timer == 0)
                {
                    timer = link.helper.ONESECOND / 4;
                    new LinkBomb(link, spriteFactory, this).Execute();
                    link.game.sounds["bombDrop"].CreateInstance().Play();
                    game.util.numBombs -= 1;
                }
            }
        }
        public void Boomerang()
        {
            useBoomerang = false;
            if (timer == 0) { timer = link.helper.ONESECOND / 4; new LinkContent.LinkScripts.LinkBoomerang(link, spriteFactory, this).Execute(); }
        }
        public void Arrow()
        {
            if (game.util.hasBow)
            {
                if (game.util.numYrups > 0)
                {
                    if (timer == 0)
                    {
                        timer = (link.helper.ONESECOND / 12) * 5;
                        new LinkArrow(link, spriteFactory, this).Execute();
                        link.game.sounds["arrowBoomerang"].CreateInstance().Play();
                        game.util.numYrups -= 1;
                    }
                }
            }
        }
        public void Damaged()
        {
            if (timer == 0)
            {
                timer = link.helper.ONESECOND / 5;
                isDamaged = false;
                new LinkDamaged(link, spriteFactory, this).Execute();
                if (invincibilityFrames <= 0)
                {
                    invincibilityFrames = link.helper.ONESECOND;
                    game.util.numLives -= 1;
                    link.game.sounds["linkHurt"].CreateInstance().Play();
                }
            }
        }
        public void Death()
        {
            if (!dead)
            {
                currentState = CurrentState.dying;
                timer = link.helper.ONESECOND + (link.helper.ONESECOND / 3);
                new LinkDeath(link, spriteFactory, this).Execute();
                dead = true;
                link.game.sounds["linkDie"].CreateInstance().Play();
            }
            else if (timer <= 0 && dead)
            {
                timer = link.helper.ONESECOND * 3;
                new LinkDeath(link, spriteFactory, this).Execute();
                dying = false; dead = false;
            }
        }
        public void GrabItem()
        {
            timer = link.helper.ONESECOND * 2; bowTimer = link.helper.ONESECOND * 2;
            grabItem = false;
            new LinkGrabItem(link, spriteFactory, this).Execute();
        }
        public void Update()
        {
            if (timer > 0) timer--;
            if (bowTimer > 0) bowTimer--;
            if (timer == 0)
            {
                new LinkOffset(link, true).Execute();
                link.drawOffset.X = 0;
                link.drawOffset.Y = 0;
                MediaPlayer.Resume();
            }
            if (invincibilityFrames > 0) invincibilityFrames--;
            wallmasterDeployedTimer--;
            if (grabItem) { GrabItem(); MediaPlayer.Pause(); }
            else if (dying) Death();
            else if (isRolling) { Roll(); isRolling = false; }
            else if (moving)
            {
                if (isDamaged) Damaged();
                else if (useSword) Sword();
                else if (usePortal) Portal();
                else if (useBomb) { Bomb(); useBomb = false; }
                else if (useArrow) { Arrow(); useArrow = false; }
                else if (useBoomerang && boomerangCaught) { Boomerang(); boomerangCaught = false; }
                else Moving();
            }
            else
            {
                if (isDamaged) Damaged();
                else if (useSword) Sword();
                else if (usePortal) Portal();
                else if (useBomb) { Bomb(); useBomb = false; }
                else if (useArrow) { Arrow(); useArrow = false; }
                else if (useBoomerang && boomerangCaught) { Boomerang(); boomerangCaught = false; }
                else if (useBoomerang && !boomerangCaught) useBoomerang = false;
                else Idle();
            }
        }
    }
}
