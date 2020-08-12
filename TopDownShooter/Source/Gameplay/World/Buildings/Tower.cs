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
    public class Tower : Building
    {

        public Tower(Vector2 Pos, Vector2 Frames, int OwnerId) : base("2d\\Building\\Tower", Pos, new Vector2(45, 45), Frames, OwnerId)
        {
            health = 30;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 Offset, Player Enemy, SquareGrid Grid)
        {

            base.Update(Offset, Enemy, Grid);
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
