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
    public class OakTree : SceneItem
    {
        public OakTree(Vector2 Pos, Vector2 Scale) : base("2d\\UI\\Scene\\oakTree", Pos, new Vector2(100, 100), new Vector2(1, 1), Scale)
        {

        }
    }
}