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
    public class Arrow : Projectile
    {

        public Arrow(Vector2 Pos, DestructibleObject Owner, Vector2 Target) : base("2d\\Projectiles\\Arrow", Pos, new Vector2(10, 20), Owner, Target)
        {
            speed = 9.0f;

            timer = new JPTimer(1000);
        }

        public override void Update(Vector2 Offset, List<DestructibleObject> Units)
        {
            base.Update(Offset, Units);
        }
        
        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
