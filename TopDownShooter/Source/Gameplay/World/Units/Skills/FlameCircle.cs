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
    public class FlameCircle : Skill
    {
        public FlameCircle(DestructibleObject Owner) : base(Owner) 
        {
            
        }

        public override void Targeting(Vector2 Offset, Player Enemy)
        {
            if(Globals.mouse.LeftClickRelease())
            {
                targetEffect.done = true;

                GameGlobals.PassProjectile(new FlameCircleProjectile(Globals.mouse.newMousePos - Offset, owner, new Vector2(0, 0), 1500));

                done = true;
                active = false;
            }
            else
            {
                targetEffect.pos = Globals.mouse.newMousePos - Offset;
            }
        }
    }
}