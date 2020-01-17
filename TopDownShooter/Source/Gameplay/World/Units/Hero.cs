using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    public class Hero : Unit
    {
        //JPTimer buildingTimer = new JPTimer(5000);
        public Hero(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, int OwnerId) : base(Path, Pos, Dims, Frames, OwnerId)
        {
            speed = 2.0f;

            health = 5;
            healthMax = health;

            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 133, 0, "Stand"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 133, 0, "Walk"));
        }

        public override void Update(Vector2 Offset)
        {
            bool checkScroll = false;
            //buildingTimer.UpdateTimer();

            if (Globals.keyboard.GetPress("A"))
            {
                // Going to the left or up is negative
                pos = new Vector2(pos.X - speed, pos.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPress("D"))
            {
                // Going to the left or up is negative
                pos = new Vector2(pos.X + speed, pos.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPress("W"))
            {
                // Going to the left or up is negative
                pos = new Vector2(pos.X, pos.Y - speed);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPress("S"))
            {
                // Going to the left or up is negative
                pos = new Vector2(pos.X, pos.Y + speed);
                checkScroll = true;
            }

            if (Globals.keyboard.GetSinglePress("D1"))
            {

                GameGlobals.PassBuilding(new ArrowTower(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId));
                /*
                if (buildingTimer.Test())
                {
                    
                    buildingTimer.ResetToZero();
                }
                */
            }

            if (checkScroll)
            {
                GameGlobals.CheckScroll(pos);

                SetAnimationByName("Walk");
            }
            else
            {
                SetAnimationByName("Stand");
            }

            rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - Offset);

            if (Globals.mouse.LeftClick())
            {
                // C# automatically pass by reference so create new objects with old data if needed
                GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - Offset));
            }

            base.Update(Offset);
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
