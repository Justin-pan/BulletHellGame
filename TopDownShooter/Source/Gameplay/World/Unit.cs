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
using System.IO;

namespace TopDownShooter
{
    public class Unit : DestructibleObject
    {

        protected Vector2 moveTo;

        protected List<Vector2> pathNodes = new List<Vector2>();

        public Unit(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, int OwnerId) : base(Path, Pos, Dims, Frames, OwnerId)
        {
            moveTo = new Vector2(Pos.X, Pos.Y);
        }

        public override void Update(Vector2 Offset, Player Enemy, SquareGrid Grid)
        {
            base.Update(Offset, Enemy, Grid);
        }

        public virtual List<Vector2> FindPath(SquareGrid Grid, Vector2 EndSlot)
        {
            pathNodes.Clear();

            Vector2 tempStartSlot = Grid.GetSlotFromPixel(pos, Vector2.Zero);

            List<Vector2> tempPath = Grid.GetPath(tempStartSlot, EndSlot, true);

            if(tempPath == null || tempPath.Count == 0)
            {
                
            }

            return tempPath;
        }

        public virtual void MoveToTarget()
        {
            if(pos.X != moveTo.X || pos.Y != moveTo.Y)
            {
                pos += Globals.RadialMovement(moveTo, pos, speed);
            }
            else if(pathNodes.Count > 0)
            {
                moveTo = pathNodes[0];
                pathNodes.RemoveAt(0);

                pos += Globals.RadialMovement(moveTo, pos, speed);
            }

            rot = Globals.RotateTowards(pos, moveTo);
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
