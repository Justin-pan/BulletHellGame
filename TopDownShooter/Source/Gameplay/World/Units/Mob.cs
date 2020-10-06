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
    public class Mob : Unit
    {
        public bool currentlyPathing;
        public JPTimer rePathTimer = new JPTimer(200);
        public Mob(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, int OwnerId) : base(Path, Pos, Dims, Frames, OwnerId)
        {
            currentlyPathing = false;
            speed = 2.0f;
        }

        public override void Update(Vector2 Offset, Player Enemy, SquareGrid Grid)
        {
            AI(Enemy, Grid);

            base.Update(Offset, Enemy, Grid);
        }

        public virtual void AI(Player Enemy, SquareGrid Grid)
        {
            rePathTimer.UpdateTimer();

            if(pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y) || rePathTimer.Test())
            {
                if(!currentlyPathing)
                {
                    Task repathTask = new Task(() =>
                    {
                        currentlyPathing = true;
                        pathNodes = FindPath(Grid, Grid.GetSlotFromPixel(Enemy.hero.pos, Vector2.Zero));
                        moveTo = pathNodes[0];
                        pathNodes.RemoveAt(0);

                        rePathTimer.ResetToZero();
                        currentlyPathing = false;
                    });

                    repathTask.Start();
                }
            }
            else
            {
                MoveToTarget();

                if(Globals.GetDistance(pos, Enemy.hero.pos) < Grid.slotDims.X * 1.2f)
                {
                    Enemy.hero.GetHit(1);
                    dead = true;
                }
            }
            
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
