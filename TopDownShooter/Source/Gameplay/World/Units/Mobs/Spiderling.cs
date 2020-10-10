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
    public class Spiderling : Mob
    {

        public Spiderling(Vector2 Pos, Vector2 Frames, int OwnerId) : base("2d\\Units\\Mobs\\Spider", Pos, new Vector2(20, 20), Frames, OwnerId)
        {
            speed = 3.0f;    
        }

        public override void Update(Vector2 Offset, Player Enemy, SquareGrid Grid)
        {

            base.Update(Offset, Enemy, Grid);
        }

        public override void AI(Player Enemy, SquareGrid Grid)
        {
            Building temp = null;
            for (int i = 0; i < Enemy.buildings.Count; i++)
            {
                if (Enemy.buildings[i].GetType().ToString() == "TopDownShooter.Tower")
                {
                    temp = Enemy.buildings[i];
                }
            }

            if (temp != null)
            {
                if(pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y))
                {
                    pathNodes = FindPath(Grid, Grid.GetSlotFromPixel(temp.pos, Vector2.Zero));
                    moveTo = pathNodes[0];
                    pathNodes.RemoveAt(0);
                }
                else 
                {
                    MoveToTarget();
                    if (Globals.GetDistance(pos, temp.pos) < Grid.slotDims.X * 1.2f)
                    {
                        //can create a var in the specific mob class to change damage amounts and override
                        temp.GetHit(this, 1);
                        dead = true;
                    }
                }
            }
            
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
