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
    public class Building : DestructibleObject
    {

        public Building(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, int OwnerId) : base(Path, Pos, Dims, Frames, OwnerId)
        {

        }

        public override void Update(Vector2 Offset, Player Enemy)
        {

            base.Update(Offset);
        }

        public override void Draw(Vector2 Offset)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue((float)texture.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)texture.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(Offset);
        }
    }
}
