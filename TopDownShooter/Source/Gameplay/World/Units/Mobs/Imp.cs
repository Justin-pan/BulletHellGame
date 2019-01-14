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
    public class Imp : Mob
    {

        public Imp(Vector2 Pos, int OwnerId) : base("2d\\Units\\Mobs\\Imp", Pos, new Vector2(40, 40), OwnerId)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 Offset, Player Enemy)
        {

            base.Update(Offset, Enemy);
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
