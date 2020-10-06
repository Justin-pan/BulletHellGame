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
    public class StillInvisibleProjectile : Projectile
    {

        float ticks, currentTick;
        public StillInvisibleProjectile(Vector2 Pos, Vector2 Dims, DestructibleObject Owner, Vector2 Target, int MSec)
            : base("2d\\Misc\\solid", Pos, Dims, Owner, Target)
        {
            ticks = 3;
            currentTick = 0;

            timer = new JPTimer(MSec);
        }

        public override void Update(Vector2 Offset, List<DestructibleObject> Units)
        {
            base.Update(Offset, Units);

            if(timer.Timer >= timer.MSec * (currentTick/(ticks - 1)))
            {
                for(int i = 0; i < Units.Count; i++)
                {
                    if(Globals.GetDistance(Units[i].pos, pos) <= dims.X / 2)
                    {
                        Units[i].GetHit(1.0f);
                    }
                }
                currentTick++;                
            }
        }

        public override void ChangePosition()
        {

        }

        public override bool HitSomething(List<DestructibleObject> Units)
        {
            return false;
        }

        public override void Draw(Vector2 Offset)
        {

        }
    }
}