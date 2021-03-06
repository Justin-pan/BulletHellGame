﻿using System;
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

        public DestructibleObject owner;

        public JPTimer timer;

        public Projectile(string Path, Vector2 Pos, Vector2 Dims, DestructibleObject Owner, Vector2 Target) : base(Path, Pos, Dims)
        {
            done = false;

            speed = 5.0f;

            owner = Owner;

            direction = Target - owner.pos;
            direction.Normalize();

            rot = Globals.RotateTowards(pos, new Vector2(Target.X, Target.Y));

            timer = new JPTimer(1600);
        }

        public virtual void Update(Vector2 Offset, List<DestructibleObject> Units)
        {
            ChangePosition();

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

        public virtual void ChangePosition()
        {
            pos += direction * speed;
        }
        
        public virtual bool HitSomething(List<DestructibleObject> Units) 
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (owner.ownerId != Units[i].ownerId && Globals.GetDistance(pos, Units[i].pos) < Units[i].hitDist)
                {
                    Units[i].GetHit(owner, 1);
                    return true;
                }
            }

            return false;
        }

        public override void Draw(Vector2 Offset)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue((float)texture.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)texture.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(Offset);
        }
    }
}
