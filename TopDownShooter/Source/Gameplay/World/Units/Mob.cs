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

        public Mob(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, int OwnerId) : base(Path, Pos, Dims, Frames, OwnerId)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 Offset, Player Enemy)
        {
            AI(Enemy);

            base.Update(Offset);
        }

        public virtual void AI(Player Enemy)
        {
            pos += Globals.RadialMovement(Enemy.hero.pos, pos, speed);
            rot = Globals.RotateTowards(pos, Enemy.hero.pos);

            if (Globals.GetDistance(pos, Enemy.hero.pos) < 15)
            {
                //can create a var in the specific mob class to change damage amounts and override
                Enemy.hero.GetHit(1);
                dead = true;
            }
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
