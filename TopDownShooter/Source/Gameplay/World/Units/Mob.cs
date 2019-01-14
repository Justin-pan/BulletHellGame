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

        public Mob(string Path, Vector2 Pos, Vector2 Dims, int OwnerId) : base(Path, Pos, Dims, OwnerId)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 Offset, Player Enemy)
        {
            AI(Enemy.hero);

            base.Update(Offset);
        }

        public virtual void AI(Hero Hero)
        {
            pos += Globals.RadialMovement(Hero.pos, pos, speed);
            rot = Globals.RotateTowards(pos, Hero.pos);

            if (Globals.GetDistance(pos, Hero.pos) < 15)
            {
                //can create a var in the specific mob class to change damage amounts and override
                Hero.GetHit(1);
                dead = true;
            }
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
