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
    public class FlameCircleProjectile : StillInvisibleProjectile
    {

        public FlameCircleProjectile(Vector2 Pos, DestructibleObject Owner, Vector2 Target, int MSec)
            : base(Pos, new Vector2(150, 150), Owner, Target, MSec)
        {
            GameGlobals.PassEffect(new FireNova(new Vector2(Pos.X, Pos.Y), new Vector2(dims.X, dims.Y), MSec));
        }

        public override void Update(Vector2 Offset, List<DestructibleObject> Units)
        {
            base.Update(Offset, Units);
        }

        public override void ChangePosition()
        {

        }

        public override bool HitSomething(List<DestructibleObject> Units)
        {
            return false;
        }

        public override void Draw(Vector2 Offset)
        {

        }
    }
}