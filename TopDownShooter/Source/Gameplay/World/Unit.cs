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
    public class Unit : Basic2d
    {
        public bool dead;

        public float speed, hitDist, health, healthMax;

        public Unit(string Path, Vector2 Pos, Vector2 Dims) : base(Path, Pos, Dims)
        {
            dead = false;
            speed = 2.0f;

            health = 1;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 Offset)
        {

            base.Update(Offset);
        }

        public virtual void GetHit(float Damage)
        {
            health -= Damage;

            if(health <= 0)
            {
                dead = true;
            }
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
