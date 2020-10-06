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
using System.IO;

namespace TopDownShooter
{
    public class Effect2d : Animated2d
    {

        public bool done, noTimer;

        public JPTimer timer;

        public Effect2d(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, int MSec) : base(Path, Pos, Dims, Frames, Color.White)
        {
            done = false;
            noTimer = false;
            timer = new JPTimer(MSec);
        }

        public override void Update(Vector2 Offset)
        {
            timer.UpdateTimer();
            if(timer.Test() && !noTimer)
            {
                done = true;
            }
            base.Update(Offset);
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
