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
    public class Skill
    {
        protected bool active;
        public bool done;

        public DestructibleObject owner;

        public Effect2d targetEffect;

        public Skill(DestructibleObject Owner)
        {
            active = false;
            done = false;

            owner = Owner;

            targetEffect = new TargetCircle(new Vector2(0,0), new Vector2(150, 150));
        }
        #region Properties
        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                if(value && !active)
                {
                    targetEffect.done = false;
                    GameGlobals.PassEffect(targetEffect);
                }

                active = value;
            }
        }
        #endregion

        public virtual void Update(Vector2 Offset, Player Enemy)
        {
            if(active && !done)
            {
                Targeting(Offset, Enemy);
            }
        }

        public virtual void Reset()
        {
            active = false;
            done = false;
            targetEffect.done = true;
        }

        public virtual void Targeting(Vector2 Offset, Player Enemy)
        {
            if(Globals.mouse.LeftClickRelease())
            {
                active = false;
                done = true;
            }
        }
    }
}