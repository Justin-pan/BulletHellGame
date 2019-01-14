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
    public class Projectile : Basic2d
    {
        public bool done;

        public float speed;

        public Vector2 direction;

        public Unit owner;

        public JPTimer timer;

        public Projectile(string Path, Vector2 Pos, Vector2 Dims, Unit Owner, Vector2 Target) : base(Path, Pos, Dims)
        {
            done = false;

            speed = 5.0f;

            owner = Owner;

            direction = Target - owner.pos;
            direction.Normalize();

            rot = Globals.RotateTowards(pos, new Vector2(Target.X, Target.Y));

            timer = new JPTimer(1200);
        }

        public virtual void Update(Vector2 Offset, List<Unit> Units)
        {
            pos += direction * speed;

            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }

            if (HitSomething(Units))
            {
                done = true;
            }
        }
        
        public virtual bool HitSomething(List<Unit> Units) 
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Globals.GetDistance(pos, Units[i].pos) < Units[i].hitDist)
                {
                    Units[i].GetHit(1);
                    return true;
                }
            }

            return false;
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
