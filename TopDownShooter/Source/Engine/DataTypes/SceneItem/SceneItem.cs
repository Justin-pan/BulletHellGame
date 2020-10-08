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
    public class SceneItem : Animated2d
    {
        public SceneItem(string Path, Vector2 Pos, Vector2 Dims, Vector2 Frames, Vector2 Scale) : base(Path, Pos, Dims * Scale, Frames, Color.White)
        {
            
        }

    }
}