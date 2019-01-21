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
    public class ArrowTower : Building
    {
        int range;

        JPTimer shootTimer = new JPTimer(1000);


        public ArrowTower(Vector2 Pos, int OwnerId) : base("2d\\Building\\ArrowTower", Pos, new Vector2(45, 45), OwnerId)
        {
            range = 220;

            health = 10;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 Offset, Player Enemy)
        {
            shootTimer.UpdateTimer();
            if (shootTimer.Test())
            {
                ShootArrow(Enemy);
                shootTimer.ResetToZero();
            }

            base.Update(Offset);
        }

        public virtual void ShootArrow(Player Enemy)
        {
            float closestDist = range, currentDist = 0;
            Unit closest = null;

            for (int i = 0; i < Enemy.units.Count; i++)
            {
                currentDist = Globals.GetDistance(pos, Enemy.units[i].pos);

                if (closestDist > currentDist)
                {
                    closestDist = currentDist;
                    closest = Enemy.units[i];
                }
            }

            if (closest != null)
            {
                GameGlobals.PassProjectile(new Arrow(new Vector2(pos.X,pos.Y), this, new Vector2(closest.pos.X, closest.pos.Y)));
            }
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
