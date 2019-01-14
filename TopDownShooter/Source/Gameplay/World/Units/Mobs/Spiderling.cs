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
    public class Spiderling : Mob
    {


        public Spiderling(Vector2 Pos, int OwnerId) : base("2d\\Units\\Mobs\\Spider", Pos, new Vector2(20, 20), OwnerId)
        {
            speed = 3.0f;    
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
